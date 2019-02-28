//  -----------------------------------------------------------------------
//  <copyright file="CodeTypeEnum.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
//   </copyright>
//  <author>lixiaojun</author>
//  <date>2013-09-26</date>
//  -----------------------------------------------------------------------
using System.ComponentModel;

namespace TZ.vNext.Model.Enum
{
    public enum CodeTypeEnum
    {
        /// <summary>
        /// 职级
        /// </summary>
        [Description("职级")]
        Rank = 1,

        /// <summary>
        /// 部门类型
        /// </summary>
        [Description("部门类型")]
        DepartmentType = 2,

        /// <summary>
        /// 数据范围
        /// </summary>
        [Description("数据范围")]
        ScopeOfData = 3,

        /// <summary>
        /// 专业特长
        /// </summary>
        [Description("专业特长")]
        Specialties = 4,

        /// <summary>
        /// 请假类型
        /// </summary>
        [Description("请假类型")]
        LeaveType = 5,

        /// <summary>
        /// 员工评价
        /// </summary>
        [Description("员工评价")]
        StaffEvaluation = 6,

        /// <summary>
        /// 岗位类型
        /// </summary>
        [Description("岗位类型")]
        RoleType = 7,

        /// <summary>
        /// 员工状态
        /// </summary>
        [Description("员工状态")]
        EmployeeStatus = 8,

        /// <summary>
        /// 工程所属专业
        /// </summary>
        [Description("工程所属专业")]
        ProjectProfession = 9,

        /// <summary>
        /// 业务取得方式
        /// </summary>
        [Description("业务取得方式")]
        BusinessMakeWay = 16,

        /// <summary>
        /// 业务来源
        /// </summary>
        [Description("业务来源")]
        BusinessSource = 17,

        /// <summary>
        /// 业务项目类别
        /// </summary>
        [Description("业务项目类别")]
        BusinessType = 18,

        /// <summary>按投资形式
        /// </summary>
        [Description("按所有制性质分类")]
        Ownership = 14,

        /// <summary>按投资形式
        /// </summary>
        [Description("按投资形式")]
        Investment = 15,

        /// <summary>按监管要求分类1
        /// </summary>
        [Description("按监管要求分类1")]
        RegulatoryRequirementsOne = 10,

        /// <summary>所属行业分类
        /// </summary>
        [Description("所属行业分类")]
        Industry = 12,

        /// <summary>按监管要求分类2
        /// </summary>
        [Description("按监管要求分类2")]
        RegulatoryRequirementsTwo = 11,

        /// <summary>客户职务 
        /// </summary>
        [Description("客户职务")]
        CustomerPosition = 21,

        /// <summary>集团职务 
        /// </summary>
        [Description("集团职务")]
        GroupPosition = 22,

        /// <summary>
        /// 客户性质
        /// </summary>
        [Description("客户性质2")]
        CustomerNature = 24,

        /// <summary>机关分类 
        /// </summary>
        [Description("机关分类")]
        OrganizationClassification = 25,

        /// <summary>证券市场 
        /// </summary>
        [Description("证券市场")]
        StockMarket = 26,

        /// <summary>审核状态
        /// </summary>
        [Description("审核状态")]
        ApproveState = 27,

        /// <summary>审核状态
        /// </summary>
        [Description("市场建设费")]
        Scjsf = 28,

        /// <summary>客户类型
        /// </summary>
        [Description("客户类型")]
        CustomerType = 23,

        /// <summary>币种
        /// </summary>
        [Description("币种")]
        Currency = 29,

        /// <summary>客户主管部门
        /// </summary>
        [Description("客户主管部门")]
        CustomerAdministratorDepartment = 45,

        /// <summary>合同类型
        /// </summary>
        [Description("合同类型")]
        ContractType = 46,

        /// <summary>合同来源
        /// </summary>
        [Description("合同来源")]
        ContractSource = 47,

        /// <summary>服务类型
        /// </summary>
        [Description("服务类型")]
        ContractServiceType = 48,

