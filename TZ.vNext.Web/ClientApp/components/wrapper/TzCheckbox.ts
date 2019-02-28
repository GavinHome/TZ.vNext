import Vue from "vue"
import { Component, Prop } from "vue-property-decorator"
import { CheckboxGroup, Message, Checkbox, Scrollbar } from "element-ui"
import { TzMessageConst } from "../common/TzCommonConst";
import { TzFetch } from "../common/TzFetch";
import { ISelect } from "../schemas/SelectItemSchema";

Vue.use(CheckboxGroup);
Vue.use(Checkbox);
Vue.use(Scrollbar);

@Component({
    props: ["value", "vertical", "url"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzCheckBox extends Vue {
    options: ISelect[] = [];
    checkAll = false
    checkedTemplates = []
    isIndeterminate = true

    @Prop() vertical!: boolean
    @Prop() url!: string;

    get getOptions() {
        return this.options;
    }

    created() {
        if (!this.url) {
            throw new Error(TzMessageConst.FETCH_URL_EXCEPTION_MESSAGE)
        }

        TzFetch.Post(this.url, {}, false).then(data => {
            if (data) {
                (data as any[]).forEach(element => {
                    this.options.push({ label: element.Label, value: element.Value })
                });
            }
            else {
                Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            }
        }).catch(e => {
            Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
        })
    }

    handleCheckAllChange(val) {
        let all: any = [];
        this.getOptions.forEach(element => {
            all.push(element.value)
        });
        this.checkedTemplates = val ? all : [];
        this.isIndeterminate = false;
        this.$emit("change", this.checkedTemplates);
    }

    handleCheckedChange(value) {
        let checkedCount = value.length;
        this.checkAll = checkedCount === this.getOptions.length;
        this.isIndeterminate = checkedCount > 0 && checkedCount < this.getOptions.length;
        this.$emit("change", this.checkedTemplates);
    }
}
