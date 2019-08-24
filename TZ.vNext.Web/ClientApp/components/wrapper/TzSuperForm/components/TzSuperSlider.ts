import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Slider, Tooltip } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Slider)
Vue.use(Tooltip)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperSlider extends Vue {
    @Prop() desc!: any
    @Prop() value!: number

    newValue: number = this.value

    update() {
        this.$emit('change', this.newValue)
    }
}
