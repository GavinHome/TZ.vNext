import Vue from "vue";
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TzSuperFormField } from "../BuilderFormComps";

@Component({
    props: ["data"],
    components: {        
        AppFormDraggleContainer: require('./BuilderAppFormDraggleContainer.vue.html'),
    }
})
export default class BuilderAppFormGroupItem extends Vue {
    @Prop() data!: any

    get rows() {
        return this.data.rows
    }

    get fields() {
        var fields:TzSuperFormField[] = []
        this.data.rows.forEach((row, a) => {
            row.fields.forEach((field, c) => {
                fields.push(field)
            })
        })

        return fields;
    }

    handleSelectFormItem(data) {
        this.$emit("selectedFormItem", data)
    }
}