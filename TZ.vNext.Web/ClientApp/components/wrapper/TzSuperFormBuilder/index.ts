import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';

import 'element-ui/lib/theme-chalk/index.css'
import { Container, Aside, Header, Main, TabPane, Tabs } from 'element-ui'
Vue.use(Container)
Vue.use(Aside)
Vue.use(Header)
Vue.use(Main)
Vue.use(TabPane)
Vue.use(Tabs)

@Component({
    props: [],
    components: {
        AppForm: require('./components/BuilderAppForm.vue.html'),
        AppFormItemProperty: require('./components/BuilderAppFormItemProperty.vue.html'),
        AppFormProperty: require('./components/BuilderAppFormProperty.vue.html'),
        AppFormComponents: require('./components/BuilderAppFormComponents.vue.html'),
    }
})
export default class TzSuperFormBuilder extends Vue {
    activeTab: number = 0
    selectFormItem: any = {}
    formAttr: any = {}

    handleFormPropertyChange(attr) {
        this.formAttr = attr
    }

    handleSelectedFormItem(item) {
        if(item) {
            this.selectFormItem = item
        }
    }
}