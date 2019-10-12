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
Vue.use(log, { entryName: '业务表单设计系统' });

Vue.prototype.$eventHub = Vue.prototype.$eventHub || new Vue()

import AlbatroUI from "albatro-ui";
import "albatro-ui/lib/theme-albatro/index.css";
Vue.use(AlbatroUI);


new Vue({
    el: '#app-root',
    //router: new VueRouter({ mode: 'history', routes: routes }),
    router: router,
    render: h => h(require('./components/app/app.vue.html'))
});


