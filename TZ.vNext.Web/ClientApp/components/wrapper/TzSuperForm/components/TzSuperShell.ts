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
    },
    components: {
    }
})
export default class TzSuperShell extends Vue {
    @Prop() value!: string
    @Prop() desc!: any

    created() {
        this.desc.slots.forEach(ele => {
            Vue.component(ele.type, ele.component)
        });
    }
}