        /// <summary>取费基数
        /// </summary>
        [Description("取费基数")]
        ContractFeeBase = 50,

        /// <summary>报告类别
        /// </summary>
        [Description("报告类别")]
        ReportType = 51,

        /// <summary>报告出具单位
        /// </summary>
        [Description("报告出具单位")]
        IssuedUnit = 52,

        /// <summary>按最终地位分类(企业法人)
        /// </summary>
        [Description("按最终地位分类(企业法人)")]
        FinalPositionCustomerEnterprise = 100,

        /// <summary>按最终地位分类（合伙企业）
        /// </summary>
        [Description("按最终地位分类（合伙企业）")]
        FinalPositionCustomerPartnership = 101,

        /// <summary>按所属性质分类
        /// </summary>
        [Description("按所属性质分类")]
        BelongsShip = 102,

        /// <summary>PPP咨询报告类型
        /// </summary>
        [Description("PPP咨询报告类型")]
        PppReport = 103,

        /// <summary>PPP报告出具单位
        /// </summary>
        [Description("PPP报告出具单位")]
        PppIssuedUnit = 104,

        /// <summary>客户类别 
        /// </summary>
        [Description("客户类别")]
        CustomerClasses = 110,

        /// <summary>
        /// 项目驳回原因 
        /// </summary>
        [Description("项目驳回原因")]
        ProjectRejectReason = 120,

        /// <summary>
        /// 合同驳回原因 
        /// </summary>
        [Description("合同驳回原因")]
        ContractRejectReason = 121,

        /// <summary>
        /// 回款类型 
        /// </summary>
        [Description("回款类型")]
        ReceiveType = 122,

        /// <summary>
        /// 回款方式 
        /// </summary>
        [Description("回款方式")]
        ReceiveMode = 123,

        /// <summary>
        /// 项目专业组类型 
        /// </summary>
        [Description("专业组类型")]
        ProjectTeamType = 1000,

        /// <summary>
        /// 工程软件类专业组类型 
        /// </summary>
        [Description("工程软件类专业组类型")]
        ProjectTeamSoftwareType = 1001,

        /// <summary>
        /// 工程软件类专业组类型 
        /// </summary>
        [Description("审计项目专业组类型")]
        AuditProjectTeamType = 1002,

        /// <summary>
        /// 取票方式 
        /// </summary>
        [Description("取票方式")]
        TicketType = 124,

        /// <summary>
        /// 现场负责人/实施部门经理审核意见 
        /// </summary>
        [Description("现场负责人/实施部门经理审核意见")]
        ProjectPrincipalOrManagerRejectReason = 125,

        /// <summary>
        /// 税务岗审核意见 
        /// </summary>
        [Description("税务岗审核意见")]
        TaxRejectReason = 126,

        /// <summary>
        /// 退票原因类型 
        /// </summary>
        [Description("退票原因类型")]
        RefundReasonType = 30,

        /// <summary>
        /// 退票原因说明 
        /// </summary>
        [Description("退票原因说明")]
        RefundReasonExplain = 31,

        /// <summary>
        /// 退票原因说明 
        /// </summary>
        [Description("开发方式")]
        DevelopmentMode = 32,

        /// <summary>
        /// 报销驳回意见 
        /// </summary>
        [Description("报销驳回意见")]
        ExpensesRejectReason = 127,

        /// <summary>
        /// 报销方式 
        /// </summary>
        [Description("报销方式")]
        ExpensesMode = 130,

        /// <summary>
        /// 销售项目类别 
        /// </summary>
        [Description("销售项目类别")]
        SaleProject = 201,

        /// <summary>
        /// 管理项目类别 
        /// </summary>
        [Description("管理项目类别")]
        ManagementProject = 202,

        /// <summary>
        /// 退票驳回意见 
        /// </summary>
        [Description("退票驳回意见")]
        InvoiceReturnRejectReason = 128,

        /// <summary>
        /// 退票类型 
        /// </summary>
        [Description("退票类型")]
        RefundType = 129,

        /// <summary>
        /// 长途交通工具 
        /// </summary>
        [Description("长途交通工具")]
        LongDistanceTransport = 131,

