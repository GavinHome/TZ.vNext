import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'

import { Select, Option, Message } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import { ISelect } from '../schemas/SelectItemSchema';
import { TzApiConst, TzMessageConst } from '../common/TzCommonConst';
import { TzFetch } from '../common/TzFetch';

Vue.use(Select);
Vue.use(Option);

@Component({
    props: ["value", "codeType", "placeholder"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzCode extends Vue {
    options: ISelect[] = [];
    @Prop() codeType!: number;

    created() {
        if (!this.codeType) {
            throw new Error("code type error")
        }
        TzFetch.Get(TzApiConst.CODE_TREE + '?codeType=' + this.codeType, false).then((res: any) => {
            res.forEach((element, i) => {
                this.options.push({ value: element.Value, label: element.Label })
            }).catch(e => {
                Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            })
        })
    }

    get getOptions() {
        return this.options;
    }

    update(value) {
        this.$emit('change', value)
    }
}
