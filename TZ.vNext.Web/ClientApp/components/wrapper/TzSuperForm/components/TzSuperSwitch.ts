import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Switch } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Switch)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperSwitch extends Vue {
    @Prop() desc!: any
    update(value) {
        this.$emit('change', value)
    }
}