        /// <summary>
        /// 市内交通工具 
        /// </summary>
        [Description("市内交通工具")]
        CityTransport = 132,

        /// <summary>
        /// 火车席别 
        /// </summary>
        [Description("火车席别")]
        TrainSeats = 133,

        /// <summary>
        /// 长途汽车席别 
        /// </summary>
        [Description("长途汽车席别")]
        CoachSeats = 134,

        /// <summary>
        /// 动车席别 
        /// </summary>
        [Description("动车席别")]
        EmuSeats = 135,

        /// <summary>
        /// 飞机席别 
        /// </summary>
        [Description("飞机席别")]
        AircraftSeats = 136,

        /// <summary>
        /// 轮船席别 
        /// </summary>
        [Description("轮船席别")]
        ShipSeats = 137,

        /// <summary>
        /// 汽车排量
        /// </summary>
        [Description("汽车排量")]
        CarDisplacement = 138,

        /// <summary>
        /// 高铁席别
        /// </summary>
        [Description("高铁席别")]
        HighSpeedRailSeats = 139,

        /// <summary>
        /// 管理项目类别
        /// </summary>
        [Description("管理项目类别")]
        ManagementProjectCategory = 140,

        /// <summary>
        /// 销售项目审核意见
        /// </summary>
        [Description("销售项目审核意见")]
        SalesProjectAuditOpinion = 141,

        /// <summary>
        /// 与编制所在地距离
        /// </summary>
        [Description("与编制所在地距离")]
        DistanceFromLocation = 143,

        /// <summary>
        /// 销售项目专业组类型 
        /// </summary>
        [Description("销售项目专业组类型")]
        SalesProjectTeamType = 144,

        /// <summary>
        /// 合同归档状态 
        /// </summary>
        [Description("合同归档状态")]
        ArchivesStatus = 145,

        /// <summary>
        /// 面试表员工属性 
        /// </summary>
        [Description("面试表员工属性")]
        PersonalType = 146,

        /// <summary>
        /// 面试员工属性 
        /// </summary>
        [Description("面试员工属性")]
        Degree = 53,

        /// <summary>
        /// 放弃入职原因 
        /// </summary>
        [Description("放弃入职原因")]
        GiveUpReason = 54,

        /// <summary>
        /// 政治面貌 
        /// </summary>
        [Description("政治面貌")]
        PoliticalStatus = 55,

        /// <summary>
        /// 外语类别 
        /// </summary>
        [Description("外语类别")]
        AdeptLanguage = 56,

        /// <summary>
        /// 外语等级 
        /// </summary>
        [Description("外语等级")]
        AdeptLanguageLevel = 57,

        /// <summary>
        /// 民族 
        /// </summary>
        [Description("民族")]
        Nationality = 58,

        /// <summary>
        /// 专业技术 
        /// </summary>
        [Description("专业技术")]
        ProfessionalTitle = 59,

        /// <summary>
        /// 教育背景 
        /// </summary>
        [Description("教育背景")]
        EducationBackground = 60,

        /// <summary>
        /// 执业资质名称 
        /// </summary>
        [Description("执业资质名称")]
        Certificate = 61,

        /// <summary>
        /// 执业资质注册单位 
        /// </summary>
        [Description("执业资质注册单位")]
        RegisterUnit = 62,

        /// <summary>
        /// 离职原因 
        /// </summary>
        [Description("离职原因")]
        ReasonsForLeaving = 63,

        /// <summary>
        /// 与紧急联系人关系 
        /// </summary>
        [Description("与紧急联系人关系")]
        Relationship = 64,

        /// <summary>
        /// 称谓 
        /// </summary>
        [Description("称谓")]
        Title = 65,

        /// <summary>
        /// 电脑使用情况 
        /// </summary>
        [Description("电脑使用情况")]
        ComputerServiceCondition = 66,

        /// <summary>
        /// 电脑使用情况 
        /// </summary>
        [Description("入职管理驳回原因")]
        EntryApplyRejectReason = 67,

