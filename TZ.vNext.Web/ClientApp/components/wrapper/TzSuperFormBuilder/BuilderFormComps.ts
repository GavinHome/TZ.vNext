import { TzSuperFormType } from "../TzSuperForm/TzSuperFormSchema";

const options = [
    { text: '选项1', value: 1 },
    { text: '选项2', value: 2 },
    { text: '选项3', value: 3 }
]

export interface TzSuperFormField {
    key: string;
    name: string;
    label: string;
    type: TzSuperFormType;
    title: string;
    format?: string | undefined | null;
    options?: any;
    cols?: number; //当前元素占用列个数，默认1
    attrs?: any;
    slots?: any;
}

export const components = [
    {
        title: '内置组件',
        comps: [
            {
                type: 'text',
                label: '静态文本',
                isfinished: true,
            },
            {
                key: '',
                name: '',
                label: '单行输入框',
                type: TzSuperFormType.Input,
                title: '单行输入框',
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: true,
            },
            {
                key: '',
                name: '',
                label: '多行输入框',
                type: TzSuperFormType.Textarea,
                title: '多行输入框',
                format: null,
                options: null,
                cols: 3,
                attrs: null,
                slots: null,
                isfinished: true,
            },
            {
                key: '',
                name: '',
                label: '数字',
                type: TzSuperFormType.Number,
                title: '数字',
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: true,
            },
            {
                key: '',
                name: '',
                label: '选择器',
                type: TzSuperFormType.Select,
                title: '选择器',
                format: null,
                options: options,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: true,
            },
            {
                type: 'autocomplete',
                label: '带建议的输入框',
                isfinished: false,
            },
            {
                type: 'date',
                label: '日期',
                isfinished: false,
            },
            {
                type: 'daterange',
                label: '日期范围',
                isfinished: false,
            },
            {
                type: 'datetime',
                label: '时间和日期',
                isfinished: false,
            },
            {
                type: 'datetimerange',
                label: '日期和时间范围',
                isfinished: false,
            },
            {
                type: 'time',
                label: '时间',
                isfinished: false,
            },
        ]
    },
    {
        title: '扩展组件',
        comps: [
            {
                type: 'dialog',
                label: '弹框',
                isfinished: false,
            },
        ]
    },    
    {
        title: '业务组件',
        comps: [
        ]
    },
]
