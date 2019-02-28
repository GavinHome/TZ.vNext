import Vue from "vue"
import { Component, Prop } from "vue-property-decorator"

import "element-ui/lib/theme-chalk/index.css"
import { Form, FormItem, Input, Row, Col, Button, Collapse, CollapseItem, Message } from "element-ui"

import { Salary } from "../../model/Salary"
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
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
    props: ["id"]
})
export default class SalaryDetailComponent extends Vue {
    @Prop() id!: string
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
}