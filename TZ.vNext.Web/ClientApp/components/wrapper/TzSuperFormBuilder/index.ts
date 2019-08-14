import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormRow, TzSuperFormField } from "../TzSuperForm/TzSuperFormSchema";
import 'element-ui/lib/theme-chalk/index.css';
import { Container, Aside, Header, Main, TabPane, Tabs } from 'element-ui';
import Guid from "../../common/Guid";
import { getFormDesc } from "../TzSuperForm/TzSuperFunc";
Vue.use(Container)
Vue.use(Aside)
Vue.use(Header)
Vue.use(Main)
Vue.use(TabPane)
Vue.use(Tabs)

@Component({
    props: [],
    components: {
        AppForm: require('./components/BuilderAppForm.vue.html'),
        AppFormItemProperty: require('./components/BuilderAppFormItemProperty.vue.html'),
        AppFormProperty: require('./components/BuilderAppFormProperty.vue.html'),
        AppFormComponents: require('./components/BuilderAppFormComponents.vue.html'),
    }
})
export default class TzSuperFormBuilder extends Vue {
    activeTab: number = 0
    selectFormItem: any = {}
    formAttr: any = {}
    rules: any = {}

    form: TzSuperFormGroup[] = [
        {
            key: "basic",
            name: "basic",
            title: "基础信息",
            isCollapsed: false,
            rows: [
                {
                    key: "basic-row1",
                    name: "basic-row1",
                    fields: [
                    ]
                }
            ]
        }
    ]

    get formData() {
        var desc = getFormDesc(this.form)
        var data = {}
        desc.forEach(d => {
            data[d.name] = null
        })
        
        return data;
    }

    handleFormPropertyChange(attr) {
        this.formAttr = attr
    }

    handleSelectedFormItem(item) {
        this.selectFormItem = item
    }

    handleFormItemPropertyChange(newVal, oldVal) {
        if (oldVal && newVal && newVal.key === oldVal.key) {
            this.form.forEach((g, a) => {
                g.rows.forEach((r, b) => {
                    r.fields.forEach((f, c) => {
                        if (f.key === oldVal.key) {
                            delete this.rules[f.name]
                            delete this.rules[newVal.name]

                            f.name = newVal.name
                            f.label = newVal.label
                            f.title = newVal.label
                            f.cols = newVal.cols
                            this.renderGroup(g.key)
                            this.renderValidetor(f.name, newVal.required, newVal.label)
                        }
                    })
                })
            })
        }
    }

    renderValidetor(name: string, required: boolean, title: string) {
        if (required) {
            Vue.set(this.rules, name, { required: required, message: "请输入" + title, trigger: 'blur' })
        } else {
            if(Object.keys(this.rules).indexOf(name) > -1) {
                delete this.rules[name]
            }
        }
    }

    renderGroup(groupKey) {
        var data: TzSuperFormField[] = []
        var group = this.form.filter(x => x.key === groupKey)[0]
        if (group && data && data.length) {
            group.rows.forEach(r => {
                r.fields.forEach(f => {
                    data.push(f)
                })
            })
        }

        this.handleFormChange(groupKey, data)
    }

    handleDeleteField(data) {
        if(Object.keys(this.rules).indexOf(data) > -1) {
            delete this.rules[data]
        }
    }

    handleFormChange(groupKey, data) {
        var group = this.form.filter(x => x.key === groupKey)[0]
        if (group && data && data.length) {
            group.rows.splice(0, group.rows.length)
            var rows = this.getGroupRows(data)
            rows.forEach(r => {
                group.rows.push(r);
            })
        }
    }

    getGroupRows(data): TzSuperFormRow[] {
        var rows: TzSuperFormRow[] = [];

        if (data.length && data.length > 0) {
            var rowKey = Guid.newGuid().toString();
            var row: TzSuperFormRow = {
                key: rowKey,
                name: rowKey,
                fields: []
            }

            data.filter(item => item).forEach(item => {
                if (this.arraySum(row.fields.map(x => x.cols ? x.cols : 1)) + item.cols <= 3) {
                    row.fields.push({
                        key: item.key,
                        name: item.name,
                        label: item.label,
                        type: item.type,
                        title: item.title,
                        format: item.format,
                        options: item.options,
                        cols: item.cols,
                        attrs: item.attrs,
                        slots: item.slots,
                    })
                } else {
                    rows.push(row)
                    rowKey = Guid.newGuid().toString();
                    row = {
                        key: rowKey,
                        name: rowKey,
                        fields: []
                    }

                    row.fields.push({
                        key: item.key,
                        name: item.name,
                        label: item.label,
                        type: item.type,
                        title: item.title,
                        format: item.format,
                        options: item.options,
                        cols: item.cols,
                        attrs: item.attrs,
                        slots: item.slots,
                    })
                }

                if (this.arraySum(row.fields.map(x => x.cols ? x.cols : 1)) == 3) {
                    rows.push(row)
                    rowKey = Guid.newGuid().toString();
                    row = {
                        key: rowKey,
                        name: rowKey,
                        fields: []
                    }
                }
            });

            if (row.fields.length > 0) {
                rows.push(row)
            }
        }

        return rows;
    }

    arraySum(array) {
        var total = 0,
            len = array.length
        for (var i = 0; i < len; i++) {
            total += array[i];
        }

        return total;
    }

    addGroup() {
        this.form.push(this.newGroup(Guid.newGuid().toString()))
    }

    removeGroup(key) {
        this.form.forEach((group, a) => {
            if (group.key === key) {
                this.form.splice(a, 1)
            }
        })
    }

    newGroup(key) {
        return {
            key: key,
            name: key,
            title: "分组信息",
            isCollapsed: false,
            rows: [
                this.newRow(Guid.newGuid().toString())
            ],
        }
    }

    newRow(key) {
        return {
            key: key,
            name: key,
            fields: [

            ]
        }
    }
}