import { debug } from "../../log";

// import "./decorators";

export enum FieldTypeEnum {
    String = "string",
    Number = "number",
    Date = "date",
    Enums = "enums",
    Command = "command"
}

export enum DataStatusEnum {
    /// <summary>
    /// 有效的
    /// </summary>
    Valid = 0,

    /// <summary>
    /// 无效的
    /// </summary>
    Invalid = 1,

    /// <summary>
    /// 删除的
    /// </summary>
    Deleted = 2,

    /// <summary>
    /// 已作废
    /// </summary>
    Nullify = 3
}

export enum FormContentType {
    /// <summary>
    /// 文本
    /// </summary>
    Text = 0,

    /// <summary>
    /// 数值
    /// </summary>
    Number = 1
}

export enum FormType {
    /// <summary>
    /// 公式计算
    /// </summary>
    Formula = 0,

    /// <summary>
    /// 导入数据
    /// </summary>
    Import = 1,

    /// <summary>
    /// 固定
    /// </summary>
    Fixed = 2
}

export enum ReportTypeEnum {
    /// <summary>
    /// 年度
    /// </summary>
    Annual = 0,

    /// <summary>
    /// 月度
    /// </summary>
    Monthly = 1
}

export enum ReportStatusEnum {
    /// <summary>
    /// 生成中
    /// </summary>
    Building = 0,

    /// <summary>
    /// 完成
    /// </summary>
    Completed = 1
}

export enum SalaryType {
    Formula = 0,
    Import = 1,
    Code = 2,
    Name = 3,
    Plate = 4,
    Company = 5,
    Organization = 6,
    Deparment = 7,
    SalaryCode = 8,
    Base = 9,
    Coef = 10,
    MonthTotal = 11,
    Subsidy = 12,
    PointSalary = 13,
    Cost = 14,
    SocialSecurity = 15,
    Reserve = 16,
    IncomeTax = 17,
    Other
}

export enum CustomerType {
    /// <summary>
    /// 社团法人
    /// </summary>
    CustomerInstitution = 0,

    /// <summary>
    /// 机关法人
    /// </summary>
    CustomerOrganization = 1,

    /// <summary>
    /// 事业法人
    /// </summary
    CustomerCareer = 2,

    /// <summary>
    /// 企业法人
    /// </summary>
    CustomerEnterprise = 3,

    /// <summary>
    /// 合伙企业
    /// </summary>
    CustomerPartnership = 4,

    /// <summary>
    /// 办事处
    /// </summary>
    CustomerOffice = 5,

    /// <summary>
    /// 自然人
    /// </summary>
    CustomerNaturalPerson = 6
}

export enum GenderType {
    /// <summary>
    /// 男
    /// </summary>
    Man = 0,

    /// <summary>
    /// 女
    /// </summary>
    Women = 1,

    /// <summary>
    /// 未知
    /// </summary>
    Unknown = 2,
}

export enum EnumConstType {
    Customer = 0,
    Gender = 1,
    DataStatus = 2,
    FormContentType = 3,
    ReportType = 4,
    ReportStatus = 5
}

export enum GridMenuType {
    Create = "Create",
    Delete = "Delete",
    Edit = "Edit",
    Detail = "Detail",
    Audit = "Audit",
    Enable = "Enable",
    Disable = "Disable",
    Termination = "Termination",
    ReportGenerate = "ReportGenerate",
    ReportView = "ReportView"
}

export namespace EnumHelper {
    export function toCustomerTypeEnumString(type: CustomerType): string {
        switch (type) {
            case CustomerType.CustomerInstitution:
                return "社团法人";
            case CustomerType.CustomerOrganization:
                return "机关法人";
            case CustomerType.CustomerCareer:
                return "事业法人";
            case CustomerType.CustomerEnterprise:
                return "企业法人";
            case CustomerType.CustomerPartnership:
                return "合伙企业";
            case CustomerType.CustomerOffice:
                return "办事处";
            case CustomerType.CustomerNaturalPerson:
                return "自然人";
            default:
                throw new Error("customer type enum error");
        }
    }

