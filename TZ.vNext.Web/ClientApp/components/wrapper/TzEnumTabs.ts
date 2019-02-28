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
    props: ["options", "defaultActiveTab"]
})

export default class TzCodeTabs extends Vue {
    activeTab = ""
    @Prop({ default: [] }) options!: ISelect[];
    @Prop({ default: "" }) defaultActiveTab!: string;

    created() {
        this.activeTab = this.defaultActiveTab;
    }

    tabChange(tab, event) {
        this.$emit('tabChange', tab.name)
    }
}