        /// <summary>
        /// 档案存放部门 
        /// </summary>
        [Description("档案存放部门")]
        ArchivesLocation = 68,

        /// <summary>
        /// 取得文凭/资格证书列类型 
        /// </summary>
        [Description("取得文凭/资格证书列类型")]
        Certificates = 69,

        /// <summary>
        /// 注册地
        /// </summary>
        [Description("注册地")]
        RegistrationPlace = 70,

        /// <summary>
        /// 应聘城市 
        /// </summary>
        [Description("应聘城市")]
        ApplyForTheCity = 71,

        /// <summary>
        /// 报告驳回原因 
        /// </summary>
        [Description("报告驳回原因")]
        ProjectReportRejectReason = 72,

        /// <summary>
        /// 月度工资审核驳回原因 
        /// </summary>
        [Description("月度工资审核驳回原因")]
        MonthlySalaryRejectReason = 203,

        /// <summary>
        /// 月度工资缓发审核驳回原因 
        /// </summary>
        [Description("月度工资缓发审核驳回原因")]
        MonthlySalaryDelayRejectReason = 204,

        /// <summary>
        /// 事务所兼岗人员申请修改审核驳回原因 
        /// </summary>
        [Description("事务所兼岗人员申请修改审核驳回原因")]
        OfficePostsModifyApplyRejectReason = 205,

        /// <summary>
        /// 月度工资缓发理由 
        /// </summary>
        [Description("月度工资缓发理由")]
        MonthlySalaryDelayReason = 206,

        /// <summary>
        /// 验收意见 
        /// </summary>
        [Description("验收意见")]
        BudgetCheckOption = 210,

        /// <summary>
        /// 重新验收意见 
        /// </summary>
        [Description("重新验收意见")]
        BudgetReCheckOption = 211,

        /// <summary>
        /// 协审意见 
        /// </summary>
        [Description("协审意见")]
        CooperationApproveOpinion = 212,

        /// <summary>
        /// 应扣类型
        /// </summary>
        [Description("应扣类型")]
        ShouldDeduction = 213,

        /// <summary>
        /// 其他类型
        /// </summary>
        [Description("其他类型")]
        Other = 214,

        /// <summary>
        /// 其他应发/应扣 
        /// </summary>
        [Description("其他应发/应扣")]
        OtherPayOrDeduction = 215,

        /// <summary>
        /// 证件类型
        /// </summary>
        [Description("证件类型")]
        CardType = 216,

        /// <summary>
        /// 机票类型
        /// </summary>
        [Description("机票类型")]
        AirTicketType = 217,

        /// <summary>
        /// 工作流类型
        /// </summary>
        [Description("工作流类型")]
        ModuleType = 300,

        /// <summary>
        /// 员工调岗申请原因
        /// </summary>
        [Description("员工调岗申请原因")]
        TransferPositionReason = 148,

        /// <summary>
        /// 调出部门工资发放情况
        /// </summary>
        [Description("调出部门工资发放情况")]
        WagesPayment = 149,

        /// <summary>
        /// 调岗审核驳回意见
        /// </summary>
        [Description("调岗审核驳回意见")]
        TransferPositionRejectReason = 150,

        /// <summary>
        /// 管理项目工资额度驳回原因
        /// </summary>
        [Description("管理项目工资额度驳回原因")]
        ProjectWageRejectReason = 151,

        /// <summary>
        /// 研发项目类型
        /// </summary>
        [Description("研发项目类型")]
        ResearchProjectType = 152,

        /// <summary>
        /// 研发项目驳回原因
        /// </summary>
        [Description("研发项目驳回原因")]
        ResearchProjectRejectReason = 153,

        /// <summary>
        /// 员工劳动关系调整驳回原因
        /// </summary>
        [Description("员工劳动关系调整驳回原因")]
        TransferPersonTypeRejectReason = 154,

        /// <summary>
        /// 项目总监类型
        /// </summary>
        [Description("项目总监类型")]
        ProjectDirectorType = 156,

