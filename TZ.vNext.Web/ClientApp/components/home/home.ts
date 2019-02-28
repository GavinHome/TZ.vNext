import Vue from "vue";
import { Component } from "vue-property-decorator";
import "../extension/DateExtensions";
import { TzApiConst } from "../common/TzCommonConst";
import { TzFetch } from "../common/TzFetch";

interface IBacklogBlock {
    ModuleName: string;
    icon: string;
    Count: number;
    ProcessDefName: string;
    ProcessChName: string;
}

@Component({
    components: {}
})
export default class HomeComponent extends Vue {
    currentDate = new Date().toFormatString("yyyy年MM月dd日");

    modules: IBacklogBlock[] = [];

    created() {
    }

    goto(block: IBacklogBlock) {
        this.$router.push({ path: "/backlogs/" + block.ModuleName, query: {} });
    }
}
