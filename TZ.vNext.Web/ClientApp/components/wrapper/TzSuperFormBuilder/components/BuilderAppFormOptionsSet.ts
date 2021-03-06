import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperOptionSchema, TzSuperDataSourceSchema } from "../../TzSuperForm/schema/TzSuperFormSchema";
import 'element-ui/lib/theme-chalk/index.css'
import { Tabs, TabPane, Table, Button, TableColumn, Form, FormItem } from 'element-ui'
import { TzFetch } from "../../../common/TzFetch";
Vue.use(Tabs)
Vue.use(TabPane)
Vue.use(Table)
Vue.use(Button)
Vue.use(TableColumn)
Vue.use(Form)
Vue.use(FormItem)

@Component({
    props: ["model"]
})
export default class BuilderAppFormOptionsSet extends Vue {
    @Prop() model!: any

    dataSource: TzSuperOptionSchema[] = []
    activeName: string = "local"
    options: any = {}

    fields: any[] = []
    source: TzSuperDataSourceSchema[] = []

    get columns() {
        return Object.keys(this.dataSource[0]).map(x => {
            return { field: x, title: x, type: "input" }
        })
    }

    created() {
        if (Array.isArray(this.model)) {
            //local
            this.dataSource = this.model
        }
        else {
            this.options = this.model
            this.activeName = "remote"
        }

        if (this.dataSource.length === 0) {
            this.add()
        }

        TzFetch.Post("/api/SuperForm/GridQueryDataSourceMeta", null).then((data) => this.source = data as TzSuperDataSourceSchema[])
    }

    add() {
        this.dataSource.push({ key: "", value: "", ext: "" })
    }

    remove(i, r) {
        this.dataSource.splice(i, 1);
    }

    submit(data) {
        this.$emit("submit", data)
    }

    remoteDataSource(query) {
        if(query === '') {
            this.list = this.source
        }

        this.loading = true;
        setTimeout(() => {
            this.loading = false;
            this.list = this.source.filter(x=> x.value.toLowerCase().indexOf(query) > -1);
        }, 200);
    }

    handleDataSourceChange(key) {
        var item = this.source.filter(x=>x.key === key)[0]
        if(item) {
            this.options.remote = item.url
            this.options.schema_meta_url = item.metaUrl
            this.options.schema_meta_key = item.key
        }
    }

    selectedSource: any = null
    loading: boolean = false
    loading1: boolean = false
    loading2: boolean = false
    loading3: boolean = false
    list: any[] = []
    list1: any[] = []
    list2: any[] = []
    list3: any[] = []

    createFilter(queryString) {
        return (data) => {
            return ((data.value && data.value.toLowerCase().indexOf(queryString.toLowerCase()) > -1) || (data.label && data.label.toLowerCase().indexOf(queryString.toLowerCase()) > -1));
        };
    }

    remoteMethod1(query) {
        if (query !== '') {
            this.loading1 = true;
            setTimeout(() => {
                this.loading1 = false;
                this.list1 = this.fields.filter(this.createFilter(query));
            }, 200);
        } else {
            this.list1 = this.fields.map(x => { return { value: x.value, label: x.label } });
        }
    }

    remoteMethod2(query) {
        if (query !== '') {
            this.loading2 = true;
            setTimeout(() => {
                this.loading2 = false;
                this.list2 = this.fields.filter(this.createFilter(query));
            }, 200);
        } else {
            this.list2 = this.fields.map(x => { return { value: x.value, label: x.label } });;
        }
    }

    remoteMethod3(query) {
        if (query !== '') {
            this.loading3 = true;
            setTimeout(() => {
                this.loading3 = false;
                this.list3 = this.fields.filter(this.createFilter(query));
            }, 200);
        } else {
            this.list3 = this.fields.map(x => { return { value: x.value, label: x.label } });
        }
    }

    remoteChange() {
        TzFetch.Post(this.options.schema_meta_url, { key : this.options.schema_meta_key }).then((data: any) => {
            if (data && data.length) {
                this.fields = data.map(item => {
                    return {
                        value: item.field,
                        label: item.title,
                    }
                })
                
                data.forEach(x => {
                    this.options.schema[x.field] = { type: x.type, filterable: true }
                })
            }
        }).catch(err => {
            this.$message.error("获取失败")
        })
    }
}