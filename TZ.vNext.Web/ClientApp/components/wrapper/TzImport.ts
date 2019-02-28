import Vue from "vue"
import { Component, Prop } from "vue-property-decorator"
import { Form, FormItem, Row, Col, Button, Upload, Message, Select, Option } from "element-ui"
import "element-ui/lib/theme-chalk/index.css"
import StoreCache from "../../components/common/TzStoreCache";
import { TzMessageConst } from "../common/TzCommonConst";
Vue.use(Form)
Vue.use(FormItem)
Vue.use(Row)
Vue.use(Col)
Vue.use(Button)
Vue.use(Upload);
Vue.use(Select);
Vue.use(Option);

@Component({
    props: ["url", "message"],
    components: {
    }
})
export default class TzImport extends Vue {
    @Prop() url!: string;
    @Prop() message: string="";
    key!: string;
    buttonDisabled = true;
    requestHeaders = {'Authorization': `Bearer ${(new StoreCache('auth')).get('token')}`}
    submit() {
        this.$emit("submit", this.key);
    }

    close() {
        this.$emit("close", null);
    }

    onSuccess(res, file, files) {
        if (res) {
            this.message = "";
            this.key = res.key
            if (this.key != null) {
                this.buttonDisabled = false
            }
            else {
                this.buttonDisabled = true
            }

            res.messages.forEach(item => {
                this.message += item + TzMessageConst.SYMBOL_LINEBREAKER
            });

        } else {
            this.key = ''
            this.buttonDisabled = true
        }
    }

    onError(err, file, files) {
        console.log(err)
        Message.error(TzMessageConst.UPLOAD_FAIL_MESSAGE)
    }

    onExceed(file, files) {
        Message.error(TzMessageConst.UPLOAD_EXCEED_MESSAGE);
    }

    onRemove() {
        this.message = ""
    }
}