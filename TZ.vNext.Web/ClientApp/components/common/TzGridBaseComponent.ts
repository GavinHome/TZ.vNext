import kendoHelper from '../extension/KendoExtensions'
import { GridCommon } from '../extension/GridExtensions'
import { GridMenuType } from "./Enums"
import Vue from "vue";
import { GridCommand } from './TzGridCommand';
import Component from 'vue-class-component';

@Component
export class GridBaseComponent extends Vue {
    schemaModelFields: any = {}
    dataSource: any = {}
    Commands: GridCommand[] = []

    bindCommands(e) {
        var commandsTemplate = this.Commands.filter(x => x.is).sort(x => x.index)
        return new GridCommon().bindCommands(commandsTemplate)
    }

    onDataBinding(e) {
        kendoHelper.onDataBinding(e)
    }

    onDataBound(e) {
        kendoHelper.onDataBound(e)
        this.dataSource = this.$refs.remoteDataSource
    }

    parameterMap(data) {
        var json = JSON.stringify(data)
        return json
    }

    getCommandInfo(type: GridMenuType) {
        var command = this.Commands.filter(x => x.name === type)[0];
        return command;
    }

    getRowData(e) {
        var data = kendoHelper.dataItem(e)
        e.preventDefault();
        return data;
    }

    onBeforeSend(xhr: any) {
        kendoHelper.onBeforeSend(xhr)
    }
}
