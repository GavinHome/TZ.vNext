import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperFormType, TzSuperFormRow } from "../../TzSuperForm/TzSuperFormSchema";
import { TzSuperFormField } from "../BuilderFormComps";
import Guid from "../../../common/Guid";

@Component({
    props: ["fields"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        draggable: require('vuedraggable'),
        TzSuperTextarea: require('../../TzSuperForm/TzSuperTextarea.vue.html'),
        TzSuperNumber: require('../../TzSuperForm/TzSuperNumber.vue.html'),
        TzSuperSelect: require('../../TzSuperForm/TzSuperSelect.vue.html'),
        TzSuperEmployeeGrid: require('../../TzSuperForm/TzSuperEmployeeGrid.vue.html'),
    }
})
export default class BuilderAppFormDraggleContainer extends Vue {
    @Prop() fields!: any
    selectIndex: number = 0
    selectKey: string = ''
    list: any = []

    formData: any = {
    }

    // 新增
    handleAdd(res) {
        this.selectIndex = res.newIndex
        this.selectKey = this.list[this.selectIndex].key
        this.$emit("selectedFormItem", this.list[this.selectIndex])
    }

    // 删除
    handleDelete(key) {
        if (this.list[this.selectIndex].key === key) {
            this.list.splice(this.selectIndex, 1)

            this.fields.forEach((field, c) => {
                if (field.key === key) {
                    this.fields.splice(c, 1)
                }
            })
        }
    }

    handleChange(res) {
        console.log('handleMoveEnd:' + JSON.stringify(res.element))
    }

    handleMove(e) {
        console.log('handleMoveEnd:' + e)
    }

    // 移动开始
    handleMoveStart(res) {
        console.log('handleMoveStart:' + res.oldIndex)
        this.selectIndex = res.oldIndex
    }

    // 移动结束
    handleMoveEnd(res) {
        console.log('handleMoveEnd:' + res.newIndex)
        this.selectIndex = res.newIndex
    }

    // 点击选中
    handleFormItemClick(key) {
        this.selectKey = key
        this.list.filter(x => x).forEach((item, i) => {
            if (item.key === key) {
                this.selectIndex = i
            }
        })

        this.$emit("selectedFormItem", this.list[this.selectIndex])
    }

    getComponentName(type: TzSuperFormType) {
        let eleBuiltInNames: string[] = ["input"];
        let tzBuiltInNames: string[] = ["textarea", "number", "select", "employee-grid"];
        if (eleBuiltInNames.includes(type)) {
            // element 内置组件
            return 'el-' + type
        } else if (tzBuiltInNames.includes(type)) {
            // tz 内置组件
            return 'tz-super-' + type
        } else {
            // 外部组件
            return type
        }
    }

    @Watch('list', { immediate: true, deep: true })
    onListChanged(val: string, oldVal: string) {
        this.fields.splice(0, this.fields.length)
        var fields = this.getFields(this.list)
        fields.forEach(r => {
            this.fields.push(r);
        })
    }


    getFields(data): TzSuperFormField[] {
        var fields: TzSuperFormField[] = [];

        if (data.length && data.length > 0) {
            data.filter(item => item).forEach(item => {
                fields.push({
                    key: item.key,
                    name: item.name,
                    label: item.label,
                    type: item.type,
                    title: item.title,
                    isOnlyDisplay: item.isOnlyDisplay,
                    format: item.format,
                    options: item.options,
                    cols: item.cols,
                    attrs: item.attrs,
                    slots: item.slots,
                })
            });
        }

        return fields;
    }
}