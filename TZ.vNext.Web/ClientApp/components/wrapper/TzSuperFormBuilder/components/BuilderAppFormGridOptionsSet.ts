import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import 'element-ui/lib/theme-chalk/index.css';
import { Tabs } from 'element-ui';
import { TzFetch } from "../../../common/TzFetch";
import { FieldTypeEnum, EnumHelper, EnumConstType } from "../../../common/Enums";
import { TzSuperGridOptionSchema } from "../../TzSuperForm/schema/TzSuperFormSchema";
Vue.use(Tabs)

@Component({
    props: ["options"]
})
export default class BuilderAppFormOptionsSet extends Vue {
    @Prop() options!: TzSuperGridOptionSchema

    remoteChange() {
        TzFetch.Post(this.options.remote, {}).then((data: any) => {
            var fields = Object.keys(data.Data).map((key, index) => {
                return {
                    field: key,
                    title: data.Data[key].title,
                    filterable: true,
                    sortable: true,
                    editable: false,
                    menu: true,
                    type: FieldTypeEnum.String,
                    width: "10%",
                    index: index + 1
                }
            })
            
            this.options.schema.splice(0, this.options.schema.length)
            this.options.schema.push({
                field: "RowNumber",
                title: "序号",
                width: "8%",
                filterable: false,
                sortable: false,
                editable: false,
                menu: false,
                type: FieldTypeEnum.Number,
                index: 0
            })
            this.options.schema.concat(fields)
        }).catch(err => {
            this.options.schema.concat([
                {
                    field: "Name",
                    title: "名称",
                    filterable: true,
                    sortable: true,
                    editable: false,
                    menu: true,
                    type: FieldTypeEnum.String,
                    width: "22%",
                    index: 1
                },
                {
                    field: "FormName",
                    title: "属性",
                    filterable: true,
                    sortable: true,
                    editable: false,
                    menu: true,
                    type: FieldTypeEnum.String,
                    width: "10%",
                    index: 2
                },
                {
                    field: "FormContent",
                    title: "薪酬项类型",
                    filterable: true,
                    sortable: true,
                    editable: false,
                    menu: true,
                    type: FieldTypeEnum.Enums,
                    width: "10%",
                    values: EnumHelper.toEnumOptions(EnumConstType.FormContentType, "text", "value"),
                    index: 2
                },
                {
                    field: "Description",
                    title: "说明",
                    filterable: true,
                    sortable: true,
                    editable: false,
                    menu: true,
                    type: FieldTypeEnum.String,
                    width: "40%",
                    index: 3,
                    hidden: false
                },
                {
                    field: "DataStatus",
                    title: "状态",
                    filterable: true,
                    sortable: true,
                    editable: false,
                    type: FieldTypeEnum.Enums,
                    menu: true,
                    width: "10%",
                    index: 4,
                    values: EnumHelper.toEnumOptions(EnumConstType.DataStatus, "text", "value")
                }
            ])
        })
    }

    submit(data) {
        console.log("data: " + JSON.stringify(data))
        this.$emit("submit", data)
    }
}