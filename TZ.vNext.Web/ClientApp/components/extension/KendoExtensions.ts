import "@progress/kendo-ui"
import StoreCache from "../common/TzStoreCache";
import TzGridDynamic from "../wrapper/TzGridDynamic";
import { FieldTypeEnum } from "../common/Enums";

var kendoJQuery = (kendo as any).jQuery

var kendoExtensions = {
    dataItem: function (e: any) {
        var grid = kendoJQuery(e.delegateTarget).data("kendoGrid")
        var tr = kendoJQuery(e.currentTarget).closest('tr')
        var data = grid.dataItem(tr)
        return data
    },
    onDataBound: function (e: any) {
        e.sender.element.data("kendoGrid").thead.find("[data-field=RowNumber]>.k-header-column-menu").remove();
    },
    onDataBinding: function (e: any) {
        var page = e.sender.pager.page()
        var pageSize = e.sender.pager.pageSize()
        e.items.forEach((ele, i) => {
            ele.RowNumber = (page - 1) * pageSize + i + 1
        });
    },
    onRowDoubleClick: function (e: any, callback: any) {
        e.sender.element.data("kendoGrid").element.undelegate("tbody tr[data-uid]", "dblclick");
        e.sender.element.data("kendoGrid").element.on('dblclick', 'tbody tr[data-uid]', function (ev) {
            if (callback) {
                callback(e.sender.element.data("kendoGrid").dataItem(kendoJQuery(ev.target).closest('tr')))
            }
        })
    },
    onRefresh: function (dataSource: any, params: any = {}) {
        if (dataSource && dataSource.kendoDataSource) {
            dataSource.kendoDataSource.read(params);
        } else {
            let grid: TzGridDynamic = dataSource as TzGridDynamic
            if (grid) {
                grid.dataSource.kendoDataSource.read(params)
            }
        }
    },
    onRequest: function (schema: any, textSearch: string) {
        let filterCondition: any[] = []
        if (schema) {
            Object.keys(schema).forEach((v, i) => {
                if (schema[v] && schema[v].filterable) {
                    if (schema[v].type === FieldTypeEnum.Number && (Number(textSearch) === 0 || Number(textSearch))) {
                        filterCondition.push({ field: v, operator: "eq", value: Number(textSearch) })
                    } else if (schema[v].type === FieldTypeEnum.Date) {
                        filterCondition.push({ field: v, operator: "eq", value: new Date(textSearch) })
                    } else if (schema[v].type === FieldTypeEnum.String) {
                        filterCondition.push({ field: v, operator: "contains", value: textSearch })
                    } else if (schema[v].type === FieldTypeEnum.Command) {
                        throw new Error("field data type error")
                    }
                }
            })
        }

        let filter = {}
        if (filterCondition.length > 0) {
            filter = { logic: "or", filters: filterCondition }
        }

        return { page: 1, pageSize: 10, filter: filter }
    },
    onBeforeSend: function (xhr: any) {
        xhr.setRequestHeader('Authorization', 'Bearer ' + (new StoreCache('auth')).get('token'));
    },
    onSearch: function (dataSource: any, schema: any, textSearch: string) {
        let filter = this.onRequest(schema, textSearch)
        if (dataSource && dataSource.kendoDataSource) {
            dataSource.kendoDataSource.filter(filter.filter)
        }
    },
    getRowData(e: any) {
        var data = this.dataItem(e)
        e.preventDefault();
        return data;
    }
}

export default kendoExtensions