import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperOptionSchema } from "../../TzSuperForm/schema/TzSuperFormSchema";
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
    options: any = {
        remote: '',
        schema: {
        },
        map: {
            key: '',
            value: '',
            ext: ''
        }
    }

    fields: any[] = []

    get columns() {
        return Object.keys(this.dataSource[0]).map(x => {
            return { field: x, title: x, type: "input" }
        })
    }

    created() {
        debugger
        if (Array.isArray(this.model)) {
            //local
            this.dataSource = this.model
        }
        else {
            this.options = this.model
            this.activeName = "remote"
        }

        if(this.dataSource.length === 0){
            this.add()
        }
    }

    add() {
        this.dataSource.push({ key: "", value: "", ext: "" })
    }

    remove(i, r) {
        this.dataSource.splice(i, 1);
    }

    submit(data) {
        console.log("data: " + JSON.stringify(data))
        this.$emit("submit", data)
    }

    loading1: boolean = false
    loading2: boolean = false
    loading3: boolean = false
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
        TzFetch.Post(this.options.remote, {}).then((data: any) => {
            this.fields = Object.keys(data.Data).map(key => {
                return {
                    value: key,
                    label: data.Data[key].title,
                }
            })

            Object.keys(data.Data).forEach(x => {
                this.options.schema[x] = { type: "string", filterable: true }
            })
        }).catch(err => {
            this.fields.push({
                value: "Id",
                label: "唯一标识",
            })

            this.fields.push({
                value: "Name",
                label: "姓名",
            })

            this.fields.push({
                value: "Code",
                label: "编号",
            })

            this.list1 = this.fields.map(x => { return { value: x.value, label: x.label } });
            this.list2 = this.fields.map(x => { return { value: x.value, label: x.label } });
            this.list3 = this.fields.map(x => { return { value: x.value, label: x.label } });

            this.options.schema["Id"] = { type: "string", filterable: true }
            this.options.schema["Name"] = { type: "string", filterable: true }
            this.options.schema["Code"] = { type: "string", filterable: true }
        })
    }
}