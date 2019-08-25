import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';

@Component({
    props: ["desc"],
    components: {
        TzGridDynamic: require("../../TzGridDynamic.vue.html")
    }
})
export default class TzSuperGrid extends Vue {
    @Prop() desc!: any;
    
    get transport_read_url() {
        return this.desc.options.remote;
    }

    get columns() {
        return this.desc.options.schema;
    }
}