import Vue from 'vue';
import { Component } from 'vue-property-decorator'
import StoreCache from "../common/TzStoreCache";

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
}