import { TzSuperFormGroup, TzSuperFormAttrSchema } from "../wrapper/TzSuperForm/schema/TzSuperFormSchema";

export interface Product {
    Id?: string;
    Name?: string;
    Description?: string;
    ContentData?: any
}

export interface ProductContent {
    form: TzSuperFormGroup[];
    formData?: any;
    formAttr: TzSuperFormAttrSchema;
    rules?: any;
}
