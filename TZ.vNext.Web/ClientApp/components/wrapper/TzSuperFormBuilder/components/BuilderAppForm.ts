import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup } from "../../TzSuperForm/TzSuperFormSchema";
import Guid from "../../../common/Guid";

@Component({
    props: ["formAttr"],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html"),
        draggable: require('vuedraggable'),
        TzSuperTextarea: require('../../TzSuperForm/TzSuperTextarea.vue.html'),
        TzSuperNumber: require('../../TzSuperForm/TzSuperNumber.vue.html'),
        TzSuperSelect: require('../../TzSuperForm/TzSuperSelect.vue.html'),
        TzSuperEmployeeGrid: require('../../TzSuperForm/TzSuperEmployeeGrid.vue.html'),
        AppFormGroupItem: require('./BuilderAppFormGroupItem.vue.html'),       
        AppFormHeader: require('./BuilderAppFormHeader.vue.html'),
    }
})
export default class BuilderAppForm extends Vue {
    @Prop() formAttr!: any

    groups: TzSuperFormGroup[] = [
        {
            key: "basic",
            name: "basic",
            title: "基础信息",
            isCollapsed: false,
            rows: [
                {
                    key: "basic-row1",
                    name: "basic-row1",
                    fields: [
                    ]
                }
            ]
        }
    ]

    formData: any = {
    }

    rules = {
    }

    selectIndex: number = 0
    selectKey: string = ''
    list: any = []

    get ActiveCollapses() {
        return this.groups.filter(x => !x.isCollapsed).map(x => x.name)
    }

    handleSubmit(data) {
        console.log(data)
        return Promise.resolve(data)
    }

    handleSuccess(response) {
        console.log(response)
        this.$message.success('创建成功')
    }

    handleError(response) {
        console.log(response)
        this.$message.success('失败')
    }

    handleEnd(response) {
        console.log(response)
        this.$message.success('处理结束')
    }

    handleRequest(response) {
        console.log(response)
        this.$message.success('自定义处理')
    }

    addGroup() {
        this.groups.push(this.newGroup(Guid.newGuid().toString()))
    }

    removeGroup(key) {
        this.groups.forEach((group, a) => {
            if (group.key === key) {
                this.groups.splice(a, 1)
            }
        })
    }

    newGroup(key) {
        return {
            key: key,
            name: key,
            title: "分组信息",
            isCollapsed: false,
            rows: [
                this.newRow(Guid.newGuid().toString())
            ],
        }
    }

    newRow(key) {
        return {
            key: key,
            name: key,
            fields: [

            ]
        }
    }

    handleNewFormItem(data, groupkey, rowindex) {
        this.groups.forEach((g, i) => {

            if (g.key == groupkey) {
                g.rows[rowindex].fields.push({
                    key: data.field,
                    name: data.field,
                    label: data.label,
                    type: data.type,
                    title: data.label,
                    isOnlyDisplay: false,
                    format: null,
                    options: null,
                    cols: 1,
                    attrs: null,
                    slots: null,
                })
            }
        })
    }

    handleSelectFormItem(data) {
        this.$emit("selectedFormItem", data)
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
}