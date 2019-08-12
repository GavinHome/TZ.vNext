import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';

@Component({
    props: ["formItem"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html")
    }
})
export default class BuilderAppFormProperty extends Vue {
    @Prop() formItem!: any

    formData: any = {}

    get form() {
        if (this.formItem.formDesc) {
            var result: any = [
                {
                    key: "basic",
                    name: "basic",
                    title: "基础信息",
                    isCollapsed: false,
                    rows: [
                        {
                            key: "basic-row1",
                            name: "basic-row1",
                            fields: this.formItem.formDesc
                        }
                    ]
                }
            ]

            return result;
        }

        return []
    }

    @Watch('formData', { immediate: true, deep: true })
    onFormDataChanged(val: string, oldVal: string) {
        this.$emit("formItemPropertyChange", val, oldVal)
    }

    @Watch('formItem', { immediate: true, deep: true })
    onFormItemaChanged(val: string, oldVal: string) {
        this.formData = this.formItem ? this.formItem.formData : {}
    }
}