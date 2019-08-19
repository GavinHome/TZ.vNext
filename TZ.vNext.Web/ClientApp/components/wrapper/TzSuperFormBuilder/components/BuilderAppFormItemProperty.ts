import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperFormType } from "../../TzSuperForm/schema/TzSuperFormSchema";

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
        cols: this.formItem.cols,
        required: false
    }

    get form() {
        if (this.formItem && this.formItem.key) {
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
                                    format: null,
                                    options: null,
                                    cols: 3,
                                    attrs: {
                                        maxlength:20
                                    },
                                    slots: null,
                                },
                                {
                                    key: "label",
                                    name: "label",
                                    label: "标签",
                                    type: TzSuperFormType.Input,
                                    title: "标签",
                                    format: null,
                                    options: null,
                                    cols: 3,
                                    attrs: {
                                        maxlength:20
                                    },
                                    slots: null,
                                },
                                {
                                    key: "cols",
                                    name: "cols",
                                    label: "所占宽度",
                                    type: TzSuperFormType.Select,
                                    title: "所占宽度",
                                    format: null,
                                    options: [
                                        { text: "1列", value: 1 },
                                        { text: "2列", value: 2 },
                                        { text: "3列", value: 3 }
                                    ],
                                    cols: 3,
                                    attrs: null,
                                    slots: null,
                                },
                                {
                                    key: "required",
                                    name: "required",
                                    label: "是否必填",
                                    type: TzSuperFormType.Switch,
                                    title: "是否必填",
                                    format: null,
                                    options: null,
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
    onFormDataChanged(val: any, oldVal: any) {
        this.$emit("formItemPropertyChange", val, oldVal)
    }

    @Watch('formItem', { immediate: true, deep: true })
    onFormItemaChanged(val: any, oldVal: any) {
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