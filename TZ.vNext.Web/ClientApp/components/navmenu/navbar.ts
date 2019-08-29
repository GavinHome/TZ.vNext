import Vue from 'vue';
import { Component } from 'vue-property-decorator'
import StoreCache from "../common/TzStoreCache";
import { TzDialog } from "../common/TzDialog";
import UpdateLog from "../home/updatelog";

var cache = new StoreCache('auth')

@Component
export default class NavbarComponent extends Vue {

    get userName() {
        return cache.get('name')
    }

    logout() {
        cache.clear()
        this.$router.push({ name: "login" })
    }

    showUpldateLog() {
        ////this.$router.push({ name: "update_log" })
        new TzDialog(this.$createElement, UpdateLog, "", (d: any): any => { });
    }
}