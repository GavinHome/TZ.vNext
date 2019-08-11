import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Select, Option } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Select)
Vue.use(Option)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperSelect extends Vue {
    @Prop() desc!: any

    get options() {
        return this.desc && Array.isArray(this.desc.options)
            ? this.desc.options
            : []
    }

    update(value) {
        this.$emit('change', value)
    }
}
