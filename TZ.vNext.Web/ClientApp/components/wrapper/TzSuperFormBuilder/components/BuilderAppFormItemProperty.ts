import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperFormType } from "../../TzSuperForm/schema/TzSuperFormSchema";

import 'element-ui/lib/theme-chalk/index.css'
import { Dialog } from 'element-ui'
Vue.use(Dialog)

import BuilderAppFormOptionsSet from "./BuilderAppFormOptionsSet"
Vue.component("app-form-options-set", BuilderAppFormOptionsSet)
import BuilderAppFormGridOptionsSet from "./BuilderAppFormGridOptionsSet"
Vue.component("app-form-grid-options-set", BuilderAppFormGridOptionsSet)

@Component({
    props: ["formItem", "rules"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        AppFormOptionsSet: require("./BuilderAppFormOptionsSet.vue.html"),
        AppFormGridOptionsSet: require("./BuilderAppFormGridOptionsSet.vue.html")
    }
})
export default class BuilderAppFormProperty extends Vue {
    @Prop() formItem!: any
    @Prop() rules!: any

    formData: any = {
        key: this.formItem.key,
        name: this.formItem.name,
        label: this.formItem.label,
        cols: this.formItem.cols,
        required: false,
        options: this.formItem.options
    }

    canSetDataSource: boolean = false
    canSetGridDataSource: boolean = false

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
                                        maxlength: 20
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
                                        maxlength: 20
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

            if (this.formItem.type === TzSuperFormType.Autocomplete) {
                result[0].rows[0].fields.push({
                    key: "setOptionDataSource",
                    name: "setOptionDataSource",
                    label: "数据来源",
                    type: TzSuperFormType.Button,
                    title: "数据来源",
                    format: null,
                    options: null,
                    cols: 3,
                    attrs: null,
                    slots: null,
                    on: {
                        click: (e) => this.canSetDataSource = true
                    }
                })
            }

            if (this.formItem.type === TzSuperFormType.Grid) {
                result[0].rows[0].fields.push({
                    key: "setOptionDataSource",
                    name: "setOptionDataSource",
                    label: "数据来源",
                    type: TzSuperFormType.Button,
                    title: "数据来源",
                    format: null,
                    options: null,
                    cols: 3,
                    attrs: null,
                    slots: null,
                    on: {
                        click: (e) => this.canSetGridDataSource = true
                    }
                })
            }

            return result;
        }

        return []
    }

    handleOptionsSet(data) {
        this.formData.options = data
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
                cols: this.formItem.cols,
                required: this.rules[this.formItem.name] ? true : false,
                options: this.formItem.options
            }
        } else {
            this.formData = {}
        }
    }
}