    export function toGridMenuTypeString(type: GridMenuType): string {
        switch (type) {
            case GridMenuType.ReportGenerate:
                return "生成";
            case GridMenuType.ReportView:
                return "详细";
            case GridMenuType.Termination:
                return "终止";
            case GridMenuType.Create:
                return "新增";
            case GridMenuType.Delete:
                return "删除";
            case GridMenuType.Edit:
                return "编辑";
            case GridMenuType.Detail:
                return "查看";
            case GridMenuType.Audit:
                return "审核";
            case GridMenuType.Enable:
                return "启用";
            case GridMenuType.Disable:
                return "禁用";
            default:
                throw new Error("grid menu type enum error");
        }
    }

    export function toGenderTypeEnumString(type: GenderType): string {
        switch (type) {
            case GenderType.Man:
                return "男";
            case GenderType.Women:
                return "女";
            case GenderType.Unknown:
                return "未知";
            default:
                throw new Error("gender type enum error");
        }
    }

    export function toDataStatusEnumString(type: DataStatusEnum): string {
        switch (type) {
            case DataStatusEnum.Valid:
                return "已启用";
            case DataStatusEnum.Invalid:
                return "已禁用";
            case DataStatusEnum.Deleted:
                return "已删除";
            case DataStatusEnum.Nullify:
                return "已作废";
            default:
                throw new Error("gender type enum error");
        }
    }

    export function toFormContentTypeEnumString(type: FormContentType): string {
        switch (type) {
            case FormContentType.Text:
                return "文本";
            case FormContentType.Number:
                return "数值";
            default:
                throw new Error("form content type enum error");
        }
    }

    export function toReportTypeEnumString(type: ReportTypeEnum): string {
        switch (type) {
            case ReportTypeEnum.Annual:
                return "年度";
            case ReportTypeEnum.Monthly:
                return "月度";
            default:
                throw new Error("form content type enum error");
        }
    }

    export function toReportStatusEnumString(type: ReportStatusEnum): string {
        switch (type) {
            case ReportStatusEnum.Building:
                return "生成中";
            case ReportStatusEnum.Completed:
                return "完成";
            default:
                throw new Error("form content type enum error");
        }
    }

    export function toEnumOptions(type: EnumConstType, labelKey: string = "label", valueKey: string = "value"): any[] {
        var options: any[] = [];
        switch (type) {
            case EnumConstType.Customer:
                options = Object.keys(CustomerType)
                    .filter(x => Number(x) !== 0 && !Number(x))
                    .map(k =>
                        Object({
                            value: CustomerType[k],
                            label: toCustomerTypeEnumString(CustomerType[k])
                        })
                    );
                return options;
            case EnumConstType.Gender:
                options = Object.keys(GenderType)
                    .filter(x => Number(x) !== 0 && !Number(x))
                    .map(k =>
                        Object({
                            value: GenderType[k],
                            label: toGenderTypeEnumString(GenderType[k])
                        })
                    );
                return options;
            case EnumConstType.DataStatus:
                options = Object.keys(DataStatusEnum)
                    .filter(x => Number(x) !== 0 && !Number(x))
                    .map(k => createKVObject(labelKey, valueKey, toDataStatusEnumString(DataStatusEnum[k]), DataStatusEnum[k]));
                return options;
            case EnumConstType.FormContentType:
                options = Object.keys(FormContentType)
                    .filter(x => Number(x) !== 0 && !Number(x))
                    .map(k => createKVObject(labelKey, valueKey, toFormContentTypeEnumString(FormContentType[k]), FormContentType[k]));
                return options;
            case EnumConstType.ReportType:
                options = Object.keys(ReportTypeEnum)
                    .filter(x => Number(x) !== 0 && !Number(x))
                    .map(k => createKVObject(labelKey, valueKey, toReportTypeEnumString(ReportTypeEnum[k]), ReportTypeEnum[k]));
                return options;
            case EnumConstType.ReportStatus:
                options = Object.keys(ReportStatusEnum)
                    .filter(x => Number(x) !== 0 && !Number(x))
                    .map(k => createKVObject(labelKey, valueKey, toReportStatusEnumString(ReportStatusEnum[k]), ReportStatusEnum[k]));
                return options;
            default:
                throw new Error("enum error");
        }
    }
}

function createKVObject(labelKey: string, valueKey: string, label: string, value: string | number) {
    var obj = Object({})
    obj[valueKey] = value;
    obj[labelKey] = label;
    return obj;
}