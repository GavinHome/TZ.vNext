import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormType } from "../../TzSuperForm/TzSuperFormSchema";
import { TzSuperFormField } from "../BuilderFormComps";
import { getComponentName } from "../../TzSuperForm/TzSuperFunc";

@Component({
    props: ["fields"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        draggable: require('vuedraggable'),
        TzSuperTextarea: require('../../TzSuperForm/TzSuperTextarea.vue.html'),
        TzSuperNumber: require('../../TzSuperForm/TzSuperNumber.vue.html'),
        TzSuperSelect: require('../../TzSuperForm/TzSuperSelect.vue.html'),
        TzSuperEmployeeGrid: require('../../TzSuperForm/TzSuperEmployeeGrid.vue.html'),
        TzSuperText: require('../../TzSuperForm/TzSuperText.vue.html'),
        TzSuperSwitch: require('../../TzSuperForm/components/TzSuperSwitch.vue.html')
    },
    watch: {
        list: {
            handler: (newProp, oldProp) => {

            },
            deep: true,
            immediate: false
        }
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

        this.fields.splice(0, this.fields.length)
        var fields = this.getFields(this.list)
        fields.forEach(r => {
            this.fields.push(r);
        })

        this.$emit("selectedFormItem", this.list[this.selectIndex])
    }

    // 删除
    handleDelete(key) {
        if (this.list[this.selectIndex].key === key) {
            this.list.splice(this.selectIndex, 1)

            this.fields.forEach((field, c) => {
                if (field.key === key) {
                    this.fields.splice(c, 1)
                    this.$emit("delete-field", field.name)
                }
            })
        }

        this.$emit("selectedFormItem", null)
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
        return getComponentName(type)
    }

    // @Watch('list', { immediate: true, deep: true })
    // onListChanged(val: any, oldVal: any) {
    //     this.fields.splice(0, this.fields.length)
    //     var fields = this.getFields(this.list)
    //     fields.forEach(r => {
    //         this.fields.push(r);
    //     })
    // }

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