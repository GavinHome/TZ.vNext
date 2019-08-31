import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { FieldTypeEnum, GridMenuType, EnumHelper } from "../../common/Enums";
import { GridColumnSchema } from "../../schemas/GridColumnSchema";
import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
import { GridCommon } from "../../extension/GridExtensions";
import kendoExtensions from "../../extension/KendoExtensions";
import { TzConfirm } from "../../common/TzDialog";
import { TzFetch } from "../../common/TzFetch";
import "element-ui/lib/theme-chalk/index.css";
import { Button, Message } from "element-ui";
Vue.use(Button);

@Component({
    components: {
        TzGridDynamic: require("../../wrapper/TzGridDynamic.vue.html")
    }
})
export default class Product extends Vue {
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
            width: "25%",
            index: 1
        },
        {
            field: "CreateByName",
            title: "拥有者",
            filterable: true,
            sortable: true,
            editable: false,
            menu: true,
            type: FieldTypeEnum.String,
            width: "25%",
            index: 2
        },
        {
            field: "LastUpldateAt",
            title: "最后修改时间",
            filterable: true,
            sortable: true,
            editable: false,
            menu: true,
            type: FieldTypeEnum.Enums,
            width: "25%",
            index: 3
        },
        // {
        //     field: "Description",
        //     title: "说明",
        //     filterable: true,
        //     sortable: true,
        //     editable: false,
        //     menu: true,
        //     type: FieldTypeEnum.String,
        //     width: "20%",
        //     index: 4,
        //     hidden: false
        // },
        {
            title: "操作",
            filterable: false,
            sortable: false,
            editable: false,
            menu: false,
            command: this.commands,
            type: FieldTypeEnum.Command,
            width: "15%",
            index: 5
        }
    ];

    get readUrl() {
        return TzApiConst.PRODUCT_GRID_QUERY
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
                name: GridMenuType.Delete,
                title: EnumHelper.toGridMenuTypeString(GridMenuType.Delete),
                action: this.delete,
                visible: function (dataItem) {
                    return dataItem.Menus && dataItem.Menus.indexOf("Delete") > -1;
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
        TzFetch.Post(TzApiConst.PRODUCT_SAVE, {
            Id: '', Name: '未命名产品', Description: '未命名产品', CreateByName: '王麻子'
        }).then((data: any) => {
            if (data && data.Id != null) {
                this.$router.push({ path: "/product/processing", query: { id: data.Id } });
            } else {
                Message.error(TzMessageConst.SAVE_FAIL_MESSAGE)
            }
        }).catch(e => {
            Message.error(TzMessageConst.SAVE_FAIL_MESSAGE)
        })
    }

    edit(e) {
        var data = kendoExtensions.getRowData(e);
        this.$router.push({ path: "/processing", query: { id: data.Id } });
    }

    detail(e) {
        var data = kendoExtensions.getRowData(e);
        this.$router.push({ path: "/product/view", query: { id: data.Id } });
    }

    delete(e) {
        var data = { id: kendoExtensions.getRowData(e).Id };
        var url = TzApiConst.PRODUCT_DISABLE;
        var message = TzMessageConst.DELETE_CONFIRM_MESSAGE + kendoExtensions.getRowData(e).Name + TzMessageConst.SYMBOL_QUESTIONMARK;
        new TzConfirm().enable(url, message, TzMessageConst.DELETE_MESSAGE, data).then(res => {
            kendoExtensions.onRefresh(this.$refs.grid)
        }).catch(e => {
            Message.error(TzMessageConst.ENABLE_FAIL_MESSAGE)
        })
    }
}