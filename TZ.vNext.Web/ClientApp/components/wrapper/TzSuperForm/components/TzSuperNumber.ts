import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { InputNumber } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(InputNumber)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperNumber extends Vue {
    @Prop() desc!: any
    update(value) {
        this.$emit('change', value)
    }
}
