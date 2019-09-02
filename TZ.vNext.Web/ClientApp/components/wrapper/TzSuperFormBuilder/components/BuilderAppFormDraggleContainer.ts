import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormType, getComponentName } from "../../TzSuperForm/schema/TzSuperFormSchema";
import { TzSuperFormField } from "../BuilderFormComps";

@Component({
    props: ["fields"],
    components: {
        draggable: require('vuedraggable'),
        TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        TzSuperTextarea: require('../../TzSuperForm/components/TzSuperTextarea.vue.html'),
        TzSuperNumber: require('../../TzSuperForm/components/TzSuperNumber.vue.html'),
        TzSuperSelect: require('../../TzSuperForm/components/TzSuperSelect.vue.html'),
        TzSuperText: require('../../TzSuperForm/components/TzSuperText.vue.html'),
        TzSuperSwitch: require('../../TzSuperForm/components/TzSuperSwitch.vue.html'),
        TzSuperDialog: require('../../TzSuperForm/components/TzSuperDialog.vue.html'),
        TzSuperShell: require('../../TzSuperForm/components/TzSuperShell.vue.html'),
        TzSuperTime: require('../../TzSuperForm/components/TzSuperTime.vue.html'),
        TzSuperTag: require('../../TzSuperForm/components/TzSuperTag.vue.html'),
        TzSuperRadio: require('../../TzSuperForm/components/TzSuperRadio.vue.html'),
        TzSuperCheckbox: require('../../TzSuperForm/components/TzSuperCheckbox.vue.html'),
        TzSuperDate: require('../../TzSuperForm/components/TzSuperDate.vue.html'),
        TzSuperDatetime: require('../../TzSuperForm/components/TzSuperDatetime.vue.html'),
        TzSuperRate: require('../../TzSuperForm/components/TzSuperRate.vue.html'),
        TzSuperSlider: require('../../TzSuperForm/components/TzSuperSlider.vue.html'),
        TzSuperPassword: require('../../TzSuperForm/components/TzSuperPassword.vue.html'),
        TzSuperYear: require('../../TzSuperForm/components/TzSuperYear.vue.html'),
        TzSuperMonth: require('../../TzSuperForm/components/TzSuperMonth.vue.html'),
        TzSuperInput: require('../../TzSuperForm/components/TzSuperInput.vue.html'),
        TzSuperDaterange: require('../../TzSuperForm/components/TzSuperDateRange.vue.html'),
        TzSuperMonthrange: require('../../TzSuperForm/components/TzSuperMonthRange.vue.html'),
        TzSuperDatetimerange: require('../../TzSuperForm/components/TzSuperDateTimeRange.vue.html'),
        TzSuperDates: require('../../TzSuperForm/components/TzSuperDates.vue.html'),
        TzSuperWeek: require('../../TzSuperForm/components/TzSuperWeek.vue.html'),
        TzSuperTimerange: require('../../TzSuperForm/components/TzSuperTimeRange.vue.html'),
        TzSuperButton: require('../../TzSuperForm/components/TzSuperButton.vue.html'),
        TzSuperAutocomplete: require('../../TzSuperForm/components/TzSuperAutocomplete.vue.html'),
        TzSuperGrid: require('../../TzSuperForm/components/TzSuperGrid.vue.html')
    }
})
export default class BuilderAppFormDraggleContainer extends Vue {
    @Prop() fields!: TzSuperFormField[]

    selectIndex: number = 0

    formData: any = {
    }

    // 删除
    handleDelete(index) {
        this.fields.splice(index, 1)
        if (index >= this.fields.length) {
            this.selectIndex = this.fields.length - 1
        }

        this.$nextTick(() => {
            this.$emit("formChanged")
        })
    }

    // 新增
    handleAdd(res) {
        this.selectIndex = res.newIndex
        this.$emit("selectedFormItem", this.fields[this.selectIndex])
    }

    // 移动开始
    handleMoveStart(res) {
        this.selectIndex = res.oldIndex
    }

    // 移动结束
    handleMoveEnd(res) {
        this.selectIndex = res.newIndex
    }

    // 点击选中
    handleFormItemClick(index) {
        this.selectIndex = index
        this.$emit("selectedFormItem", this.fields[this.selectIndex])
    }

    getComponentName(type: TzSuperFormType) {
        return getComponentName(type)
    }

    changed(e) {
        if (e.added) {
            var lastKey = this.checkName()
            e.added.element.name = e.added.element.name.replace("{0}", lastKey)
        }
    }

    checkName(): string {
        var names = this.fields.filter((x: TzSuperFormField) => x && x.name && x.name.startsWith("field_")).map(x => x.name).sort().reverse();
        var max = names.length
        while (names.filter(x => x === "field_" + max).length > 0) {
            max = max + 1
        }

        return max.toString()
    }
}