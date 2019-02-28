import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import 'element-ui/lib/theme-chalk/index.css'
import { MenuItem, Submenu } from 'element-ui'
import AuthFunction from '../common/TzCommonFunc';

Vue.use(MenuItem)
Vue.use(Submenu)

@Component({
    props: ['nkey', 'name', 'nodes', 'title', 'icon', 'isCollapse'],
    name: 'tree-menu'
})
export default class TreeMenuComponent extends Vue {
    @Prop({ default: [] }) nodes!: any[]

    get getNodes() {
        return this.nodes.filter(AuthFunction)
    }
}