        /// <summary>
        /// 员工信息申请修改审核驳回原因
        /// </summary>
        [Description("员工信息申请修改审核驳回原因")]
        EntryPersonalModifyRejectReason = 218,

        /// <summary>
        /// 转正审核驳回原因
        /// </summary>
        [Description("转正审核驳回原因")]
        PositiveRejectReason = 219,

        /// <summary>
        /// 请假类型
        /// </summary>
        [Description("请假类型")]
        AbsenceType = 220,

        /// <summary>
        /// 请假审核驳回原因
        /// </summary>
        [Description("请假审核驳回原因")]
        AbsenceRejectReason = 221,

        /// <summary>
        /// 请假开始时间
        /// </summary>
        [Description("请假开始时间")]
        AbsenceBeginTime = 222,

        /// <summary>
        /// 请假结束时间
        /// </summary>
        [Description("请假结束时间")]
        AbsenceEndTime = 223,

        /// <summary>
        /// 合同关闭驳回原因
        /// </summary>
        [Description("合同关闭驳回原因")]
        ContractCloseRejectReason = 224,

        /// <summary>
        /// 销售项目关闭驳回原因
        /// </summary>
        [Description("销售项目关闭驳回原因")]
        SaleProjectCloseRejectReason = 225,

        /// <summary>
        /// 薪酬地区
        /// </summary>
        [Description("薪酬地区")]
        SalaryRegion = 226,

        /// <summary>
        /// 薪酬类型
        /// </summary>
        [Description("薪酬类型")]
        SalaryType = 227,

        /// <summary>
        /// 外语能力
        /// </summary>
        [Description("外语能力")]
        ForeignLanguageAbility = 228,

        /// <summary>
        /// 回款修改审核驳回原因
        /// </summary>
        [Description("回款修改审核驳回原因")]
        ReceivedPaymentsRejectReason = 229,

        /// <summary>
        /// 投标实体销售项目结果确认类型
        /// </summary>
        [Description("投标实体销售项目结果确认类型")]
        BidSalesProjectResultConfirmation = 230,

        /// <summary>
        /// 实体及入围销售项目结果确认类型
        /// </summary>
        [Description("实体及入围销售项目结果确认类型")]
        SalesProjectResultConfirmation = 231,

        /// <summary>
        /// 销售项目结果确认附件类型
        /// </summary>
        [Description("销售项目结果确认附件类型")]
        SalesProjectResultConfirmationAttachmentType = 232,

        /// <summary>
        /// 销售项目结果确认驳回原因
        /// </summary>
        [Description("销售项目结果确认驳回原因")]
        SalesProjectResultConfirmationRejectReason = 233,

        /// <summary>
        /// 事件类型
        /// </summary>
        [Description("事件类型")]
        AffairType = 234,

        /// <summary>
        /// 客户审核驳回原因
        /// </summary>
        [Description("客户审核驳回原因")]
        CustomerManagementRejectReason = 236,

        /// <summary>
        /// 客户现状
        /// </summary>
        [Description("客户现状")]
        CustomerReality = 73,

        /// <summary>
        /// 收费方式
        /// </summary>
        [Description("收费方式")]
        ChargingMethods = 74,

        /// <summary>
        /// 离职原因
        /// </summary>
        [Description("离职原因")]
        LeaveReason = 75,

        /// <summary>
        /// 面试流程终止原因
        /// </summary>
        [Description("面试流程终止原因")]
        InterviewStopProcessReason = 76,

        /// <summary>
        /// 入职流程终止原因
        /// </summary>
        [Description("入职流程终止原因")]
        EntityStopProcessReason = 77,

        /// <summary>
        /// 假期类型
        /// </summary>
        [Description("假期类型")]
        VacationType = 78,

        /// <summary>
        /// 参建单位类型
        /// </summary>
        [Description("参建单位类型")]
        ConstructionUnitsType = 79,

        /// <summary>
        /// 政策性调整驳回原因
        /// </summary>
        [Description("政策性调整驳回原因")]
        PolicyAdjustmentRejectReason = 80,

