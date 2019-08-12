


import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Input, Button } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Input)
Vue.use(Button)

import TzEmployee from "../TzEmployee";
import { TzDialog } from '../../common/TzDialog';

@Component({
    props: ["value"],
    model: {
        prop: 'value',
        event: 'change'
    },
    components: {
        TzEmployee: require("../../wrapper/TzEmployee.vue.html"),
    }
})
export default class TzSuperEmployeeGrid extends Vue {
    @Prop() value!: string

    newValue: string = this.value

    selectEmployee() {
        new TzDialog(
            this.$createElement,
            TzEmployee,
            "人员信息",
            (d: any): any => {
                this.newValue = d.Name
                this.update(d.Id)
            }
        );
    }

    update(value) {
        this.$emit('change', value)
    }
}
