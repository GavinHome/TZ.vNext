import Vue from "vue";
import { Component, Watch } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormType } from "../../TzSuperForm/schema/TzSuperFormSchema";

@Component({
    props: [],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html")
    }
})
export default class BuilderAppFormProperty extends Vue {
    activeTab: number = 0

    form: TzSuperFormGroup[] = [
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
                        // {
                        //     key: "labelWidth",
                        //     name: "labelWidth",
                        //     label: "表单宽度：",
                        //     type: TzSuperFormType.Input,
                        //     title: "表单宽度",
                        //     isOnlyDisplay: false,
                        //     format: null,
                        //     options: null,
                        //     cols: 3,
                        //     attrs: null,
                        //     slots: null,
                        // },
                        {
                            key: "isHideSubmitBtn",
                            name: "isHideSubmitBtn",
                            label: "提交按钮",
                            type: TzSuperFormType.Select,
                            title: "提交按钮",
                            format: null,
                            options: [
                                { text: '显示', value: false },
                                { text: '隐藏', value: true }
                            ],
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "isHideBackBtn",
                            name: "isHideBackBtn",
                            label: "返回按钮",
                            type: TzSuperFormType.Select,
                            title: "返回按钮",
                            format: null,
                            options: [
                                { text: '显示', value: false },
                                { text: '隐藏', value: true }
                            ],
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "submitBtnText",
                            name: "submitBtnText",
                            label: "提交按钮文字",
                            type: TzSuperFormType.Input,
                            title: "提交按钮文字",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "backBtnText",
                            name: "backBtnText",
                            label: "返回按钮文字",
                            type: TzSuperFormType.Input,
                            title: "返回按钮文字",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "isAutoHandlePost",
                            name: "isAutoHandlePost",
                            label: "是否默认处理请求",
                            type: TzSuperFormType.Switch,
                            title: "是否默认处理请求",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "action",
                            name: "action",
                            label: "提交地址",
                            type: TzSuperFormType.Input,
                            title: "提交地址",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "isCustomHandleRequest",
                            name: "isCustomHandleRequest",
                            label: "是否自定义请求",
                            type: TzSuperFormType.Switch,
                            title: "是否自定义请求",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "single",
                            name: "single",
                            label: "单组",
                            type: TzSuperFormType.Switch,
                            title: "单组",
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

    formData: any = {
        labelWidth: '120px',
        isHideSubmitBtn: false,
        isHideBackBtn: false,
        submitBtnText: '提交',
        backBtnText: '返回',
        isCustomHandleRequest: false,
        isAutoHandlePost: true,
        action: '/api/SuperForm/TestSuperFormSave',
        single: false
    }

    @Watch('formData', { immediate: true, deep: true })
    onFormDataChanged(val: string, oldVal: string) {
        this.$emit("formPropertyChange", val)
    }
}