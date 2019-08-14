
import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Input, Button, Dialog } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Input)
Vue.use(Button)
Vue.use(Dialog)

@Component({
    props: ["value", "desc"],
    // model: {
    //     prop: 'value',
    //     event: 'change'
    // },
    components: {
    }
})
export default class TzSuperDialog extends Vue {
    @Prop() value!: string
    @Prop() desc!: any

    isShow: boolean = false

    created() {
        this.desc.slots.forEach(ele => {
            Vue.component(ele.type, ele.component)
        });
    }

    get newValue(){
        return this.value
    }
}
