import { GridBaseComponent } from "./TzGridBaseComponent"
import { TzConfirm } from "./TzDialog"
import { GridMenuType, EnumHelper } from "./Enums"
import kendoHelper from '../extension/KendoExtensions'
import Component from "vue-class-component";
import { GridCommand } from "./TzGridCommand";

@Component
export class GridCrudComponent extends GridBaseComponent {
    name: string = ''
    route: string = ''
    api: string = ''

    created() {
        this.Commands.push({
            name: GridMenuType.Detail,
            title: EnumHelper.toGridMenuTypeString(GridMenuType.Detail),
            route: this.route + "/" + GridMenuType.Detail,
            api: this.api + "/" + GridMenuType.Detail,
            action: this.detail,
            visible: function (dataItem) {
                return dataItem.Menus && dataItem.Menus.indexOf("Detail") > -1
            },
            is: true,
            index: 0,
            param: {}
        });

        this.Commands.push({
            name: GridMenuType.Edit,
            title: EnumHelper.toGridMenuTypeString(GridMenuType.Edit),
            route: this.route + "/" + GridMenuType.Edit,
            api: this.api + "/" + GridMenuType.Edit,
            action: this.edit,
            visible: function (dataItem) {
                return dataItem.Menus && dataItem.Menus.indexOf("Edit") > -1
            },
            is: true,
            index: 1,
            param: {}
        });

        this.Commands.push({
            name: GridMenuType.Delete,
            title: EnumHelper.toGridMenuTypeString(GridMenuType.Delete),
            route: this.route + "/" + GridMenuType.Delete,
            api: this.api + "/" + GridMenuType.Delete,
            action: this.delete,
            visible: function (dataItem) {
                return dataItem.Menus && dataItem.Menus.indexOf("Delete") > -1
            },
            is: true,
            index: 2,
            param: {}
        });

        this.Commands.push({
            name: GridMenuType.Create,
            title: EnumHelper.toGridMenuTypeString(GridMenuType.Create) + this.name,
            route: this.route + "/" + GridMenuType.Create,
            api: this.api + "/" + GridMenuType.Create,
            action: this.detail,
            visible: function (dataItem) {
                return dataItem.Menus && dataItem.Menus.indexOf("Create") > -1
            },
            is: false,
            index: -1,
            param: {}
        });

        var customCommands = this.customCommands();

        customCommands.forEach((v, i) => {
            this.Commands.push(v);
        })
    }

    customCommands(): GridCommand[] {
        return []
    }

    detail(e) {
        var data = this.getRowData(e)
        var command = this.getCommandInfo(GridMenuType.Detail);
        this.$router.push({ path: command.route, query: { id: data.Id } })
    }

    edit(e) {
        var data = this.getRowData(e)
        var command = this.getCommandInfo(GridMenuType.Edit);
        this.$router.push({ path: command.route, query: { id: data.Id } })
    }

    delete(e) {
        var data = this.getRowData(e)
        var command = this.getCommandInfo(GridMenuType.Delete);
        new TzConfirm().delete(command.api, { id: data.Id }).then((res) => {
            kendoHelper.onRefresh(this.$refs.remoteDataSource)
        });
    }

    create() {
        var command = this.getCommandInfo(GridMenuType.Create);
        this.$router.push({ path: command.route, query: {} })
    }
}