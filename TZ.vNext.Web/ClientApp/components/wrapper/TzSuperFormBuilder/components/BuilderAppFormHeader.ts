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
    props: ["form", "formData", "rules"],
    components: {
        JsonEditor: require('../../TzSuperForm/TzSuperJsonEditor.vue.html'),
        Codemirror: require('../../TzSuperForm/TzSuperCodemirror.vue.html')
    }
})
export default class BuilderAppFormHeader extends Vue {
    @Prop() form!: any
    @Prop() formData!: any
    @Prop() rules!: any

    isShowData: boolean = false
    isShowCode: boolean = false

    get view_code_html() {
        return tpl.template_tpl
    }

    get render_code_html() {
        return tpl.render_tpl.replace('%1', JSON.stringify(this.form, null, 4))
            .replace('%2', JSON.stringify(this.formData))
            .replace('%3', JSON.stringify(this.rules))
            .replace('%4', "100")
    }
}