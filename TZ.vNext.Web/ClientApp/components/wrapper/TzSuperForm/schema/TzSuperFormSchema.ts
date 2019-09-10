import { GridColumnSchema } from "../../../schemas/GridColumnSchema";

export interface TzSuperFormGroup {
    key: string;
    name: string;
    title: string;
    isCollapsed: boolean;
    rows: TzSuperFormRow[];
}

export interface TzSuperFormRow {
    key: string;
    name: string;
    fields: TzSuperFormField[]
}

export interface TzSuperFormField {
    key: string;
    name: string;
    label: string;
    type: TzSuperFormType | string;
    title: string;
    format?: string | undefined | null;
    options?: any;
    cols?: number; //当前元素占用列个数，默认1
    attrs?: any;
    slots?: any;
    class?: string;
    style?: string;
    on?: any;
}

export enum TzSuperFormType {
    /// <summary>
    /// 静态文本
    /// </summary>
    Text = "text",
    /// <summary>
    /// 单行文本
    /// </summary>
    Input = "input",
    /// <summary>
    /// 多行文本
    /// </summary>
    Textarea = "textarea",
    /// <summary>
    /// 数字
    /// </summary>
    Number = "number",
    /// <summary>
    /// 选择器
    /// </summary>
    Select = "select",
    /// <summary>
    /// 弹框数据
    /// </summary>
    Dialog = "dialog",
    /// <summary>
    /// 弹框数据
    /// </summary>
    Switch = "switch",
    /// <summary>
    /// 超级外壳
    /// </summary>
    Shell = "shell",
    /// <summary>
    /// 时间
    /// </summary>
    Time = "time",
    /// <summary>
    /// 标签
    /// </summary>
    Tag = "tag",
    /// <summary>
    /// 单选
    /// </summary>
    Radio = "radio",
    /// <summary>
    /// 多选
    /// </summary>
    Checkbox = "checkbox",
    /// <summary>
    /// 日期
    /// </summary>
    Date = "date",
    /// <summary>
    /// 日期时间
    /// </summary>
    DateTime = "datetime",
    /// <summary>
    /// 评分
    /// </summary>
    Rate = "rate",
    /// <summary>
    /// 滑块
    /// </summary>
    Slider = "slider",
    /// <summary>
    /// 密码
    /// </summary>
    Password = "password",
    /// <summary>
    /// 密码
    /// </summary>
    Year = "year",
    /// <summary>
    /// 密码
    /// </summary>
    Month = "month",
    /// <summary>
    /// 密码
    /// </summary>
    DateRange = "daterange",
    /// <summary>
    /// 密码
    /// </summary>
    MonthRange = "monthrange",
    /// <summary>
    /// 密码
    /// </summary>
    DateTimeRange = "datetimerange",
    /// <summary>
    /// 多个日期
    /// </summary>
    Dates = "dates",
    /// <summary>
    /// 多个日期
    /// </summary>
    Week = "week",
    /// <summary>
    /// 时间范围
    /// </summary>
    TimeRange = "timerange",
    /// <summary>
    /// 按钮
    /// </summary>
    Button = "button",
    /// <summary>
    /// 建议输入框
    /// </summary>
    Autocomplete = "autocomplete",
    /// <summary>
    /// 级联选择
    /// </summary>
    Cascader = "cascader",
    /// <summary>
    /// 动态列表
    /// </summary>
    Grid = "grid"
}

export function getComponentName(type: TzSuperFormType) {
    let builtInNames: string[] = ["input", "textarea", "number", "select", "dialog", "text", "switch", "shell", "time", "tag", "radio", "checkbox", "date", "datetime", "rate", "slider", "password", "year", "month", "daterange", "monthrange", "datetimerange", "dates", "week", "timerange", "button", "autocomplete", "grid"];
    if (builtInNames.includes(type)) {
        // tz 内置组件
        return 'tz-super-' + type
    } else {
        // 外部组件
        return type
    }
}

export function getFormDesc(form: TzSuperFormGroup[]) {
    let fields: TzSuperFormField[] = [];
    form.forEach((f, i) => {
        f.rows.forEach((r, j) => {
            fields = fields.concat(r.fields)
        })
    })

    return fields;
}

export interface TzSuperOptionSchema {
    value: string;
    ext?: string;
    key: string;
}

export interface TzSuperGridOptionSchema {
    remote: string;
    schema_meta_url: string;
    schema: GridColumnSchema[];
    schema_meta_key: string
    map?: any;
}

export interface TzSuperDataSourceSchema {
    key: string;
    value: string;
    url?: string;
    metaUrl: string;
}

export interface TzSuperFormAttrSchema {
    name: string,
    labelWidth: string,
    isHideSubmitBtn: boolean,
    isHideBackBtn: boolean,
    submitBtnText: string,
    backBtnText: string,
    isCustomHandleRequest: boolean,
    isAutoHandlePost: boolean,
    action: string,
    single: boolean
}