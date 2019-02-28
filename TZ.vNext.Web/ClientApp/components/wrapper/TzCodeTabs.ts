import Vue from 'vue'
import { Component, Prop } from 'vue-property-decorator'

import { Tabs, TabPane, Message } from "element-ui"
import "element-ui/lib/theme-chalk/index.css"
import { ISelect } from '../schemas/SelectItemSchema';
import { TzApiConst, TzMessageConst } from '../common/TzCommonConst';
import { TzFetch } from '../common/TzFetch';

Vue.use(Tabs);
Vue.use(TabPane);

@Component({
    props: ["codeType"]
})

export default class TzCodeTabs extends Vue {
    options: ISelect[] = [];
    activeTab: string = "";
    @Prop() codeType!: number;

    created() {
        if (!this.codeType) {
            throw new Error("code type error")
        }

        TzFetch.Get(TzApiConst.CODE_TREE + '?codeType=' + this.codeType, false).then((res: any) => {
            res.forEach((element, i) => {
                this.options.push({ value: element.Value, label: element.Label })
            });

            if (this.options[0]) {
                this.activeTab = this.options[0].value
                //this.tabChange(this.activeTab)
                this.$emit('tabChange', this.activeTab)
            }
        }).catch(e => {
            Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
        })
    }

    get getOptions() {
        return this.options;
    }

    tabChange(tab, event) {
        this.$emit('tabChange', tab.name)
    }
}
