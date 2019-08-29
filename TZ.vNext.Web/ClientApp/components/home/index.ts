import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import { Main, Aside, Container, Header, Dropdown, DropdownItem, Table, TableColumn, Footer, Scrollbar } from 'element-ui'

Vue.use(Main)
Vue.use(Aside)
Vue.use(Container)
Vue.use(Header)
Vue.use(Dropdown)
Vue.use(DropdownItem)
Vue.use(Table)
Vue.use(TableColumn)
Vue.use(Footer)
Vue.use(Scrollbar)

@Component({
    components: {
        MenuComponent: require('../navmenu/sidemenu.vue.html'),
        NavbarComponent: require('../navmenu/navbar.vue.html'),
        breadcrumbs: require('../navmenu/breadcrumbs.vue.html'),
    }
})
export default class AppComponent extends Vue {
    isCollapse = false

    listenCollapsed(isCollapse) {
        this.isCollapse = isCollapse
    }
}
