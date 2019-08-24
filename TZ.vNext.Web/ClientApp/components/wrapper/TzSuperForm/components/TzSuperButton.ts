import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Button } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Button)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperButton extends Vue {
    @Prop() desc!: any
    @Prop() value!: any
}
