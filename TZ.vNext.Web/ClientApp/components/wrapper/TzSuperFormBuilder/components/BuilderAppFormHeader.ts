import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import tpl from "./form_tpl"

import 'element-ui/lib/theme-chalk/index.css'
import { Row, Col, FormItem, Form } from 'element-ui'

Vue.use(Row)
Vue.use(Col)
Vue.use(Form)
Vue.use(FormItem)


@Component({
    props: ["form", "formData", "rules", "formAttr"],
    components: {
        JsonEditor: require('../../TzSuperForm/TzSuperJsonEditor.vue.html'),
        Codemirror: require('../../TzSuperForm/TzSuperCodemirror.vue.html')
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
        return tpl.render_tpl.replace('%1', JSON.stringify(this.form, null, 4))
            .replace('%2', JSON.stringify(this.formData, null, 4))
            .replace('%3', JSON.stringify(this.rules, null, 4))
            .replace('%4', "100")
    }
}