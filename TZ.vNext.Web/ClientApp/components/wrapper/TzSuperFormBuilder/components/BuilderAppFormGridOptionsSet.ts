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
    columns: any[] = [{
        field: "field",
        title: "字段",
        filterable: false,
        sortable: false,
        editable: false,
        menu: false,
        type: "string",
        width: "10%",
        index: 1
    },
    {
        field: "title",
        title: "名称",
        filterable: false,
        sortable: false,
        editable: false,
        menu: false,
        type: "string",
        width: "10%",
        index: 2
    },
    {
        field: "index",
        title: "位置",
        filterable: false,
        sortable: false,
        editable: false,
        menu: false,
        type: "number",
        width: "10%",
        index: 0
    }]

    remove(i, r) {
        this.options.schema.splice(i, 1);
    }

    created() {
        debugger
    }

    remoteChange() {
        TzFetch.Post(this.options.schema_meta_url, { key: "VEmployee" }).then((data: any) => {
            var fields = data.map((item, index) => {
                return {
                    field: item.field,
                    title: item.title,
                    filterable: true,
                    sortable: true,
                    editable: false,
                    menu: true,
                    type: item.type,
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

            this.options.schema = this.options.schema.concat(fields)
        }).catch(err => {
            this.$message.error("数据获取失败")
        })
    }

    submit(data) {
        console.log("data: " + JSON.stringify(data))
        this.$emit("submit", data)
    }
}