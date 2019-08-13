import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperFormType } from "../../TzSuperForm/TzSuperFormSchema";
import { TzSuperFormField } from "../BuilderFormComps";

@Component({
    props: ["formItem"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html")
    }
})
export default class BuilderAppFormProperty extends Vue {
    @Prop() formItem!: any

    formData: any = {
        key: this.formItem.key,
        name: this.formItem.name,
        label: this.formItem.label,
        cols: this.formItem.cols
    }

    get form() {
        if (this.formItem) {
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
                            fields: [
                                {
                                    key: "name",
                                    name: "name",
                                    label: "标识",
                                    type: TzSuperFormType.Input,
                                    title: "标识",
                                    isOnlyDisplay: false,
                                    format: null,
                                    options: null,
                                    cols: 3,
                                    attrs: null,
                                    slots: null,
                                },
                                {
                                    key: "label",
                                    name: "label",
                                    label: "标签",
                                    type: TzSuperFormType.Input,
                                    title: "标签",
                                    isOnlyDisplay: false,
                                    format: null,
                                    options: null,
                                    cols: 3,
                                    attrs: null,
                                    slots: null,
                                },
                                {
                                    key: "cols",
                                    name: "cols",
                                    label: "所占宽度",
                                    type: TzSuperFormType.Select,
                                    title: "所占宽度",
                                    isOnlyDisplay: false,
                                    format: null,
                                    options: [
                                        { text: "1列", value: 1 },
                                        { text: "2列", value: 2 },
                                        { text: "3列", value: 3 }
                                    ],
                                    cols: 3,
                                    attrs: null,
                                    slots: null,
                                }
                            ]
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
        if (this.formItem) {
            this.formData = {
                key: this.formItem.key,
                name: this.formItem.name,
                label: this.formItem.label,
                cols: this.formItem.cols
            }
        } else {
            this.formData = {}
        }
    }
}