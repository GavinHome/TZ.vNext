export const TzCodeConst = {
}

export const TzConst = {
    RowNumber: 'RowNumber',  //序号列
    Coef:'Coef',         //系数的key
    Percent: 100,
    MinNumber: -999999999999999,
    MaxNumber: 999999999999999,
    DefaultDigit: 2,
    TWO: 2,
    THREE: 3,
    FORE: 4,
}

export const TzMessageConst = {
    AUTOGENERATE_MESSAGE: "提交后自动生成",

    CANCEL_MESSAGE: "取消",
    CANCEL_OPERATION_SUCCESS_MESSAGE: "已取消操作",

    DATA_FAIL_MESSAGE: "数据获取失败，请稍后重试！",

    DELETE_MESSAGE: "删除",
    DELETE_CONFIRM_MESSAGE: "是否删除 ",
    DELETE_FAIL_MESSAGE: "删除失败，请稍后重试！",
    DELETE_SUCCESS_MESSAGE: "删除成功",
    DELETEFILE_FAIL_MESSAGE: "删除失败，连接失败或文件不存在",

    DOWNLOAD_FAIL_MESSAGE: "下载错误，连接失败或文件不存在",

    DISABLE_MESSAGE: "禁用",
    DISABLE_CONFIRM_MESSAGE: "是否禁用 ",
    DISABLE_FAIL_MESSAGE: "禁用失败，请稍后重试！",
    DISABLE_SUCCESS_MESSAGE: "禁用成功",

    ENABLE_MESSAGE: "启用",
    ENABLE_CONFIRM_MESSAGE: "是否启用 ",
    ENABLE_FAIL_MESSAGE: "启用失败，请稍后重试！",
    ENABLE_SUCCESS_MESSAGE: "启用成功",

    IMPORT_FAIL_MESSAGE: "导入失败，请稍后重试！",
    IMPORT_SUCCESS_MESSAGE: "导入成功",

    OK_MESSAGE: "确定",

    SAVE_FAIL_MESSAGE: "保存失败，请稍后重试！",
    SAVE_SUCCESS_MESSAGE:"保存成功",

    STOP_MESSAGE: "取消",
    STOP_FAIL_MESSAGE: "终止失败，请稍后重试！",
    STOP_CONFIRM_MESSAGE: "是否终止 ",

    SUBMIT_FAIL_MESSAGE: "提交失败，请稍后重试！",

    TEMPLATE_FAIL_MESSAGE: "模板错误，可能的原因有：模板为空；模板内容不正确；模板列重复，请检查并修改",

    UPLOAD_EXCEED_MESSAGE: "已超过上传个数限制",
    UPLOAD_FAIL_MESSAGE: "上传失败，请稍后重试！",

    AUTO_LOGIN_FAIL_MESSAGE: "系统登录错误",
    LOGIN_FAIL_MESSAGE: "用户名或密码错误",
    LOGIN_SUCCESS_MESSAGE: "登录成功",

    SYMBOL_LINEBREAKER: "\n\n",
    SYMBOL_QUESTIONMARK: " ？",

    FETCH_URL_EXCEPTION_MESSAGE: "请求地址错误"
}

export const TzRuleMsgConst = {
    LOGIN_USERNAME: "请输入用户名",
    LOGIN_PASSWORD: "请输入密码",


    SALARY_FORMCONTENT_REQUIRED: "请选择薪酬项类型",
    SALARY_NAME_REQUIRED: "请输入薪酬项名称",
    SALARY_NAME_REPEATED: "薪酬项名称不能重复",
}

////系统API地址
export const TzApiConst = {
    LOG_INDEX: "/api/log/index",

    SALARY_GRID_QUERY: "/api/Salary/GridQuerySalaries",
    SALARY_FINDBYID: "/api/Salary/FindById",
    SALARY_ENABLE: "/api/Salary/Enable",
    SALARY_DISABLE: "/api/Salary/Disable",
    SALARY_CHECKNAME: "/api/Salary/CheckName",
    SALARY_SAVE: "/api/Salary/Save",

    PRODUCT_GRID_QUERY: "/api/Product/GridQueryProducts",
    PRODUCT_SAVE: "/api/Product/Save",
    PRODUCT_ENABLE: "/api/Product/Enable",
    PRODUCT_DISABLE: "/api/Product/Disable",
    PRODUCT_FINDBYID: "/api/Product/FindById",

    CODE_TREE: "/api/Codes/TreeQueryCodes",

    TOKEN: "/api/token"
}

export const TzFunctionConst = {
    SALARY_MANAGEMENT: "00000000-0000-1111-0000-000000000000",
    SALARY_BASIC: "00000000-0000-1111-1000-000000000000",
    SALARY_BASIC_SALARY_LIST: "00000000-0000-1111-1005-000000000000",
    SALARY_BASIC_SALARY_CREATE: "00000000-0000-1111-1005-000000000000",
    SALARY_BASIC_SALARY_EDIT: "00000000-0000-1111-1005-000000000000",
    SALARY_BASIC_SALARY_DETAIL: "00000000-0000-1111-1005-000000000000",
    
    PRODUCT_MANAGEMENT: "00000000-0000-1112-0000-000000000000",
    PRODUCT_LIST: "00000000-0000-1112-0001-000000000000",
    PRODUCT_CREATE: "00000000-0000-1112-0001-000000000000",
    PRODUCT_EDIT: "00000000-0000-1112-0001-000000000000",
    PRODUCT_DETAIL: "00000000-0000-1112-0001-000000000000"
}