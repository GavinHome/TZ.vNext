import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'

@Component({
    props: {
        app: String,
        prop: Object
    }
})

export default class AsyncComponent extends Vue {
    @Prop() prop!: Object;
    @Prop() app!: string;
    render(h, cxt) {
        return h(require(`./${this.app}`), {
            props: this.prop
        })
    }
}