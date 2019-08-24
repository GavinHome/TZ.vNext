import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { TimeSelect, Scrollbar } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(TimeSelect)
Vue.use(Scrollbar)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperTime extends Vue {
    @Prop() desc!: any
    @Prop() value!: any

    newValue: any = this.value

    update() {
        this.$emit('change', this.newValue)
    }

    get options() {
        return this.desc && this.desc.options
            ? this.desc.options
            : {
                start: '08:30',
                step: '00:30',
                end: '18:00'
            }
    }
}
