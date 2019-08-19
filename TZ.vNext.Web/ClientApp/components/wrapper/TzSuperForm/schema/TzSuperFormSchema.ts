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
    Shell = "shell"
}

export function getComponentName(type: TzSuperFormType) {
    let eleBuiltInNames: string[] = ["input"];
    let tzBuiltInNames: string[] = ["textarea", "number", "select", "dialog", "text", "switch", "shell"];
    if (eleBuiltInNames.includes(type)) {
        // element 内置组件
        return 'el-' + type
    } else if (tzBuiltInNames.includes(type)) {
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
