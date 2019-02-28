import './css/site.css'
import 'bootstrap'
import Vue from 'vue'
import VueRouter from 'vue-router'
import router from './router'
import 'font-awesome/css/font-awesome.min.css'
import fetchIntercept from "./http/FetchIntercept"
import log from './log';

fetchIntercept.register()
Vue.use(VueRouter)
Vue.use(log, { entryName: '薪酬系統' });

new Vue({
    el: '#app-root',
    //router: new VueRouter({ mode: 'history', routes: routes }),
    router: router,
    render: h => h(require('./components/app/app.vue.html'))
});


