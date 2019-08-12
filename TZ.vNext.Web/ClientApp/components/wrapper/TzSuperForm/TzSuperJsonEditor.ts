import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import VJsoneditor from 'v-jsoneditor' 
Vue.use(VJsoneditor)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    },
    components: {
        VJsoneditor
    }
})
export default class TzSuperJsonEditor extends Vue {
    @Prop() value!: any

    json: any = this.value

    update(value) {
        this.$emit('change', value)
    }

    onError() {

    }
}
