import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import { Tree, Row, Col, Button, Message } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import { TzApiConst, TzMessageConst } from '../common/TzCommonConst';
import { TzFetch } from '../common/TzFetch';

Vue.use(Tree);
Vue.use(Row);
Vue.use(Col);
Vue.use(Button);

interface NodeInfo {
    Label: string;
    Value: string;
    IsLeaf: boolean;
}

@Component({
    props: ["codeType", "multiply"]
})
export default class TzCode extends Vue {
    data = []
    props = {
        label: 'Label',
        value: 'Value',
        id: 'value',
        children: 'Children',
        isLeaf: 'IsLeaf'
    }

    @Prop() codeType!: number;
    @Prop() multiply!: boolean;

    created() {
        if (!this.codeType) {
            throw new Error("code type error")
        }
    }

    loadNode(node, resolve) {
        var params = node.level === 0 ? '?codeType=' + this.codeType : 'codeType=' + this.codeType + '&parentId=' + node.data.Value
        TzFetch.Get(TzApiConst.CODE_TREE + params, false).then(res => resolve(res)).catch(e => {
            Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
        })
    }

    onSelectedCustomer(dataItem) {
        this.$emit("submit", dataItem)
    }

    dblclick(data) {
        if (data.IsLeaf) {
            this.onSelectedCustomer({ Id: data.Value, Name: data.Label })
        }
    }

    submitCode() {
        var nodes = (this.$refs.tree as any).getCheckedNodes().filter(x => x.IsLeaf) as NodeInfo[];
        let ids: string[] = [];
        let names: string[] = [];
        nodes.forEach((node) => {
            names.push(node.Label);
            ids.push(node.Value);
        });

        this.$emit("submit", { Id: ids.join(","), Name: names.join(",") })
    }

    close() {
        this.$emit("close", null)
    }
}
