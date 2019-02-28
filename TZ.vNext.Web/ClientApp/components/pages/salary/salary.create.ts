import Vue from "vue"
import { Component, Prop } from "vue-property-decorator"
import { Form, FormItem, Input, Row, Col, Button, Message, Collapse, CollapseItem } from "element-ui"
import "element-ui/lib/theme-chalk/index.css"
import { Salary } from "../../model/Salary"
import remote from "../../common/TzValidators"
import { TzApiConst, TzMessageConst, TzRuleMsgConst } from "../../common/TzCommonConst";
import { TzFetch } from "../../common/TzFetch";

Vue.use(Form)
Vue.use(FormItem)
Vue.use(Input)
Vue.use(Row)
Vue.use(Col)
Vue.use(Button)
Vue.use(Collapse)
Vue.use(CollapseItem)

@Component({
    props: ["id"],
    components: {
        TzEnumSelect: require("../../wrapper/TzEnumSelect.vue.html")
    }
})
export default class SalaryCreateComponent extends Vue {
    @Prop() id!: string

    rules = {
        Name: [{ required: true, message: TzRuleMsgConst.SALARY_NAME_REQUIRED, trigger: "change" },
            { validator: remote, url: TzApiConst.SALARY_CHECKNAME + '?id=' + this.id, message: TzRuleMsgConst.SALARY_NAME_REPEATED, trigger: 'blur' }],
        FormContent: [{ required: true, message: TzRuleMsgConst.SALARY_FORMCONTENT_REQUIRED, trigger: "change" }]
    }

    model = new Salary()

    created() {
        if (this.id) {
            TzFetch.Post(TzApiConst.SALARY_FINDBYID, { id: this.id }).then(data => {
                if (data) {
                    this.model = data
                }
                else {
                    Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
                }
            }).catch(e => {
                Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            });
        }
    }

    submitForm(formName) {
        (this.$refs[formName] as any).validate(valid => {
            if (valid) {
                TzFetch.Post(TzApiConst.SALARY_SAVE, this.model).then((data: any) => {
                    if (data && data.Name != null) {
                        this.$router.replace("/parts")
                    } else {
                        Message.error(TzMessageConst.SAVE_FAIL_MESSAGE)
                    }
                }).catch(e => {
                    Message.error(TzMessageConst.SAVE_FAIL_MESSAGE)
                })
            } else {
                return false
            }
        })
    }
}