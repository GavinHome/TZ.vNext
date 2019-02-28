import TzSpin from "./TzSpin";

export namespace TzFetch {
    export function Post(url: string, data: any, isSpinner: boolean = true) {
        var promise = new Promise((resolve, reject) => {
            if (isSpinner) {
                TzSpin.show()
            }

            fetch(url, {
                method: "post",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            }).then(res => res.json()).then(res => {
                TzSpin.close()
                resolve(res)
            }).catch((e) => {
                TzSpin.close()
                reject(e)
            })
        });

        return promise;
    }

    export function Get(url: string, isSpinner: boolean = true) {
        var promise = new Promise((resolve, reject) => {
            if (isSpinner) {
                TzSpin.show()
            }

            fetch(url, {
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(res => res.json()).then(res => {
                TzSpin.close()
                resolve(res)
            }).catch(e => {
                TzSpin.close()
                reject(e)
            })
        });

        return promise;
    }
}