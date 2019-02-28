import { MessageBox, Message } from "element-ui";
import { TzMessageConst } from "./TzCommonConst";
import { TzFetch } from "./TzFetch";

export interface IDialog {
}

export class TzDialog implements IDialog {
    static index: number = 0;
    private showConfirmButton: boolean;
    private showCancelButton: boolean;

    constructor(h: any, comp: any, title: string, callback: (d: any) => {}, props?: any) {
        this.showConfirmButton = false;
        this.showCancelButton = true;

        if (props && (props.multiply || props.custom)) {
            this.showCancelButton = false;
        }

        //const h = that.$createElement;
        MessageBox({
            message: h(comp, {
                key: TzDialog.index++,
                props: props,
                on: {
                    "submit": function (d: any) {
                        if (callback) {
                            callback(d)
                        }

                        MessageBox.close()
                    },
                    "close": function (d: any) {
                        MessageBox.close()
                    }
                }
            }),
            title: title,
            showConfirmButton: this.showConfirmButton,
            showCancelButton: this.showCancelButton,
            customClass: 'tz-dialog'
        });
    }
}

export class TzConfirm implements IDialog {
    delete(url: string, data: {}) {
        var promise = new Promise((resolve, reject) => {
            this.warn(url, TzMessageConst.DELETE_CONFIRM_MESSAGE, TzMessageConst.DELETE_MESSAGE, data).then(res => {
                Message.success(TzMessageConst.DELETE_SUCCESS_MESSAGE)
                resolve(res);
            }).catch(e => {
                reject(e);
                Message.success(TzMessageConst.DELETE_FAIL_MESSAGE)
            })
        });

        return promise
    }

    enable(url: string, message: string, title: string, data: any) {
        var promise = new Promise((resolve, reject) => {
            this.warn(url, message, title, data).then(res => {
                Message.success(TzMessageConst.ENABLE_SUCCESS_MESSAGE)
                resolve(res);
            }).catch(e => {
                reject(e);
            })
        });

        return promise;
    }

    disable(url: string, message: string, title: string, data: any) {
        var promise = new Promise((resolve, reject) => {
            this.warn(url, message, title, data).then(res => {
                Message.success(TzMessageConst.DISABLE_SUCCESS_MESSAGE)
                resolve(res);
            }).catch(e => {
                reject(e);
            })
        });

        return promise;
    }

    warn(url: string, message: string, title: string, data: any) {
        var promise = new Promise((resolve, reject) => {
            MessageBox.confirm(message, title, {
                confirmButtonText: TzMessageConst.OK_MESSAGE,
                cancelButtonText: TzMessageConst.CANCEL_MESSAGE,
                type: 'warning'
            }).then((res) => {
                TzFetch.Post(url, data, false).then(res => {
                    resolve(res);
                }).catch((e) => {
                    reject(e)
                })
            }).catch((e) => {
                Message.info(TzMessageConst.CANCEL_OPERATION_SUCCESS_MESSAGE)
            })
        });

        return promise;
    }
}
