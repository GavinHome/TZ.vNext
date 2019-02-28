import Vue from "vue";
import { Component } from "vue-property-decorator";

import "element-ui/lib/theme-chalk/index.css";
import { Button, Message } from "element-ui";
import { GridMenuType, EnumHelper, FieldTypeEnum, EnumConstType } from "../../common/Enums";
import kendoExtensions from "../../extension/KendoExtensions";
import { GridCommon } from "../../extension/GridExtensions";
import { GridColumnSchema } from "../../schemas/GridColumnSchema";
import { TzConfirm } from "../../common/TzDialog";
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";

Vue.use(Button);

@Component({
    components: {
        TzGridDynamic: require("../../wrapper/TzGridDynamic.vue.html")
    }
})
export default class SalaryPartsComponent extends Vue {

    columnsData: GridColumnSchema[] = [
        {
            field: "RowNumber",
            title: "序号",
            width: "8%",
            filterable: false,
            sortable: false,
            editable: false,
            menu: false,
            type: FieldTypeEnum.Number,
            index: 0
        },
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
        },
        {
            title: "",
            filterable: false,
            sortable: false,
            editable: false,
            menu: false,
            command: this.commands,
            type: FieldTypeEnum.Command,
            width: "15%",
            index: 99
        }
    ];

    get readUrl() {
        return TzApiConst.SALARY_GRID_QUERY
    }

    get columns() {
        return this.columnsData.sort((x, y) => x.index - y.index);
    }

    get commands() {
        var commands = [
            {
                name: GridMenuType.Detail,
                title: EnumHelper.toGridMenuTypeString(GridMenuType.Detail),
                action: this.detail,
                visible: function (dataItem) {
                    return dataItem.Menus && dataItem.Menus.indexOf("Detail") > -1;
                },
                is: true,
                index: 0,
                param: {}
            },
            {
                name: GridMenuType.Edit,
                title: EnumHelper.toGridMenuTypeString(GridMenuType.Edit),
                action: this.edit,
                visible: function (dataItem) {
                    return dataItem.Menus && dataItem.Menus.indexOf("Edit") > -1;
                },
                is: true,
                index: 1,
                param: {}
            },
            {
                name: GridMenuType.Enable,
                title: EnumHelper.toGridMenuTypeString(GridMenuType.Enable),
                action: this.enable,
                visible: function (dataItem) {
                    return dataItem.Menus && dataItem.Menus.indexOf("Enable") > -1;
                },
                is: true,
                index: 2,
                param: {}
            },
            {
                name: GridMenuType.Disable,
                title: EnumHelper.toGridMenuTypeString(GridMenuType.Disable),
                action: this.disable,
                visible: function (dataItem) {
                    return dataItem.Menus && dataItem.Menus.indexOf("Disable") > -1;
                },
                is: true,
                index: 2,
                param: {}
            }
        ];

        var commandsTemplate = commands.filter(x => x.is).sort(x => x.index);
        return new GridCommon().bindCommands(commandsTemplate);
    }

    create() {
        this.$router.push({ path: "/parts/create" });
    }

    detail(e) {
        var data = kendoExtensions.getRowData(e);
        this.$router.push({ path: "/parts/detail", query: { id: data.Id } });
    }

    edit(e) {
        var data = kendoExtensions.getRowData(e);
        this.$router.push({ path: "/parts/edit", query: { id: data.Id } });
    }

    enable(e) {
        var data = { id: kendoExtensions.getRowData(e).Id };
        var url = TzApiConst.SALARY_ENABLE;
        var message = TzMessageConst.ENABLE_CONFIRM_MESSAGE + kendoExtensions.getRowData(e).Name + TzMessageConst.SYMBOL_QUESTIONMARK;
        new TzConfirm().enable(url, message, TzMessageConst.ENABLE_MESSAGE, data).then(res => {
            kendoExtensions.onRefresh(this.$refs.salaryGrid)
        }).catch(e => {
            Message.error(TzMessageConst.ENABLE_FAIL_MESSAGE)
        })
    }

    disable(e) {
        var data = { id: kendoExtensions.getRowData(e).Id };
        var url = TzApiConst.SALARY_DISABLE;
        var message = TzMessageConst.DISABLE_CONFIRM_MESSAGE + kendoExtensions.getRowData(e).Name + TzMessageConst.SYMBOL_QUESTIONMARK;
        new TzConfirm().disable(url, message, TzMessageConst.DISABLE_MESSAGE, data).then(res => {
            kendoExtensions.onRefresh(this.$refs.salaryGrid)
        }).catch(e => {
            Message.error(TzMessageConst.DISABLE_FAIL_MESSAGE)
        })
    }
}
