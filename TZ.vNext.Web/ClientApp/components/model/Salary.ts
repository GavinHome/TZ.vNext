import { FormType, FormContentType } from "../common/Enums";

export class Salary {
    Id?: string;
    Name?: string;
    Key?: string;
    FormType?: FormType;
    FormContent?: FormContentType;
    FormName?: string;
    SalaryType?: string;
    IsIncluded?: boolean;
    Description?: string;
}

export interface EmployeeSalaryItem {
    Key?: string;
    Value?: string;
    Name?: string;
    FormType?: FormType;
    FormName?: string;
    FormContent?: FormContentType;
}

export interface EmployeeSalaryGroup {
    Key?: string;
    Value?: string;
    Name?: string;
    Type?: FormType;
    Children: EmployeeSalaryItem[];
}