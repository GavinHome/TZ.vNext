import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';

import 'element-ui/lib/theme-chalk/index.css'
import { Card, Scrollbar } from 'element-ui'
Vue.use(Card)
Vue.use(Scrollbar)

import { components } from "../BuilderFormComps"
import { TzSuperFormType } from "../../TzSuperForm/TzSuperFormSchema";

@Component({
  props: [],
  components: {
    draggable: require('vuedraggable')
  }
})
export default class BuilderAppFormComponents extends Vue {

  components: any = components
  globalId: number = 0
  // formDesc: any = {
  //     field: {
  //       type: 'input',
  //       label: '数据字段'
  //     },
  //     label: {
  //       type: 'input',
  //       label: '标签'
  //     },
  //     layout: {
  //       type: 'slider',
  //       label: '宽度',
  //       default: 24,
  //       attrs: {
  //         min: 1,
  //         max: 24,
  //         'format-tooltip' (val) {
  //           return `${val} / 24`
  //         }
  //       }
  //     },
  //     default: {
  //       type: 'codemirror',
  //       label: '默认值'
  //     },
  //     tip: {
  //       type: 'input',
  //       label: '表单项提示'
  //     }
  //   }

  formDesc: any = [
    {
      key: "field",
      name: "field",
      label: "标识：",
      type: TzSuperFormType.Input,
      title: "标识",
      isOnlyDisplay: false,
      format: null,
      options: null,
      cols: 3,
      attrs: null,
      slots: null,
    },
    {
      key: "label",
      name: "label",
      label: "标签：",
      type: TzSuperFormType.Input,
      title: "标签",
      isOnlyDisplay: false,
      format: null,
      options: null,
      cols: 3,
      attrs: null,
      slots: null,
    },
    {
      key: "cols",
      name: "cols",
      label: "所占宽度：",
      type: TzSuperFormType.Select,
      title: "所占宽度",
      isOnlyDisplay: false,
      format: null,
      options: [
        { text: "1列", value: 1 },
        { text: "2列", value: 2 },
        { text: "3列", value: 3 }
      ],
      cols: 3,
      attrs: null,
      slots: null,
    }
  ]

  addFormItem(data) {
    data.field = 'key_' + this.globalId++
    data.formDesc = this.formDesc
    data.formData = {
      field: data.field,
      label: data.label,
      cols: 1,
      type: data.type
    }
    console.log(data)
    return data
  }
}