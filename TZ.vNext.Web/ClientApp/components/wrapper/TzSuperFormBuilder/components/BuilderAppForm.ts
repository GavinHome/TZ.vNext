import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup } from "../../TzSuperForm/TzSuperFormSchema";

@Component({
    props: ["form", "formAttr"],
    components: {
        //TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        // draggable: require('vuedraggable'),
        // TzSuperTextarea: require('../../TzSuperForm/TzSuperTextarea.vue.html'),
        // TzSuperNumber: require('../../TzSuperForm/TzSuperNumber.vue.html'),
        // TzSuperSelect: require('../../TzSuperForm/TzSuperSelect.vue.html'),
        // TzSuperEmployeeGrid: require('../../TzSuperForm/TzSuperEmployeeGrid.vue.html'),
        AppFormGroupItem: require('./BuilderAppFormGroupItem.vue.html'),
        AppFormHeader: require('./BuilderAppFormHeader.vue.html'),
    }
})
export default class BuilderAppForm extends Vue {
    @Prop() form!: TzSuperFormGroup[]
    @Prop() formAttr!: any

    formData: any = {}
    rules = {}

    get activeCollapses() {
        return this.form.filter(x => !x.isCollapsed).map(x => x.name)
    }

    get showSubmitBtn() {
        return !this.formAttr.isHideSubmitBtn;
    }

    get showBackBtn() {
        return !this.formAttr.isHideBackBtn;
    }

    get submitBtnText() {
        return this.formAttr.submitBtnText;
    }

    get backBtnText() {
        return this.formAttr.backBtnText;
    }

    get labelWidth() {
        return this.formAttr.labelWidth
    }

    handleValidateForm(e) {
        console.log(e)
    }

    handleSelectFormItem(data) {
        this.$emit("selectedFormItem", data)
    }

    removeGroup(key) {
        this.$emit('remove-group', key)
    }
}