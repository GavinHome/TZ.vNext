import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import 'element-ui/lib/theme-chalk/index.css'
import { Button, Popover, Menu, Submenu, MenuItem, } from 'element-ui'

Vue.use(Button)
Vue.use(Popover)
Vue.use(Menu)
Vue.use(Submenu)
Vue.use(MenuItem)

@Component({
    props: ['collapse']
})

export default class ShortCutsComponent extends Vue {
    @Prop({ default: true }) collapse!: boolean;

    get isCollapsed() {
        return this.collapse;
    }

    goto(path) {
        this.$router.push({ path: path })
    }
}