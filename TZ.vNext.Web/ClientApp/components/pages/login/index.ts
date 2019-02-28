import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import spin from '../../common/TzSpin'
import StoreCache from '../../common/TzStoreCache'
import config from "../../../config"

import { Main, Container, Form, Footer, Card, Input, Button, FormItem, Row, Col, Checkbox, Message } from 'element-ui'
import { TzApiConst, TzMessageConst, TzRuleMsgConst } from '../../common/TzCommonConst';

Vue.use(Main)
Vue.use(Container)
Vue.use(Form)
Vue.use(Footer)
Vue.use(Card)
Vue.use(Input)
Vue.use(Button)
Vue.use(FormItem)
Vue.use(Row)
Vue.use(Col)
Vue.use(Checkbox)

@Component({
    props: ['redirect'],
})
export default class LoginComponent extends Vue {

    max_fetch_retry_number = 10;

    model = {
        UserName: '201406348',
        Password: '1',
        IsAutoLogin: false
    }

    rules = {
        UserName: [
            { required: true, message: TzRuleMsgConst.LOGIN_USERNAME, trigger: 'blur' }
        ],
        Password: [
            { required: true, message: TzRuleMsgConst.LOGIN_PASSWORD, trigger: 'blur' }
        ]
    }

    @Prop({ default: '/home' }) redirect!: string;

    created() {
        this.autoLogin(this.max_fetch_retry_number)
    }

    autoAuth(data: string) {
        if (!data) {
            window.location.href = config.CaBaseUrl + "/Login/Index?subsystemUrl=" + config.SalaryUrl
            return;
        }

        return fetch(TzApiConst.TOKEN, {
            method: 'post',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: "authentication=" + encodeURIComponent(data)
        }).then(res => {
            if (res && res.status === 200) {
                res.json().then((data) => {
                    this.LoginSuccess(data);
                });
            } else {
                throw new Error(TzMessageConst.AUTO_LOGIN_FAIL_MESSAGE)
            }
        })
    }

    autoLogin(n: number) {
        if (!config.EnableCA) {
            return;
        }

        spin.show();
        fetch(config.CaBaseUrl + "/Login/AuthGateway", {
            credentials: "include",
            method: 'POST',
        }).then(res => res.json()).then(data => this.autoAuth(data)).then(spin.resolve(() => { })).catch(err => {
            console.log(err)
            if (n === 1) {
                spin.close()
                Message.error(TzMessageConst.AUTO_LOGIN_FAIL_MESSAGE)
                return;
            }

            this.autoLogin(n - 1)
        })
    }

    submitForm(formName) {
        (this.$refs[formName] as any).validate((valid) => {
            if (valid) {
                const searchParams = Object.keys(this.model).map((key) => {
                    return encodeURIComponent(key) + '=' + encodeURIComponent(this.model[key]);
                }).join('&');
                spin.show()
                fetch(TzApiConst.TOKEN, {
                    method: 'post',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    body: searchParams
                }).then(res => {
                    if (res && res.status === 200) {
                        res.json().then((data) => {
                            this.LoginSuccess(data);
                        });
                    } else {
                        Message.error(TzMessageConst.LOGIN_FAIL_MESSAGE)
                    }
                }).then(spin.resolve(() => { }))
            } else {
                return false;
            }
        });
    }

    private LoginSuccess(data: any) {
        let cache = new StoreCache('auth');
        cache.set('token', data.access_token);
        //cache.set('expirationTime', new Date((new Date()).getTime() + 1000*data.expires_in));
        var now = new Date();
        now.setSeconds(now.getSeconds() + data.expires_in);
        cache.set('expirationTime', now);
        cache.set('name', data.user_name);
        cache.set('functions', data.user_func);
        this.$router.replace(this.redirect ? this.redirect : '/home');
        Message.success(TzMessageConst.LOGIN_SUCCESS_MESSAGE);
    }
}
