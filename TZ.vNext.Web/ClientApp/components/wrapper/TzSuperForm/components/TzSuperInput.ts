import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Input } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Input)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperRate extends Vue {
    @Prop() desc!: any
    @Prop() value!: any

    newValue: any = this.value

    update() {
        this.$emit('change', this.newValue)
    }
}
