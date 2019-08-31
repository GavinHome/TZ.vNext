import Vue from "vue";
import { Component, Prop, Model } from 'vue-property-decorator';
import { TzFetch } from "../../common/TzFetch";
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
import { TzSuperFormGroup } from "../../wrapper/TzSuperForm/schema/TzSuperFormSchema";
import { Product } from "../../model/Product";
import { Message } from "element-ui"

@Component({
    props: ["id"],
    components: {
        TzSuperForm: require("../../wrapper/TzSuperForm/index.vue.html"),
    },
})
export default class ProductDetail extends Vue {
    @Prop() id!: string

    model!: Product

    get data() {
        return this.model ? this.model.Content : null
    }

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
}