        /// <summary>
        /// 公司奖罚驳回原因
        /// </summary>
        [Description("公司奖罚驳回原因")]
        PerformanceSanctionRejectReason = 81,

        /// <summary>
        /// 五险一金驳回原因
        /// </summary>
        [Description("五险一金驳回原因")]
        SocialInsuranceEmployeeRejectReason = 82,

        /// <summary>
        /// 劳动关系归属单位
        /// </summary>
        [Description("劳动关系归属单位")]
        LaborRelationsBelongUnit = 83,

        /// <summary>
        /// 证照借阅驳回原因
        /// </summary>
        [Description("证照借阅驳回原因")]
        LicenseBorrow = 84,

        /// <summary>
        /// 送达方式
        /// </summary>
        [Description("送达方式")]
        LicenseBorrowServiceMode = 85,

        /// <summary>
        /// 证照转借驳回原因
        /// </summary>
        [Description("证照转借驳回原因")]
        LicenseSubtenancy = 86,

        /// <summary>
        /// 劳务派遣入职驳回原因
        /// </summary>
        [Description("劳务派遣入职驳回原因")]
        TemporaryEntryApplyRejectReason = 88,

        /// <summary>
        /// 管理项目审核驳回原因
        /// </summary>
        [Description("管理项目审核驳回原因")]
        ProjectDailyRejectReason = 89,

        /// <summary>
        /// 收款类型
        /// </summary>
        [Description("收款类型")]
        ReceivablesType = 90,

        /// <summary>
        /// 收款方式
        /// </summary>
        [Description("收款方式")]
        ReceivablesMode = 91,

        /// <summary>
        /// 保证金回款类型
        /// </summary>
        [Description("保证金回款类型")]
        MarginReceivablesType = 92,

        /// <summary>
        /// 保证金审核驳回原因
        /// </summary>
        [Description("保证金审核驳回原因")]
        MarginReceivablesRejectReason = 93,

        /// <summary>
        /// 需求变更驳回原因
        /// </summary>
        [Description("需求变更驳回原因")]
        RequirementChangeRejectReason = 235,

        /// <summary>
        /// 工时驳回原因
        /// </summary>
        [Description("工时驳回原因")]
        WorkingHourRejectReason = 237,

        /// <summary>
        /// 机票改签驳回原因
        /// </summary>
        [Description("机票改签驳回原因")]
        ChangeAirTicketRejectReason = 238,

        /// <summary>
        /// 往来管理单据类型
        /// </summary>
        [Description("往来管理单据类型")]
        DocumentType = 239,

        /// <summary>
        /// 往来管理审核流程驳回意见
        /// </summary>
        [Description("往来管理审核流程驳回意见")]
        CurrentAccountRejectReason = 240,

        /// <summary>
        /// 审计询问驳回原因
        /// </summary>
        [Description("审计询问驳回原因")]
        InquiryRejectReason = 241,

        /// <summary>
        /// 资料清单驳回原因
        /// </summary>
        [Description("资料清单驳回原因")]
        MaterialListRejectReason = 242,

        /// <summary>
        /// 审计评价驳回原因
        /// </summary>
        [Description("审计评价驳回原因")]
        EvaluationRejectReason = 243,

        /// <summary>
        /// 考核-收入调整审核驳回原因
        /// </summary>
        [Description("收入调整审核驳回原因")]
        PerformanceBorrowSalaryRejectReason = 244,

        /// <summary>
        /// 考核-政策调整审核驳回原因
        /// </summary>
        [Description("政策调整审核驳回原因")]
        PerformanceAdjustIncomeRejectReason = 245,

            /// <summary>
        /// 考核-预借工资审核驳回原因
        /// </summary>
        [Description("预借工资审核驳回原因")]
        PerformancePolicyAdjustmentRejectReason = 246,

        /// <summary>
        /// 考核-分公司分摊审核驳回原因
        /// </summary>
        [Description("分公司分摊审核驳回原因")]
        PerformanceBranchManagerRejectReason = 247,

        /// <summary>
        /// 专业特长
        /// </summary>
        [Description("专业特长")]
        PersonalInfoMajorSpecialties = 248,

