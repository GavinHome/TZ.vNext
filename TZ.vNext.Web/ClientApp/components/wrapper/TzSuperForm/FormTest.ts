import Vue from "vue";
import { Component } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormType, getFormDesc, TzSuperOptionSchema } from "./schema/TzSuperFormSchema";
import 'element-ui/lib/theme-chalk/index.css'
import ElementUI from 'element-ui'
import { FieldTypeEnum, EnumHelper, EnumConstType } from "../../common/Enums";
Vue.use(ElementUI)

@Component({
    props: [],
    components: {
        TzSuperForm: require("./index.vue.html")
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
                                    component: null,
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
                            slots: null
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
                        },
                        {
                            key: "tag",
                            name: "tag",
                            label: "标签",
                            type: TzSuperFormType.Tag,
                            title: "标签",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "autocomplete",
                            name: "autocomplete",
                            label: "建议输入框",
                            type: TzSuperFormType.Autocomplete,
                            title: "建议输入框",
                            format: null,
                            //options: null,
                            // options: [
                            //     //local = { value: "value or label", ext: "code or tip", key:"id"}
                            //     { value: "三全鲜食（北新泾店）", ext: "长宁区新渔路144号", key:"1" },
                            //     { value: "Hot honey 首尔炸鸡（仙霞路）", ext: "上海市长宁区淞虹路661号", key:"2" },
                            //     { value: "新旺角茶餐厅", ext: "上海市普陀区真北路988号创邑金沙谷6号楼113", key:"3" },
                            //     { value: "泷千家(天山西路店)", ext: "天山西路438号", key:"4" },
                            //     { value: "胖仙女纸杯蛋糕（上海凌空店）", ext: "上海市长宁区金钟路968号1幢18号楼一层商铺18-101", key:"5" },
                            //     { value: "贡茶", ext: "上海市长宁区金钟路633号", key:"6" },
                            //     { value: "豪大大香鸡排超级奶爸", ext: "上海市嘉定区曹安公路曹安路1685号", key:"7" },
                            //     { value: "茶芝兰（奶茶，手抓饼）", ext: "上海市普陀区同普路1435号", key:"8" },
                            //     { value: "十二泷町", ext: "上海市北翟路1444弄81号B幢-107", key:"9" },
                            //     { value: "星移浓缩咖啡", ext: "上海市嘉定区新郁路817号", key:"10" },
                            //     { value: "阿姨奶茶/豪大大", ext: "嘉定区曹安路1611号", key:"11" },
                            //     { value: "新麦甜四季甜品炸鸡", ext: "嘉定区曹安公路2383弄55号", key:"12" },
                            //     { value: "Monica摩托主题咖啡店", ext: "嘉定区江桥镇曹安公路2409号1F，2383弄62号1F", key:"13" },
                            // ],
                            // options: [
                            //     { value: "三全鲜食（北新泾店）", key:"1" },
                            //     { value: "Hot honey 首尔炸鸡（仙霞路）", key:"2" },
                            //     { value: "新旺角茶餐厅", key:"3" },
                            //     { value: "泷千家(天山西路店)", key:"4" },
                            //     { value: "胖仙女纸杯蛋糕（上海凌空店）", key:"5" },
                            //     { value: "贡茶", key:"6" },
                            //     { value: "豪大大香鸡排超级奶爸", key:"7" },
                            //     { value: "茶芝兰（奶茶，手抓饼）", key:"8" },
                            //     { value: "十二泷町", key:"9" },
                            //     { value: "星移浓缩咖啡", key:"10" },
                            //     { value: "阿姨奶茶/豪大大", key:"11" },
                            //     { value: "新麦甜四季甜品炸鸡", key:"12" },
                            //     { value: "Monica摩托主题咖啡店", key:"13" },
                            // ],
                            on: {
                                select: (data: TzSuperOptionSchema) => this.autocompleteChange(data)
                            },
                            options: {
                                remote: "/api/Employees/GridQueryEmployees",
                                schema: {
                                    Id: { editable: false, nullable: true, filterable: false, type: FieldTypeEnum.String },
                                    Name: { editable: false, nullable: true, filterable: true, type: FieldTypeEnum.String },
                                    Code: { editable: false, nullable: true, filterable: true, type: FieldTypeEnum.String },
                                    CompanyTypeName: { editable: false, nullable: true, filterable: true, type: FieldTypeEnum.String },
                                    OrganizationName: { editable: false, nullable: true, filterable: true, type: FieldTypeEnum.String }
                                },
                                map: {
                                    value: "Name",
                                    ext: "Code",
                                    key: "Id"
                                }
                            },
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
                            key: "radio",
                            name: "radio",
                            label: "单选",
                            type: TzSuperFormType.Radio,
                            title: "单选",
                            format: null,
                            options: [
                                { text: '男', value: 0 },
                                { text: '女', value: 1 },
                                { text: '未知', value: 2, attrs: { disabled: true } }
                            ],
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "checkbox",
                            name: "checkbox",
                            label: "多选",
                            type: TzSuperFormType.Checkbox,
                            title: "多选",
                            format: null,
                            options: [
                                { text: '男', value: 0 },
                                { text: '女', value: 1 },
                                { text: '未知', value: 2, attrs: { disabled: true } }
                            ],
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                    ]
                },
                {
                    key: "basic-row5",
                    name: "basic-row5",
                    fields: [
                        {
                            key: "rate",
                            name: "rate",
                            label: "打分",
                            type: TzSuperFormType.Rate,
                            title: "打分",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "rate_value",
                            name: "rate_value",
                            label: "评分",
                            type: TzSuperFormType.Rate,
                            title: "评分",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: {
                                disabled: true,
                                "show-score": true
                            },
                            slots: null
                        }
                    ]
                },
                {
                    key: "basic-row6",
                    name: "basic-row6",
                    fields: [
                        {
                            key: "slider",
                            name: "slider",
                            label: "滑块",
                            type: TzSuperFormType.Slider,
                            title: "滑块",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "password",
                            name: "password",
                            label: "密码",
                            type: TzSuperFormType.Password,
                            title: "密码",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                    ]
                },

                {
                    key: "basic-row7",
                    name: "basic-row7",
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
                                    component: null,
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
            key: "dateandtimeandrange",
            name: "dateandtimeandrange",
            title: "日期时间及范围",
            isCollapsed: false,
            rows: [
                {
                    key: "dateandtimeandrange-row1",
                    name: "dateandtimeandrange-row1",
                    fields: [
                        {
                            key: "year",
                            name: "year",
                            label: "年份",
                            type: TzSuperFormType.Year,
                            title: "年份",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "month",
                            name: "month",
                            label: "月份",
                            type: TzSuperFormType.Month,
                            title: "月份",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "date",
                            name: "date",
                            label: "日期",
                            type: TzSuperFormType.Date,
                            title: "日期",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                    ]
                },
                {
                    key: "dateandtimeandrange-row2",
                    name: "dateandtimeandrange-row2",
                    fields: [
                        {
                            key: "dates",
                            name: "dates",
                            label: "多个日期",
                            type: TzSuperFormType.Dates,
                            title: "多个日期",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "datetime",
                            name: "datetime",
                            label: "日期时间",
                            type: TzSuperFormType.DateTime,
                            title: "日期时间",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "time",
                            name: "time",
                            label: "时间",
                            type: TzSuperFormType.Time,
                            title: "时间",
                            format: null,
                            options: {
                                start: '08:00',
                                step: '02:00',
                                end: '19:00'
                            },
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                    ]
                },
                {
                    key: "dateandtimeandrange-row3",
                    name: "dateandtimeandrange-row3",
                    fields: [
                        {
                            key: "daterange",
                            name: "daterange",
                            label: "日期范围",
                            type: TzSuperFormType.DateRange,
                            title: "日期范围",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "datetimerange",
                            name: "datetimerange",
                            label: "时间和日期范围",
                            type: TzSuperFormType.DateTimeRange,
                            title: "时间和日期范围",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                        {
                            key: "timerange",
                            name: "timerange",
                            label: "时间范围",
                            type: TzSuperFormType.TimeRange,
                            title: "时间范围",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        },
                    ]
                }, {
                    key: "dateandtimeandrange-row4",
                    name: "dateandtimeandrange-row4",
                    fields: [
                        {
                            key: "week",
                            name: "week",
                            label: "周",
                            type: TzSuperFormType.Week,
                            title: "周",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: null,
                            slots: null
                        }, 
                        // {
                        //     key: "monthrange",
                        //     name: "monthrange",
                        //     label: "月份范围",
                        //     type: TzSuperFormType.MonthRange,
                        //     title: "月份范围",
                        //     format: null,
                        //     options: null,
                        //     cols: 1,
                        //     attrs: null,
                        //     slots: null
                        // },
                    ]
                }
            ]
        },
        {
            key: "grid",
            name: "grid",
            title: "动态列表",
            isCollapsed: false,
            rows: [
                {
                    key: "grid-row1",
                    name: "grid-row",
                    fields: [
                        {
                            key: "dynamic-grid",
                            name: "dynamic-grid",
                            label: "薪酬项",
                            type: TzSuperFormType.Grid,
                            title: "薪酬项",
                            format: null,
                            options: {
                                remote: "/api/Employees/GridQueryEmployees",
                                schema: [
                                    {
                                        field: "RowNumber",
                                        title: "序号",
                                        width: "8%",
                                        filterable: false,
                                        sortable: false,
                                        editable: false,
                                        menu: false,
                                        type: FieldTypeEnum.Number,
                                        index: 0
                                    },
                                    {
                                        field: "Name",
                                        title: "名称",
                                        filterable: true,
                                        sortable: true,
                                        editable: false,
                                        menu: true,
                                        type: FieldTypeEnum.String,
                                        width: "20%",
                                        index: 1
                                    },
                                    {
                                        field: "Code",
                                        title: "编号",
                                        filterable: true,
                                        sortable: true,
                                        editable: false,
                                        menu: true,
                                        type: FieldTypeEnum.String,
                                        width: "10%",
                                        index: 2
                                    },
                                    {
                                        field: "CompanyTypeName",
                                        title: "所属公司",
                                        filterable: true,
                                        sortable: true,
                                        editable: false,
                                        menu: true,
                                        type: FieldTypeEnum.String,
                                        width: "30%",
                                        index: 3
                                    },
                                    {
                                        field: "OrganizationName",
                                        title: "所属机构",
                                        filterable: true,
                                        sortable: true,
                                        editable: false,
                                        menu: true,
                                        type: FieldTypeEnum.String,
                                        width: "30%",
                                        index: 4
                                    }
                                ]
                            },
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                    ]
                },
            ]
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
                            label: "内容",
                            type: TzSuperFormType.Textarea,
                            title: "内容",
                            format: null,
                            options: null,
                            cols: 3,
                            attrs: null,
                            slots: null,
                        },
                    ]
                },
                {
                    key: "remark-row2",
                    name: "remark-row2",
                    fields: [
                        {
                            key: "button",
                            name: "button",
                            label: "按钮",
                            type: TzSuperFormType.Button,
                            title: "提交",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: {
                                type: 'primary'
                            },
                            slots: null,
                            class: "",
                            on: {
                                click: () => {
                                }
                            }
                        }, 
                        {
                            key: "button1",
                            name: "button1",
                            label: "按钮",
                            type: TzSuperFormType.Button,
                            title: "提交",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: {
                                type: 'success'
                            },
                            slots: null,
                            class: "",
                            on: {
                                click: () => {
                                }
                            }
                        },
                        {
                            key: "button2",
                            name: "button2",
                            label: "按钮",
                            type: TzSuperFormType.Button,
                            title: "提交",
                            format: null,
                            options: null,
                            cols: 1,
                            attrs: {
                                type: 'info'
                            },
                            slots: null,
                            class: "",
                            on: {
                                click: () => {
                                }
                            }
                        },
                    ]
                }
            ],
        },
    ]

    formData: any = {
        title: null,
        content: null,
        count: 100,
        number: 0.5,
        user: null, //"杨晓民;纪伟伟",
        userId: null,
        total: 50,
        finished: 1,
        isMarried: true,
        shell: ["中国", "陕西", "西安"],
        time: '',
        tag: ['标签1', '标签2'],
        radio: 1,
        checkbox: [],
        date: '2019-08-22',
        datetime: '2019-08-22 16:12:55',
        rate: 0,
        rate_value: 3.7,
        slider: 50,
        password: "111111",
        year: '2019',
        month: '2019-08',
        daterange: '',
        monthrange: '',
        datetimerange: '',
        dates: '2019-08-22',
        week: '',
        timerange: '',
        autocomplete: '',
        autocompleteId: '',
    }

    isLoading: boolean = false

    titleChange(e: any) {
        console.log(e)
    }
    
    autocompleteChange(data: TzSuperOptionSchema) {
        this.formData.autocompleteId = data.key
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

        if (field) {
            field.slots[0].default = value.map(x => {
                return { Id: x.Id, Name: x.Name }
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
        // content: [
        //     { required: true, message: "请输入内容", trigger: 'blur' }
        // ],
        // finished: [
        //     { required: true, message: "请选择是否完成", trigger: 'blur' }
        // ],
        // user: [
        //     { required: true, message: "请选择申请人", trigger: 'blur' }
        // ],
        // time: [
        //     { required: true, message: "请选择时间", trigger: 'blur' }
        // ]
    }

    handleSubmit(data) {
        console.log("submit：" + data)
        this.$message.success(JSON.stringify(data))
        return Promise.resolve(data)
    }

    handleSuccess(response) {
        console.log("success" + response)
        this.$message.success('创建成功')
    }

    handleError(response) {
        console.log("error" + response)
        this.$message.success('创建失败')
    }

    handleEnd(response) {
        console.log("end: " + response)
        this.$message.success('处理结束')
    }

    handleRequest(response) {
        this.isLoading = true;
        console.log("handleRequest" + response)
        this.$message.success('自定义处理')
    }
}