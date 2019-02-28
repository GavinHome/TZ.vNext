import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import { ISelect } from "../schemas/SelectItemSchema";
import { EnumHelper, EnumConstType } from "../common/Enums";

import { Select, Option } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";

Vue.use(Select);
Vue.use(Option);

@Component({
  props: ["value", "enumType", "placeholder"],
  model: {
    prop: "value",
    event: "change"
  }
})
export default class TzEnumSelect extends Vue {
  options: ISelect[] = [];
  @Prop() enumType?: EnumConstType;

  created() {
    if (!this.enumType) {
      throw new Error("enum type error");
    }

    this.options = EnumHelper.toEnumOptions(this.enumType);
  }

  get getOptions() {
    return this.options;
  }

  update(value) {
    this.$emit("change", value);
  }
}
