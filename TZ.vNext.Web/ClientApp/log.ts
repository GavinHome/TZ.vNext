import StoreCache from "./components/common/TzStoreCache";
import { TzApiConst } from "./components/common/TzCommonConst";
import { TzFetch } from "./components/common/TzFetch";

let debugConfig = {
    Vue: null,
    //项目名称
    entryName: "entryName",
    //脚本版本
    scriptVersion: "1.0",
    // 环境
    releaseStage: "pro",

    //上报地址
    logReportUrl: TzApiConst.LOG_INDEX
};

// 日志上报
function _logReport(
    type: any,
    severity: any,
    error: any,
    metaData: any,
    message: any,
    lineNumber: any,
    columnNumber: any,
    fileName: any
) {
    let { Vue } = debugConfig;

    message = message || (error && error.message) || "";

    //这里可以做一个灰度控制

    let { entryName, releaseStage, logReportUrl, scriptVersion } = debugConfig,
        name = (error && error.name) || "error",
        stacktrace = (error && error.stack) || "",
        time = Date.now(),
        title = document.title,
        url = window.location.href,
        client = {
            userAgent: window.navigator.userAgent,
            height: window.screen.height,
            width: window.screen.width,
            referrer: window.document.referrer
        },
        pageLevel = "p4";

    //此处可以给页面进行分级
    //pageLevel = "p0"; //getPageLevel();

    //此处http请求使用的是vue-resource，可以根据各自的情况进行调整
    if (Vue) {
        var bodyContent = {
            entryName,
            scriptVersion,
            message,
            metaData,
            name,
            releaseStage,
            severity,
            stacktrace,
            time,
            title,
            type,
            url,
            client,
            lineNumber,
            columnNumber,
            fileName,
            pageLevel
        }

        TzFetch.Post(logReportUrl, bodyContent, false)
    }
}

let debug = {
    notifyWarn({ message, metaData, lineNumber, columnNumber, fileName }) {
        let type = "caught",
            severity = "warn";
        _logReport(
            type,
            severity,
            message,
            metaData,
            message,
            lineNumber,
            columnNumber,
            fileName
        );
    },
    notifyError({
        type = "caught",
        error,
        message,
        metaData,
        lineNumber,
        columnNumber,
        fileName
    }) {
        let severity = "error";
        _logReport(
            type,
            severity,
            error,
            metaData,
            message,
            lineNumber,
            columnNumber,
            fileName
        );
    }
};

function getUserToken(): any {
    let cache = new StoreCache("auth");
    let token = cache.get("token");
    let name = cache.get("name");

    return { token: token, name: name };
}

export default function (Vue, option = {}) {
    debugConfig = Object.assign(debugConfig, { Vue, ...option });

    //如果你想在开发环境不去捕获错误信息 可以在此处加上环境判断
    function formatComponentName(vm) {
        if (vm.$root === vm) return "root";
        let name = vm._isVue
            ? (vm.$options && vm.$options.name) ||
            (vm.$options && vm.$options._componentTag)
            : vm.name;
        return (
            (name ? "component <" + name + ">" : "anonymous component") +
            (vm._isVue && vm.$options && vm.$options.__file
                ? " at " + (vm.$options && vm.$options.__file)
                : "")
        );
    }

    Vue.config.errorHandler = function (err, vm, info) {
        if (vm) {
            let componentName = formatComponentName(vm);
            let propsData = vm.$options && vm.$options.propsData;
            debug.notifyError({
                error: err,
                message: "",
                metaData: {
                    componentName,
                    propsData,
                    info,
                    userToken: getUserToken() //metaData可以存一些额外数据，比如：用户信息等
                },
                lineNumber: null,
                columnNumber: null,
                fileName: ""
            });
        } else {
            debug.notifyError({
                error: err,
                message: "",
                metaData: {
                    userToken: getUserToken() //metaData可以存一些额外数据，比如：用户信息等
                },
                lineNumber: 1,
                columnNumber: 2,
                fileName: ""
            });
        }
    };

    window.onerror = function (msg, url, lineNo, columnNo, error) {
        debug.notifyError({
            type: "uncaught",
            error,
            metaData: {
                userToken: getUserToken() //metaData可以存一些额外数据，比如：用户信息等
            },
            message: msg,
            lineNumber: lineNo,
            columnNumber: columnNo,
            fileName: url
        });
    };
}

//最后我们把debug抛到外面供其他地方调用
export { debug };
