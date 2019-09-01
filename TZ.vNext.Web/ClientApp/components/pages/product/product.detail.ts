import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzFetch } from "../../common/TzFetch";
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
import { Product } from "../../model/Product";
import { Message } from "element-ui";
import 'element-ui/lib/theme-chalk/index.css'
import ElementUI from 'element-ui'
import { FieldTypeEnum, EnumHelper, EnumConstType } from "../../common/Enums";
import { TzSuperFormType, TzSuperFormGroup } from "../../wrapper/TzSuperForm/schema/TzSuperFormSchema";
Vue.use(ElementUI)

@Component({
    props: ["id"],
    components: {
        TzSuperForm: require("../../wrapper/TzSuperForm/index.vue.html"),
    },
})
export default class ProductDetail extends Vue {
    @Prop() id!: string

    model: Product = {}

    created() {
        if (this.id) {
            TzFetch.Post(TzApiConst.PRODUCT_FINDBYID, { id: this.id }).then(data => {
                if (data) {
                    this.model = data as Product
                }
                else {
                    Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
                }
            }).catch(e => {
                Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            });
        }
    }

    handleSubmit(data) {
        console.log("submit：" + data)
        this.$message.success(JSON.stringify(data))
        return Promise.resolve(data)
    }

    handleSuccess(response) {
        console.log("success" + response)
        this.$message.success('创建成功')
    }

    handleError(response) {
        console.log("error" + response)
        this.$message.success('创建失败')
    }

    handleEnd(response) {
        console.log("end: " + response)
        this.$message.success('处理结束')
    }

    handleRequest(response) {
        console.log("handleRequest" + response)
        this.$message.success('自定义处理')
    }
}