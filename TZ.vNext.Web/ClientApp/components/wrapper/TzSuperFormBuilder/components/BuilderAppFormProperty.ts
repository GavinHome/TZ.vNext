import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormType } from "../../TzSuperForm/TzSuperFormSchema";

import 'element-ui/lib/theme-chalk/index.css'
// import { Form } from 'element-ui'
// Vue.use(Form)

@Component({
    props: [],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html")
    }
})
export default class BuilderAppFormProperty extends Vue {
    activeTab: number = 0

    form: any = [
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
                            key: "labelPosition",
                            name: "labelPosition",
                            label: "标签位置：",
                            type: TzSuperFormType.Select,
                            title: "标签位置",
                            isOnlyDisplay: false,
                            format: null,
                            options: [{ text: 'left', value: 'left' }, { text: 'right', value: 'right' }, { text: 'top', value: 'top' }],
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "span",
                            name: "span",
                            label: "表单宽度：",
                            type: TzSuperFormType.Input,
                            title: "表单宽度",
                            isOnlyDisplay: false,
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "isHideSubmitBtn",
                            name: "isHideSubmitBtn",
                            label: "提交按钮：",
                            type: TzSuperFormType.Select,
                            title: "提交按钮",
                            isOnlyDisplay: false,
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
                            label: "返回按钮：",
                            type: TzSuperFormType.Select,
                            title: "返回按钮",
                            isOnlyDisplay: false,
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
                            label: "提交按钮文字：",
                            type: TzSuperFormType.Input,
                            title: "提交按钮文字",
                            isOnlyDisplay: false,
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "backBtnText",
                            name: "backBtnText",
                            label: "返回按钮文字：",
                            type: TzSuperFormType.Input,
                            title: "返回按钮文字",
                            isOnlyDisplay: false,
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                    ]
                }
            ]
        }
    ]

    defaultData: any = {
        isHideSubmitBtn: false,
        isHideBackBtn: false,
        submitBtnText: '提交',
        backBtnText: '返回',
        labelWidth: 100,
        labelPosition: 'top',
        span: null
    }

    formData: any = {
        isHideSubmitBtn: false,
        isHideBackBtn: false,
        submitBtnText: '提交',
        backBtnText: '返回',
        labelWidth: 100,
        labelPosition: 'top',
        span: '120px'
    }
}