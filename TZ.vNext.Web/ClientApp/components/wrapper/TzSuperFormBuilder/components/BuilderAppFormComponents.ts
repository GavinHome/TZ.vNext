import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'
import { components } from "../BuilderFormComps"
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
    console.log('comp:' + JSON.stringify(data))
    return {
      key: data.key,
      name: data.name,
      label: data.label,
      type: data.type,
      title: data.title,
      isOnlyDisplay: data.isOnlyDisplay,
      format: data.format,
      options: data.options,
      cols: data.cols,
      attrs: data.attrs,
      slots: data.slots,
    }
  }
}