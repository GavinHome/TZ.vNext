import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperFormType } from "../../TzSuperForm/TzSuperFormSchema";

@Component({
    props: ["data"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        draggable: require('vuedraggable'),
        TzSuperTextarea: require('../../TzSuperForm/TzSuperTextarea.vue.html'),
        TzSuperNumber: require('../../TzSuperForm/TzSuperNumber.vue.html'),
        TzSuperSelect: require('../../TzSuperForm/TzSuperSelect.vue.html'),
        TzSuperEmployeeGrid: require('../../TzSuperForm/TzSuperEmployeeGrid.vue.html'),
    }
})
export default class BuilderAppFormGroupItem extends Vue {
    @Prop() data!: any
    selectIndex: number = 0
    selectKey: string = ''
    list: any = []
    
    formData: any = {
    }

    // get group() {
    //     return this.data
    // }

    get rows() {
        return this.data.rows
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

            this.data.rows.forEach((row, a) => {
                row.fields.forEach((field, c) => {
                    if (field.key === key) {
                        row.fields.splice(c, 1)
                    }
                })
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
        this.list.forEach((item, i) => {
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
        this.$emit("change", this.data.key, this.list)
        console.log('list:' + JSON.stringify(this.list))
    }
}