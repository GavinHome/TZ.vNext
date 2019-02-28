import { GridCommand } from "../common/TzGridCommand";
import { FieldTypeEnum } from "../common/Enums";

export interface GridColumnSchema {
    field?: string;
    title: string;
    width?: string;
    filterable: boolean;
    sortable: boolean;
    editable: boolean;
    type: FieldTypeEnum;
    format?: string;
    menu: boolean;
    command?: GridCommand[];
    index: number;
    hidden?: boolean;
    values?: any[];
}
