import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator'

import { Tag } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(Tag)

@Component({
    props: ["value", "desc"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzSuperTag extends Vue {
    @Prop() desc!: any
    @Prop() value!: string[] 

    tags: string[] = this.value
    inputVisible: boolean = false
    inputValue: string = ''

    handleClose(tag) {
        this.tags.splice(this.tags.indexOf(tag), 1);
    }

    showInput() {
        this.inputVisible = true;
        this.$nextTick(() => {
            (this.$refs.saveTagInput as any).$refs.input.focus();
        });
    }

    handleInputConfirm() {
        let inputValue = this.inputValue;
        if (inputValue) {
            this.tags.push(inputValue);
            this.$emit('change', this.tags)
        }

        this.inputVisible = false;
        this.inputValue = '';
    }
}
