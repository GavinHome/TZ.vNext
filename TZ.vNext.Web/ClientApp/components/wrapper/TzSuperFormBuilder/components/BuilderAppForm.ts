import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup } from "../../TzSuperForm/schema/TzSuperFormSchema";

@Component({
    props: ["form", "formAttr", "rules", "formData"],
    components: {
        AppFormGroupItem: require('./BuilderAppFormGroupItem.vue.html'),
        AppFormHeader: require('./BuilderAppFormHeader.vue.html'),
    }
})
export default class BuilderAppForm extends Vue {
    @Prop() form!: TzSuperFormGroup[]
    @Prop() formAttr!: any
    @Prop() rules!: any
    @Prop() formData!: any

    selectGroupKey: number = 0

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

    removeGroup(key: any) {
        this.$emit('remove-group', key)
    }
    
    handleDeleteField(data) {
        this.$emit('delete-field', data)
    }

    handleGroupItemClick(key) {
        this.selectGroupKey = key;
    }
}