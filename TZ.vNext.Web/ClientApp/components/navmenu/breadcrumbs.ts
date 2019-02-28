import Vue from 'vue';
import { Component } from 'vue-property-decorator'
import 'element-ui/lib/theme-chalk/index.css'
import Breabcrumbs from '../common/TzBreadcrumbs'
import { Breadcrumb, BreadcrumbItem } from 'element-ui'

Vue.use(Breadcrumb)
Vue.use(BreadcrumbItem)
Vue.use(Breabcrumbs)

@Component
export default class TzBreadcrumbsComponent extends Vue {
}