        /// <summary>
        /// 专业特长-行业（支持类）
        /// </summary>
        [Description("专业特长-行业（支持类）")]
        MajorSpecialtiesSupport = 249,

        /// <summary>
        /// 审计报告驳回原因
        /// </summary>
        [Description("审计报告驳回原因")]
        AuditReportRejectReason = 250,

        /// <summary>
        /// 审计底稿驳回原因
        /// </summary>
        [Description("审计底稿驳回原因")]
        AuditPapersRejectReason = 251,

        /// <summary>
        /// 项目投资类别
        /// </summary>
        [Description("项目投资类别")]
        ProjectInvestmentType = 252,

        /// <summary>
        /// 划转事项
        /// </summary>
        [Description("划转事项")]
        TransferMatter = 253,

        /// <summary>
        /// 划转类型
        /// </summary>
        [Description("划转类型")]
        TransferType = 254,

        /// <summary>
        /// 划出科目
        /// </summary>
        [Description("划出科目")]
        Subject = 255,

        /// <summary>
        /// 成本划转驳回原因
        /// </summary>
        [Description("成本划转驳回原因")]
        CostTransferRejectReason = 256,

        /// <summary>
        /// 发票类型
        /// </summary>
        [Description("发票类型")]
        InvoiceType = 257,

        /// <summary>
        /// 机票账单驳回原因
        /// </summary>
        [Description("机票账单驳回原因")]
        AirTicketBillRejectReason = 258,

        /// <summary>
        /// 五险一金办理类别
        /// </summary>
        [Description("办理类别")]
        SocialInsuranceEmployeeType = 259,

        /// <summary>
        /// 公司名称
        /// </summary>
        [Description("公司名称")]
        CompanyName = 260,

        /// <summary>
        /// 五险一金基数驳回意见
        /// </summary>
        [Description("五险一金基数驳回意见")]
        SocialInsuranceBaseRejectReason = 261,

        /// <summary>
        /// 合同修改类型
        /// </summary>
        [Description("合同修改类型")]
        ContactModifyType = 262,

        /// <summary>
        /// 员工离职驳回意见
        /// </summary>
        [Description("员工离职驳回意见")]
        EmployeeLeaveRequest = 263,

        /// <summary>
        /// 合同结算审核驳回意见
        /// </summary>
        [Description("合同结算审核驳回意见")]
        ContractClearingApprovalRejectReason = 264,

        /// <summary>
        /// 证照类型
        /// </summary>
        [Description("证照类型")]
        LicenseType = 265,

        /// <summary>
        /// 证照操作
        /// </summary>
        [Description("证照操作")]
        LicenseOperation = 266,

        /// <summary>
        /// 公司类型
        /// </summary>
        [Description("公司类型")]
        CompanyType = 270,

        /// <summary>
        /// 法人主体登记注册类型
        /// </summary>
        [Description("法人主体登记注册类型")]
        RegistrationType = 271,

        /// <summary>
        /// 法人主体增值税纳税人类别
        /// </summary>
        [Description("法人主体增值税纳税人类别")]
        VatTaxpayerCategory = 272,

        /// <summary>
        /// 销售项目积分要素
        /// </summary>
        [Description("销售项目积分要素")]
        SalesProjectPointFactor = 301,

        /// <summary>
        /// 经销商类别
        /// </summary>
        [Description("经销商类别")]
        DealerType = 302,

        /// <summary>
        /// 经销商合作方类别
        /// </summary>
        [Description("经销商合作方类别")]
        CustomerCategory = 303,
        
        /// <summary>
        /// 经销商合作方类别
        /// </summary>
        [Description("经销商合作方驳回原因")]
        DealerPartnerRejectReason = 236,

        /// <summary>
        /// 服务产品类别
        /// </summary>
        [Description("服务产品类别")]
        SalesProduct = 304,

        /// <summary>
        /// 所属条线
        /// </summary>
        [Description("所属条线")]
        ProfessionalPlate = 306,
    }
}
