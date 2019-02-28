import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import 'element-ui/lib/theme-chalk/index.css'
import { Menu, Submenu, MenuItemGroup, MenuItem, RadioGroup, RadioButton } from 'element-ui'
import AuthFunction from '../common/TzCommonFunc'


Vue.use(Menu)
Vue.use(Submenu)
Vue.use(MenuItemGroup)
Vue.use(MenuItem)
Vue.use(RadioGroup)
Vue.use(RadioButton)

@Component({
    components: {
        TreeMenu: require('./treeMenu.vue.html'),
        ShortCuts: require('./shortcuts.vue.html')
    },
    props: ['isCollapsed']
})

export default class SideMenuComponent extends Vue {
    isCollapse = false

    @Prop({ default: false }) isCollapsed!: boolean;

    created() {
        this.isCollapse = this.isCollapsed
    }

    handleOpen(key: string, keyPath: string) {
        console.log(key, keyPath);
    }

    handleClose(key: string, keyPath: string) {
        console.log(key, keyPath);
    }

    handleCollapse() {
        this.isCollapse = !this.isCollapse
        this.$emit("listenCollapsed", this.isCollapse)
    }

    get matchRouter() {
        //.filter(x => String.isNullOrEmpty(x.meta.functionId) || (!String.isNullOrEmpty(x.meta.functionId) && x.meta.functionId === '1' ))
        return (this.$router as any).options.routes.find(x => x.name === 'home').children.filter(AuthFunction)  //.filter(x => x.path != '*')
    }

    get navigation() {
        return this.$route.meta.parent ? this.$route.meta.parent : this.$route.name;
    }
}