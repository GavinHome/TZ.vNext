import { TzSuperFormType, TzSuperFormGroup, TzSuperFormField } from "./TzSuperFormSchema";

export function getComponentName(type: TzSuperFormType) {
    let eleBuiltInNames: string[] = ["input"];
    let tzBuiltInNames: string[] = ["textarea", "number", "select", "dialog", "text", "switch"];
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
