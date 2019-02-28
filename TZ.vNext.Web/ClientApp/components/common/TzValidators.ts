import { TzFetch } from "./TzFetch";

let remote = (rule, value, callback) => {
    if (String.isNullOrEmpty(rule.url)) {
        callback();
        throw new Error("remote validator url is null");
    }

    if (!value) {
        return callback();
    }

    let url: string = rule.url;
    if (rule.field) {
        if (url.indexOf("?") > 0) {
            url += "&" + rule.field + "=" + value;
        } else {
            url += "?" + rule.field + "=" + value;
        }
    }

    //const url = rule.url + "?" + rule.field + "=" + value
    let message: string = rule.message;
    if (message) {
        message = `'${value}'已存在`;
    }

    PostHandler(url, message).then(res => {
        callback();
    }).catch(err => {
        callback(err);
    })
};

export default remote;

let files = (rule, value, callback) => {
    if (String.isNullOrEmpty(rule.url)) {
        callback();
        throw new Error("files validator url is null");
    }

    if (!value) {
        return callback();
    }

    let url: string = rule.url;
    let message: string = rule.message;
    if (message) {
        message = `请上传文件`;
    }

    PostHandler(url, message).then(res => {
        callback();
    }).catch(err => {
        callback(err);
    })
};

export var filesValidator = files;

function PostHandler(url: string, message: string) {
    var promise = new Promise((resolve, reject) => {
        TzFetch.Post(url, {}, false).then(data => {
            if (data) {
                resolve(data);
            } else {
                reject(new Error(message));
            }
        }).catch(e => reject(e));
    });

    return promise
}