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
    type: TzSuperFormType;
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
    /// 选择器
    /// </summary>
    Employee = "employee-grid",

    /// <summary>
    /// 弹框数据
    /// </summary>
    Dialog = "dialog",

    /// <summary>
    /// 弹框数据
    /// </summary>
    Switch = "switch"
}
