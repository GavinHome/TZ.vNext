import Vue from "vue"
import { Component, Prop } from "vue-property-decorator"

@Component({
    props: ["id"],
    components: {
        
    }
})
export default class EmployeeCreateComponent extends Vue {
    @Prop() id!: string
}