import { TzSuperFormType } from "../TzSuperForm/schema/TzSuperFormSchema";

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
                key: '',
                name: '',
                label: '静态文本',
                type: TzSuperFormType.Text,
                title: '静态文本',
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
                cols: 1,
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
                key: '',
                name: '',
                label: '开关',
                type: TzSuperFormType.Switch,
                title: '开关',
                format: null,
                options: null,
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
                key: '',
                name: '',
                label: '弹框',
                type: TzSuperFormType.Dialog,
                title: '弹框',
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: false,
            },
            {
                key: '',
                name: '',
                label: '上传',
                type: 'upload',
                title: '上传',
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: false,
            },
            {
                key: '',
                name: '',
                label: '动态列表',
                type: 'grid',
                title: '动态列表',
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: false,
            },
            {
                key: '',
                name: '',
                label: '超级外壳',
                type: 'shell',
                title: '超级外壳',
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
                isfinished: false,
                tip: '可以组装任何组件'
            },
        ]
    },    
    {
        title: '业务组件',
        comps: [
        ]
    },
]
