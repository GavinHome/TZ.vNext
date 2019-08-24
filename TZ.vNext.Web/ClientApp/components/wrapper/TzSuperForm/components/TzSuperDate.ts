import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { DatePicker } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(DatePicker)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperDate extends Vue {
    @Prop() desc!: any
    @Prop() value!: any

    newValue: any = this.value

    update() {
        this.$emit('change', this.newValue)
    }
}
