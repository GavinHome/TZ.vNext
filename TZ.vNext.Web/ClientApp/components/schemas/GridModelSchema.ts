import { FieldTypeEnum } from "../common/Enums";

export interface GridModelSchemaType {
    filterable: boolean;
    sortable: boolean;
    editable: boolean;
    nullable: boolean;
    type: FieldTypeEnum;
}

export interface GridModelSchema {
    [key: string]: GridModelSchemaType;
}
