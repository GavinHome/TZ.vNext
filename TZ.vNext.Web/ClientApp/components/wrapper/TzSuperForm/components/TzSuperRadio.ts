import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { RadioGroup, Radio } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(RadioGroup)
Vue.use(Radio)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperRadio extends Vue {
    @Prop() desc!: any
    @Prop() value!: any

    newValue: any = this.value

    update() {
        this.$emit('change', this.newValue)
    }

    get options() {
        return this.desc && this.desc.options
            ? this.desc.options
            : []
    }
}
