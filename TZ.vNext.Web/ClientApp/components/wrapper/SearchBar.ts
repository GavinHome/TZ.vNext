import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import 'element-ui/lib/theme-chalk/index.css'
import { Form, FormItem, Input, Button } from 'element-ui'
import kendoHelper from "../extension/KendoExtensions";
import customSearch, { CustomDataSource } from '../common/TzCustomSearch';
Vue.use(Form)
Vue.use(FormItem)
Vue.use(Input)
Vue.use(Button)

@Component({
    props: ['isSearchAll', 'value', 'schemaModelFields', 'mode'],
    components: {
    }
})

export default class SearchBarComponent extends Vue {
    @Prop() mode!: string
    query = ''

    search() {
        if (this.mode === 'custom') {
            let props = this.$props as any
            let schemaModelFields = this.query ? props.schemaModelFields : null
            customSearch.onSearch(props.value as CustomDataSource, schemaModelFields, this.query)
        } else {
            let props = this.$props as any
            let schemaModelFields = this.query ? props.schemaModelFields : null
            kendoHelper.onSearch(props.value, schemaModelFields, this.query)
        }
    }

    clear() {
        this.query = ''
        this.search()
    }

    clearText() {
        this.query = ''
    }

    enterSearch(e) {
        e.preventDefault()
        this.search()
    }
}