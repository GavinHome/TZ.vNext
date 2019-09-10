import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzFetch } from "../../common/TzFetch";
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
import { Product } from "../../model/Product";
import { Message, Row, Col, FormItem } from "element-ui";
import TzSuperFormBuilder from "../../wrapper/TzSuperFormBuilder";

Vue.use(Row);
Vue.use(Col);
Vue.use(FormItem);

@Component({
    props: ["id"],
    components: {
        TzSuperFormBuilder: require("../../wrapper/TzSuperFormBuilder/index.vue.html")
    },
})
export default class Processing extends Vue {
    @Prop() id!: string

    model = new Product()

    editDescription: boolean = false

    timer!: any

    status: string = ""

    get data() {
        return this.model ? this.model.ContentData : null
    }

    created() {
        if (this.id) {
            TzFetch.Post(TzApiConst.PRODUCT_FINDBYID, { id: this.id }).then(data => {
                this.model = data as Product;
                if (this.model) {
                    if (this.model && this.model.ContentData) {
                        (this.$refs.builder as TzSuperFormBuilder).setForm(this.model.ContentData.form, this.model.ContentData.formData, this.model.ContentData.rules, this.model.ContentData.formAttr)
                    }

                    this.$nextTick(() => {
                        this.setTimer();
                    })
                }
                else {
                    Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
                }
            }).catch(e => {
                Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            });
        }
    }

    setTimer() {
        this.timer = setInterval(() => {
            this.submit()
        }, 10000)
    }

    beforeDestroy() {
        if (this.timer) {
            clearInterval(this.timer);
        }
    }

    submit() {
        this.status = "正在保存..."
        this.model.ContentData = {
            form: (this.$refs.builder as TzSuperFormBuilder).form,
            formData: (this.$refs.builder as TzSuperFormBuilder).formData,
            rules: (this.$refs.builder as TzSuperFormBuilder).rules,
            formAttr: (this.$refs.builder as TzSuperFormBuilder).formAttr
        }
        
        TzFetch.Post(TzApiConst.PRODUCT_SAVE, this.model, false).then( d => {
            this.status = "保存完成..."
            setTimeout(() => {
                this.status = ""
            }, 3000);
        }).catch(e => Message.error(TzMessageConst.DATA_FAIL_MESSAGE));
    }

    back() {
        this.$router.go(-1);
    }
}