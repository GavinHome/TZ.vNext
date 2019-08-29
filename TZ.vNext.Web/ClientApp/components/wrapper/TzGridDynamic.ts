import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import kendoHelper from "../extension/KendoExtensions";
import { GridModelSchema, GridModelSchemaType } from "../schemas/GridModelSchema";
import { GridColumnSchema } from "../schemas/GridColumnSchema";
import "@progress/kendo-ui"
import '@progress/kendo-ui/css/web/kendo.common.min.css'
import '@progress/kendo-ui/css/web/kendo.common.core.min.css'
import '@progress/kendo-ui/css/web/kendo.silver.min.css'
import '@progress/kendo-ui/js/cultures/kendo.culture.zh-CN.js'
import '@progress/kendo-ui/js/messages/kendo.messages.zh-CN.js'
import { GridInstaller } from '@progress/kendo-grid-vue-wrapper'
import { DataSourceInstaller } from '@progress/kendo-datasource-vue-wrapper'
import IUrlParameterSchema from "../schemas/IUrlParameterSchema";
import { encodeQueryData } from "../common/TzCommonFunc";
import { Message } from "element-ui";
import { TzMessageConst } from "../common/TzCommonConst";
import StoreCache from "../../components/common/TzStoreCache";
import "../extension/StringExtensions";
import { debug } from '../../log';

kendo.culture("zh-CN")
Vue.use(GridInstaller)
Vue.use(DataSourceInstaller)

@Component({
    props: ["transport_read_url", "columns", "querys"],
    components: {
        SearchBar: require("./SearchBar.vue.html")
    }
})
export default class TzGridDynamic extends Vue {
    @Prop() transport_read_url!: string;
    @Prop() columns!: GridColumnSchema[];
    @Prop() querys!: IUrlParameterSchema;

    schemaModelField: GridModelSchema = {};
    dataSource: any = {};

    get schemaModelFields() {
        this.schemaModelField = {};
        this.columns
            .filter(x => !String.isNullOrEmpty(x.field))
            .forEach((e, i) => {
                let value: GridModelSchemaType = {
                    filterable: e.filterable,
                    sortable: e.sortable,
                    editable: e.editable,
                    nullable: true,
                    type: e.type
                };

                this.schemaModelField[e.field as string] = value;
            });

        return this.schemaModelField;
    }

    get DynamicColumns() {
        return this.columns;
    }

    onDataBinding(e) {
        kendoHelper.onDataBinding(e);
    }

    onDataBound(e) {
        kendoHelper.onDataBound(e);
        this.dataSource = this.$refs.remoteDataSource;
    }

    parameterMap(data, type) {
        var json = JSON.stringify(data);
        return json;
    }

    onBeforeSend(xhr) {
        kendoHelper.onBeforeSend(xhr);
    }

    onRefresh(param?: any) {
        kendoHelper.onRefresh(this.$refs.remoteDataSource, param)
    }

    onError(err) {
        if (err.xhr.status === 401) {
            this.clearAuth();
        } else {
            ////log errors
            debug.notifyError({
                error: err,
                message: err,
                metaData: {
                    userToken: this.getUserToken()
                },
                lineNumber: 1,
                columnNumber: 2,
                fileName: 'TzGridDynamic.ts'
            });

            Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
        }
    }

    clearAuth() {
        const cache = new StoreCache("auth");
        cache.clear();
        this.$router.push({ path: "/login" })
    }

    getUserToken(): any {
        let cache = new StoreCache("auth");
        let token = cache.get("token");
        let name = cache.get("name");

        return { token: token, name: name };
    }

    get readData() {
        return { url: this.transport_read_url, beforeSend: this.onBeforeSend, }
    }

    requestStart(e) {
        var params = encodeQueryData(this.querys);
        e.sender.options.transport.read.url = this.transport_read_url + (params ? "?" + params : '')
    }
}
