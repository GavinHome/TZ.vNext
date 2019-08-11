import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Input } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Input)

@Component({
    props: ["value"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperTextarea extends Vue {
    update(value) {
        this.$emit('change', value)
    }
}
