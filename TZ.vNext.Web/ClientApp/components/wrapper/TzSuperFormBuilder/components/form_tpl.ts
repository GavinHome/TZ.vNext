const view_template_tpl = `<template>
  <tz-super-form
    :form="form"
    :form-data="formData"
    :label-width="labelWidth"
    :rules="rules"
    :request-fn="handleSubmit" 
    @request-success="handleSuccess" 
    @request-error="handleError" 
    @request-end="handleEnd" 
    @request="handleRequest"
    %1
  ></tz-super-form>
</template>

<script src="./render.ts"></script>`

const component_render_tpl = `import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup } from "../../TzSuperForm/schema/TzSuperFormSchema";

@Component({
    props: [],
    components: {
        TzSuperForm: require("../../TzSuperForm/index.vue.html")
    }
})
export default class CustomComponent extends Vue {

    form: TzSuperFormGroup[] = %1

    formData: any = %2

    rules: any = %3

    labelWidth: number = %4

    handleSubmit(data) {
        console.log(data)
        return Promise.resolve(data)
    }

    handleSuccess(response) {
        console.log(response)
        this.$message.success('创建成功')
    }

    handleError(response) {
        console.log(response)
        this.$message.success('失败')
    }

    handleEnd(response) {
        console.log(response)
        this.$message.success('处理结束')
    }

    handleRequest(response) {
        console.log(response)
        this.$message.success('自定义处理')
    }
}`

export default { template_tpl: view_template_tpl, render_tpl: component_render_tpl }