import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'
import { components, TzSuperFormField } from "../BuilderFormComps"
import 'element-ui/lib/theme-chalk/index.css'
import { Card, Scrollbar } from 'element-ui'

Vue.use(Card)
Vue.use(Scrollbar)

@Component({
  props: [],
  components: {
    draggable: require('vuedraggable')
  }
})
export default class BuilderAppFormComponents extends Vue {

  components: any = components
  globalId: number = 0

  cloneComponent(data) {
    var id = this.globalId++;
    data.key = 'key_' + id
    data.name = 'field_' + id

    var options = {
      "remote": "/api/Employees/GridQueryEmployees",
      "schema": {
        "Id": { "filterable": false, "type": "string" },
        "Name": { "filterable": true, "type": "string" },
        "Code": { "filterable": true, "type": "string" },
        "CompanyTypeName": { "filterable": true, "type": "string" },
        "OrganizationName": { "filterable": true, "type": "string" }
      },
      "map": {
        "value": "Name",
        "ext": "Code",
        "key": "Id"
      }
    }

    return {
      key: data.key,
      name: data.name,
      label: data.label + id,
      type: data.type,
      title: data.title,
      format: data.format,
      options: data.type === "autocomplete" ? options : data.options,
      cols: data.cols,
      attrs: data.attrs,
      slots: data.slots,
    }
  }
}