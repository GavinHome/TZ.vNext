import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzFetch } from "../../common/TzFetch";
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
import { Product } from "../../model/Product";

@Component({
    props: ["id"],
    components: {
        TzSuperFormBuilder: require("../../wrapper/TzSuperFormBuilder/index.vue.html")
    },
})
export default class Processing extends Vue {
    @Prop() id!: string

    model!: Product

    timer!: any

    get data() {
        return this.model ? this.model.Content : null
    }

    created() {
        if (this.id) {
            // TzFetch.Post(TzApiConst.PRODUCT_FINDBYID, { id: this.id }).then(data => {
            //     if (data) {
            //         this.model = data as Product
            //     }
            //     else {
            //         Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            //     }
            // }).catch(e => {
            //     Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            // });
        }
    }

    mounted() {
        //this.timer = setInterval(() => {
        //TzFetch.Post(TzApiConst.PRODUCT_SAVE, this.model).catch(e => Message.error(TzMessageConst.DATA_FAIL_MESSAGE));
        //},1000)
    }

    beforeDestroy() {
        if(this.timer) {
            clearInterval(this.timer);
        }
    }
}