import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormField, getFormDesc } from "../TzSuperForm/schema/TzSuperFormSchema";
import 'element-ui/lib/theme-chalk/index.css';
import { Container, Aside, Header, Main, TabPane, Tabs, Dialog, Button } from 'element-ui';
import Guid from "../../common/Guid";
import "../../extension/ArrayExtensions";
import TzAreaCascader from "../TzAreaCascader";

Vue.use(Container)
Vue.use(Aside)
Vue.use(Header)
Vue.use(Main)
Vue.use(TabPane)
Vue.use(Tabs)
Vue.use(Dialog)
Vue.use(Button)

@Component({
    props: [],
    components: {
        AppForm: require('./components/BuilderAppForm.vue.html'),
        AppFormItemProperty: require('./components/BuilderAppFormItemProperty.vue.html'),
        AppFormProperty: require('./components/BuilderAppFormProperty.vue.html'),
        AppFormComponents: require('./components/BuilderAppFormComponents.vue.html'),
        JsonEditor: require('../TzSuperForm/components/TzSuperJsonEditor.vue.html'),
        TzAreaCascader: require("../TzAreaCascader.vue.html"),
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

    form_init_data: any = []
    isInitData: boolean = false

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

                            this.renderValidetor(f.name, newVal.required, newVal.label);
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
            if (Object.keys(this.rules).indexOf(name) > -1) {
                delete this.rules[name]
            }
        }
    }

    handleDeleteField(data) {
        if (Object.keys(this.rules).indexOf(data) > -1) {
            delete this.rules[data]
        }
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

    initForms(data) {
        this.form = this.form_init_data ? this.form_init_data : []
    }

    handleJsonChanged(data) {
        this.form_init_data = data
    }

    created() {
        this.form_init_data = [
            {
                "key": "basic",
                "name": "basic",
                "title": "基础信息",
                "isCollapsed": false,
                "rows": [
                    {
                        "key": "369eceee-f7e7-4747-82a5-a5a301dfbb6d",
                        "name": "369eceee-f7e7-4747-82a5-a5a301dfbb6d",
                        "fields": [
                            {
                                "key": "title",
                                "name": "title",
                                "label": "标题",
                                "type": "input",
                                "title": "标题",
                                "format": null,
                                "options": null,
                                "cols": 1,
                                "attrs": null,
                                "slots": null
                            },
                            {
                                "key": "count",
                                "name": "count",
                                "label": "数量",
                                "type": "number",
                                "title": "数量",
                                "format": null,
                                "options": null,
                                "cols": 1,
                                "attrs": null,
                                "slots": null
                            },
                            {
                                "key": "number",
                                "name": "number",
                                "label": "人数",
                                "type": "number",
                                "title": "人数",
                                "format": null,
                                "options": null,
                                "cols": 1,
                                "attrs": null,
                                "slots": null
                            },
                            {
                                "key": "finished",
                                "name": "finished",
                                "label": "是否完成",
                                "type": "select",
                                "title": "是否完成",
                                "format": null,
                                "options": [
                                    {
                                        "text": "是",
                                        "value": true
                                    },
                                    {
                                        "text": "否",
                                        "value": false
                                    }
                                ],
                                "cols": 1,
                                "attrs": null,
                                "slots": null
                            },
                            {
                                "key": "user",
                                "name": "user",
                                "label": "申请人",
                                "type": "dialog",
                                "title": "申请人",
                                "format": null,
                                "options": null,
                                "cols": 1,
                                "attrs": null,
                                "slots": []
                            },
                            {
                                "key": "total",
                                "name": "total",
                                "label": "总计",
                                "type": "text",
                                "title": "总计",
                                "format": null,
                                "options": null,
                                "cols": 1,
                                "attrs": null,
                                "slots": null
                            },
                            {
                                "key": "shell",
                                "name": "shell",
                                "label": "超级外壳",
                                "type": "shell",
                                "title": "超级外壳",
                                "format": null,
                                "options": null,
                                "cols": 3,
                                "attrs": null,
                                "slots": [
                                    {
                                        "type": "tz-area-cascader",
                                        "component": TzAreaCascader,
                                        "props": {},
                                        "default": ["中国", "陕西", "西安"],
                                        "on": {
                                            change: data => {}
                                        }
                                    }
                                ]
                            },
                            {
                                "key": "content",
                                "name": "content",
                                "label": "内容",
                                "type": "textarea",
                                "title": "内容",
                                "format": null,
                                "options": null,
                                "cols": 3,
                                "attrs": null,
                                "slots": null
                            }
                        ]
                    }
                ]
            }
        ]
    }
}