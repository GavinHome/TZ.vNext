import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormType, TzSuperFormGroup } from "../../TzSuperForm/TzSuperFormSchema";
import TzSuperForm from "../../TzSuperForm";
import Guid from "../../../common/Guid";

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

    get group() {
        return this.data
    }

    // 新增
    handleAdd(res) {
        this.selectIndex = res.newIndex       

        var data = this.list[this.selectIndex]
        console.log(data)
        if (data) {
            var rowIndex = -1;
            this.data.rows.forEach((row, index) => {
                if (this.arraySum(row.fields.map(x => x.cols ? x.cols : 1)) < 3) {
                    rowIndex = index;
                }
            })

            if (rowIndex === -1) {
                this.data.rows.push({
                    key: Guid.newGuid().toString(),
                    name: Guid.newGuid().toString(),
                    fields: []
                })

                rowIndex = this.data.rows.length - 1;
            }

            //this.$emit('newFormItem', data, this.data.key, rowIndex)
            this.data.rows[rowIndex].fields.push({
                key: data.field,
                name: data.field,
                label: data.label,
                type:  data.type,
                title: data.label,
                isOnlyDisplay: false,
                format: null,
                options: null,
                cols: 1,
                attrs: null,
                slots: null,
            })
            
            this.$emit("selectedFormItem", data)
            this.selectKey = data.field
        }
    }

    // 删除
    handleDelete(key) {
        debugger
        var index = -1;
        this.list.forEach((item, i) => {
            if (item.field == key) {
                index = i
            }
        })

        if (index != -1) {
            this.list.splice(index, 1)
        }

        this.data.rows.forEach((row, a) => {
            row.fields.forEach((field, c) => {
                if (field.key === key) {
                    row.fields.splice(c, 1)
                }
            })
        })
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
    handleFormItemClick(key) {
        this.selectIndex = key
        this.selectKey = key

        var index = -1;
        this.list.forEach((item, i) => {
            if (item.field == key) {
                index = i
            }
        })

        this.$emit("selectedFormItem", this.list[index])
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

    removeGroup(key) {
        this.$emit('remove-group', key)
    }    

    arraySum(array) {
        var total = 0,
            len = array.length
        for (var i = 0; i < len; i++) {
            total += array[i];
        }

        return total;
    };
}