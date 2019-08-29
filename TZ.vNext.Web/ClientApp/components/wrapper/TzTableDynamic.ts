import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";

import {
    Form,
    FormItem,
    Input,
    Row,
    Col,
    Collapse,
    CollapseItem,
    InputNumber,
    Pagination,
    Tooltip,
    Loading,
    Message
} from "element-ui";

import "element-ui/lib/theme-chalk/index.css";
import { GridColumnSchema } from "../schemas/GridColumnSchema";
import { GridModelSchemaType } from "../schemas/GridModelSchema";
import { CustomDataSource } from "../common/TzCustomSearch";
import "../extension/DateExtensions";
import IUrlParameterSchema from "../schemas/IUrlParameterSchema";
import { encodeQueryData } from "../common/TzCommonFunc";
import { TzConst, TzMessageConst } from "../common/TzCommonConst"
import { FieldTypeEnum } from "../common/Enums"
import { TzFetch } from "../common/TzFetch";
import SearchBar from "./SearchBar";
import "../extension/StringExtensions";

Vue.use(Form);
Vue.use(FormItem);
Vue.use(Input);
Vue.use(Row);
Vue.use(Col);
Vue.use(Collapse);
Vue.use(CollapseItem);
Vue.use(InputNumber);
Vue.use(Pagination);
Vue.use(Tooltip);
Vue.use(Loading);

@Component({
    props: ["fetchUrl", "columns", "pageSize", "value", "querys"],
    components: {
        SearchBar: require("./SearchBar.vue.html")
    }
})
export default class TzTableDynamic extends Vue {
    @Prop() fetchUrl!: string;
    @Prop() columns!: GridColumnSchema[];
    @Prop() value!: any[];
    @Prop() pageSize!: number;
    //@Prop() qparams!: IUrlParameterSchema;
    @Prop() querys!: IUrlParameterSchema;

    maxNumber: number = TzConst.MaxNumber
    currentPage: number = 1;
    schemaModelFields: any = {};

    customDataSource: CustomDataSource = {
        fetchUrl: this.remoteUrl,
        local: this.value != null,
        model: this.value,
        request: {
            page: 1,
            pageSize: this.pageSize ? this.pageSize : 10,
            sort: [
                // {
                //   field: "Name",
                //   dir: "asc"
                // }
            ]
        },
        total: 0,
        dataSource: [],
        loading: false,
        filter: function (filter: any) {
            if (filter) {
                this.request.filter = filter;
            }

            if (this.local) {
                if (this.model) {
                    this.total = this.model.length;

                    //todo: filter
                    let data = this.model.filter((value, i) => {
                        let flag = false;
                        if (filter && filter.filters) {
                            let filters = filter.filters as any[];
                            if (filters.length > 0) {
                                filters.forEach((v, i) => {
                                    if ((value[v.field] as string).indexOf(v.value) > -1) {
                                        flag = true;
                                    }
                                });
                            } else {
                                flag = true;
                            }
                        } else {
                            flag = true;
                        }

                        if (flag) {
                            return value;
                        } else {
                            return null;
                        }
                    });

                    //todo: sort
                    if (this.request.sort && this.request.sort.length > 0) {
                        data = data.sort(
                            (x, y) =>
                                (x[this.request.sort[0].field] > y[this.request.sort[0].field]
                                    ? 1
                                    : -1) * (this.request.sort[0].dir === "asc" ? 1 : -1)
                        );
                    }

                    let skip = (this.request.page - 1) * this.request.pageSize;
                    let end = skip + this.request.pageSize;
                    end = data.length > end ? end : data.length;
                    this.dataSource = data.slice(skip, end);

                    this.onDataBinding(this.dataSource);
                }
            } else {
                this.loading = true;
                TzFetch.Post(this.fetchUrl, this.request, false)
                    .then((data: any) => {
                        if (data) {
                            this.dataSource = data.Data;
                            this.total = data.Total;
                            this.onDataBinding(this.dataSource);
                        } else {
                            this.dataSource = [];
                            this.total = 0;
                        }

                        this.loading = false;
                    }).catch(e => {
                        Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
                        this.loading = false;
                    });
            }
        },
        onDataBinding(data: any) {
            var page = this.request.page;
            var pageSize = this.request.pageSize;
            data.forEach((ele, i) => {
                ele.RowNumber = (page - 1) * pageSize + i + 1;
                for (var key in ele){
                    if(ele[key] === 'True'){
                        ele[key] = true;
                    }else if(ele[key] === 'False'){
                        ele[key] = false;
                    }
                }
            });
        }
    };

    get dataSource() {
        return this.customDataSource.dataSource;
    }

    get loading() {
        return this.customDataSource.loading;
    }

    get total() {
        return this.customDataSource.total;
    }

    get remoteUrl() {
        if (this.querys) {
            return this.fetchUrl + "?" + encodeQueryData(this.querys);
        } else {
            return this.fetchUrl;
        }
    }

    get dynamicColumns() {
        this.schemaModelFields = {};
        this.columns
            .filter(x => !String.isNullOrEmpty(x.field))
            .forEach((e, i) => {
                if (e.field === TzConst.RowNumber) {
                    e.title = '序号';
                    e.width = e.width === undefined ? '60' : e.width;
                    e.filterable = false;
                    e.sortable = false;
                    e.menu = false;
                    e.type = FieldTypeEnum.Number;
                    e.index = -1;
                }

                let value: GridModelSchemaType = {
                    filterable: e.filterable,
                    sortable: e.sortable,
                    editable: e.editable,
                    nullable: true,
                    type: e.type
                };

                this.schemaModelFields[e.field as string] = value;
            });

        this.$nextTick(() => {
            if (this.value) {
                this.customDataSource.model = this.value;
            }

            this.customDataSource.filter();
        });
        
        return this.columns;
    }

    toFormat(format: string, data: string) {
        if (format === '{0:N}') {
            return String.formatMoney(data, TzConst.DefaultDigit);
        } else if (format === '{0:N3}') {
            return String.formatMoney(data, TzConst.THREE);
        } else if (format === '{0:N4}') {
            return String.formatMoney(data, TzConst.FORE);
        }
        else {
            return data;
        }
    }

    rowClass(data){
        if(!String.isNullOrEmpty(data.row.RowClass)){
            return data.row.RowClass;
        }
    }

    handleSizeChange(val) {
        console.log(`每页 ${val} 条`);
    }

    handleCurrentChange(val) {
        console.log(`当前页: ${val}`);
        this.currentPage = val;
        this.customDataSource.request.page = val;
        this.customDataSource.filter();
    }

    prevClick() {
        console.log(`prevClick`)
    }

    nextClick() {
        console.log(`nextClick`)
    }

    onQueryDataRefresh(data: IUrlParameterSchema, isClearSearch: boolean = true) {
        this.customDataSource.fetchUrl =
            this.fetchUrl + "?" + encodeQueryData(data);

        if (isClearSearch) {
            this.customDataSource.filter({});
        } else {
            this.customDataSource.filter();
        }
    }

    clearSearch() {
        (this.$refs.searchBar as SearchBar).clearText();
    }
}
