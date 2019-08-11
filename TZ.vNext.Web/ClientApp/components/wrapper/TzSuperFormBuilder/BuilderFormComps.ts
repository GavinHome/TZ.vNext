const options = [
    { text: '选项1', value: 1 },
    { text: '选项2', value: 2 },
    { text: '选项3', value: 3 }
]

export const components = [
    {
        title: '内置组件',
        comps: [
            {
                type: 'text',
                label: '静态文本',
                default: '我是一段静态文本',
                defaultType: 'input',
                isfinished: false,
            },
            {
                type: 'input',
                label: '单行输入框',
                isHideOptions: true,
                defaultType: 'input',
                isfinished: true,
            },
            {
                type: 'textarea',
                label: '多行输入框',
                isHideOptions: true,
                defaultType: 'input',
                isfinished: true,
            },
            {
                type: 'number',
                label: '数字',
                isHideOptions: true,
                isHideSlots: true,
                defaultType: 'number',
                isfinished: true,
            },
            {
                type: 'select',
                label: '选择器',
                options: options,
                defaultType: 'input',
                isfinished: true,
            },
            {
                type: 'autocomplete',
                label: '带建议的输入框',
                isHideOptions: true,
                defaultType: 'input',
                isfinished: false,
            },
            {
                type: 'date',
                label: '日期',
                isHideOptions: true,
                defaultType: 'date',
                isfinished: false,
            },
            {
                type: 'daterange',
                label: '日期范围',
                isHideOptions: true,
                defaultType: 'daterange',
                isfinished: false,
            },
            {
                type: 'datetime',
                label: '时间和日期',
                isHideOptions: true,
                defaultType: 'datetime',
                isfinished: false,
            },
            {
                type: 'datetimerange',
                label: '日期和时间范围',
                isHideOptions: true,
                defaultType: 'datetimerange',
                isfinished: false,
            },
            {
                type: 'time',
                label: '时间',
                isHideOptions: true,
                defaultType: 'time',
                isfinished: false,
            },
        ]
    },
    {
        title: '业务组件',
        comps: [
            {
                type: 'employee-grid',
                label: '人员组件'
            },
        ]
    },
    {
        title: '扩展组件',
        comps: [
        ]
    }
]
