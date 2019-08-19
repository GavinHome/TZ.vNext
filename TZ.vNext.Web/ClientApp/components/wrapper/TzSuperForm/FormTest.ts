import Vue from "vue";
import { Component } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormType, getFormDesc } from "./schema/TzSuperFormSchema";
import TzEmployee from "../TzEmployee";
import TzAreaCascader from "../TzAreaCascader";


@Component({
    props: [],
    components: {
        TzAreaCascader: require("../TzAreaCascader.vue.html"),
        TzSuperForm: require("./index.vue.html"),
        TzEmployee: require("../TzEmployee.vue.html")
    },
    watch: {
        formData: {
            handler: (newProp, oldProp) => {
                newProp['total'] = newProp['count'] * newProp['number']
            },
            deep: true,
            immediate: false
        }
    }
})
export default class FormTest extends Vue {
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
                        {
                            key: "title",
                            name: "title",
                            label: "标题",
                            type: TzSuperFormType.Input,
                            title: "标题",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: {
                                placeholder: "请输入标题"
                            },
                            on: {
                                change: e => this.titleChange(e)
                            },
                            slots: null,
                        },
                        {
                            key: "count",
                            name: "count",
                            label: "数量",
                            type: TzSuperFormType.Number,
                            title: "数量",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null,
                            class: "",
                            style: "",
                        },
                        {
                            key: "number",
                            name: "number",
                            label: "人数",
                            type: TzSuperFormType.Number,
                            title: "人数",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null,
                        }
                    ]
                },
                {
                    key: "basic-row2",
                    name: "basic-row2",
                    fields: [
                        {
                            key: "finished",
                            name: "finished",
                            label: "是否完成",
                            type: TzSuperFormType.Select,
                            title: "是否完成",
                            format: null,
                            options: [
                                { text: '是', value: 1 },
                                { text: '否', value: 0 }
                            ],
                            cols: 1,
                            attrs: null,
                            slots: null,
                        },
                        {
                            key: "user",
                            name: "user",
                            label: "申请人",
                            type: TzSuperFormType.Dialog,
                            title: "申请人",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: [
                                {
                                    type: "tz-employee",
                                    component: TzEmployee,
                                    props: {
                                        multiply: true,
                                    },
                                    default: [],
                                    // [
                                    //     // { Id: null, Name: null }, 
                                    //     // { Id: "f9eb6793-649e-4745-b24d-a37300b26ff4", Name: "杨晓民" }, 
                                    //     // { Id: "e6bceb24-46d4-428d-9832-a7b600eaba95", Name: "纪伟伟" }
                                    // ],
                                    submit: data => this.selectEmployee("user", data),
                                }
                            ]
                        },
                        {
                            key: "total",
                            name: "total",
                            label: "总计",
                            type: TzSuperFormType.Text,
                            title: "总计",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: [
                                {
                                    type: "tz-employee",
                                    component: TzEmployee,
                                    props: {
                                        multiply: true,
                                    }
                                }
                            ]
                        }
                    ]
                },
                {
                    key: "basic-row3",
                    name: "basic-row3",
                    fields: [
                        {
                            key: "isMarried",
                            name: "isMarried",
                            label: "婚否",
                            type: TzSuperFormType.Switch,
                            title: "婚否",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        }
                    ]
                },
                {
                    key: "basic-row4",
                    name: "basic-row4",
                    fields: [
                        {
                            key: "shell",
                            name: "shell",
                            label: "超级外壳",
                            type: TzSuperFormType.Shell,
                            title: "超级外壳",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: [
                                {
                                    type: "tz-area-cascader",
                                    component: TzAreaCascader,
                                    props: {},
                                    default: ["中国", "陕西", "西安"],
                                    on: {
                                        change: data => this.changeShell(data)
                                    }
                                }
                            ]
                        }
                    ]
                }
            ],
        },
        {
            key: "remark",
            name: "remark",
            title: "备注信息",
            isCollapsed: false,
            rows: [
                {
                    key: "remark-row1",
                    name: "remark-row1",
                    fields: [
                        {
                            key: "content",
                            name: "content",
                            label: "内容：",
                            type: TzSuperFormType.Textarea,
                            title: "内容",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                    ]
                }
            ],
        }
    ]

    formData: any = {
        title: null,
        content: null,
        count: 100,
        number: 0.5,
        user: null, //"杨晓民;纪伟伟",
        userId: null,
        total: 50,
        isMarried: true,
        shell: ["中国", "陕西", "西安"]
    }

    titleChange(e: any) {
        console.log(e)
    }

    changeShell(data: any) {
        Vue.set(this.formData, "shell", data)
    }

    selectEmployee(name, value) {
        if (value && value.length) {
            Vue.set(this.formData, name, value.map(x => x.Name).join("；"))
            Vue.set(this.formData, name + "Id", value.map(x => x.Id).join("；"))
        } else {
            Vue.set(this.formData, name, value.Name)
            Vue.set(this.formData, name + "Id", value.Id)
        }

        ////修改默认值
        var fields = getFormDesc(this.form)
        var field = fields.filter(x => x.key === name)[0]

        if(field) {
            field.slots[0].default = value.map(x => {
                return { Id: x.Id, Name: x.Name}
            })
        }
    }

    // @Watch('formData', { immediate: true, deep: true })
    // onFormDataChanged(val: any, oldVal: any) {
    //     //this.formData['total'] = this.formData['count'] * 10
    // }

    rules = {
        title: [
            { required: true, message: "请输入标题", trigger: 'blur' }
        ],
        content: [
            { required: true, message: "请输入内容", trigger: 'blur' }
        ],
        finished: [
            { required: true, message: "请选择是否完成", trigger: 'blur' }
        ],
        user: [
            { required: true, message: "请选择申请人", trigger: 'blur' }
        ]
    }

    handleSubmit(data) {
        console.log(data)
        return Promise.resolve(data)
    }

    handleSuccess(response) {
        console.log(response)
        this.$message.success('创建成功')
    }

    handleError(response) {
        console.log(response)
        this.$message.success('失败')
    }

    handleEnd(response) {
        console.log(response)
        this.$message.success('处理结束')
    }

    handleRequest(response) {
        console.log(response)
        this.$message.success('自定义处理')
    }
}