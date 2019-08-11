import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormType } from "../../TzSuperForm/TzSuperFormSchema";

@Component({
    props: ["formItem"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html")
    }
})
export default class BuilderAppFormProperty extends Vue {
    @Prop() formItem!: any

    get form() {
        if (this.formItem.formDesc) {
            var result: any = [
                {
                    key: "basic",
                    name: "basic",
                    title: "基础信息",
                    isCollapsed: false,
                    rows: [
                        {
                            key: "basic-row1",
                            name: "basic-row1",
                            fields: this.formItem.formDesc
                        }
                    ]
                }
            ]

            return result;
        }

        return []
    }

    get formData() {
        return this.formItem ? this.formItem.formData : {}
    }
}