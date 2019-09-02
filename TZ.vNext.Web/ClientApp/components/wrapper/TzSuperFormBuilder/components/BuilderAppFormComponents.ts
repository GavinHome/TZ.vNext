import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'
import { components, TzSuperFormField } from "../BuilderFormComps"
import 'element-ui/lib/theme-chalk/index.css'
import { Card, Scrollbar } from 'element-ui'
import Guid from "../../../common/Guid";

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
    // var id = this.globalId++;
    // data.key = 'key_' + id
    // data.name = 'field_' + id
    data.key = Guid.newGuid().toString();
    data.name = 'field_{0}'

    return {
      key: data.key,
      name: data.name,
      label: data.label,
      type: data.type,
      title: data.title,
      format: data.format,
      options: data.options,
      cols: data.cols,
      attrs: data.attrs,
      slots: data.slots,
    }
  }
}