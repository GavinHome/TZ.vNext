import { Loading } from 'element-ui';
var unique;
export default {
    show() {
        let opt = { body: true, text: '加载中...' };
        if (!unique) unique = Loading.service(opt);
    },
    close() {
        if (unique) {
            unique.close();
            unique = null;
        }
    },
    resolve(resolve) {
        return function (component) {            
            if (unique) {
                unique.close();
                unique = null;
            }
            resolve(component)
        }
    }
}