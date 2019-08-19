import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import tpl from "./form_tpl"

import 'element-ui/lib/theme-chalk/index.css'
import { Row, Col, FormItem, Form } from 'element-ui'
import { TzSuperFormGroup, TzSuperFormRow } from "../../TzSuperForm/schema/TzSuperFormSchema";
import { TzSuperFormField } from "../BuilderFormComps";
import Guid from "../../../common/Guid";

Vue.use(Row)
Vue.use(Col)
Vue.use(Form)
Vue.use(FormItem)


@Component({
    props: ["form", "formData", "rules", "formAttr"],
    components: {
        JsonEditor: require('../../TzSuperForm/components/TzSuperJsonEditor.vue.html'),
        Codemirror: require('../../TzSuperForm/components/TzSuperCodemirror.vue.html')
    }
})
export default class BuilderAppFormHeader extends Vue {
    @Prop() formAttr!: any
    @Prop() form!: any
    @Prop() formData!: any
    @Prop() rules!: any

    isShowData: boolean = false
    isShowCode: boolean = false

    get view_code_html() {
        let htmlFormAttr: string[] = []
        const formAttrEntries = Object.entries(this.formAttr)

        // 拼接form属性
        if (formAttrEntries.length) {
            htmlFormAttr = formAttrEntries.reduce((acc: string[], val) => {
                acc.push(`:${val[0]}="${val[1]}"`)
                return acc
            }, [])
        }

        return tpl.template_tpl.replace('%1', htmlFormAttr.join('\n    '))
    }

    get render_code_html() {
        return tpl.render_tpl.replace('%1', JSON.stringify(this.form_render, null, 4))
            .replace('%2', JSON.stringify(this.formData, null, 4))
            .replace('%3', JSON.stringify(this.rules, null, 4))
            .replace('%4', "100")
    }

    get form_render() {
        var forms: TzSuperFormGroup[] = []
        this.form.forEach((g, a) => {
            var fields: TzSuperFormField [] = []
            g.rows.forEach((r, b) => {
                fields = fields.concat(r.fields)
            })

            var rows = rows = this.getGroupRows(fields);

            var group: TzSuperFormGroup = {
                key: g.key,
                name: g.name,
                title: g.title,
                isCollapsed: g.isCollapsed,
                rows: rows
            }

            forms.push(group)
        })

        return forms;
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
                if (row.fields.map(x => x.cols ? x.cols : 1).sum() + item.cols <= 3) {
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

                if (row.fields.map(x => x.cols ? x.cols : 1).sum() == 3) {
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
}