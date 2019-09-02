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
        //return Promise.resolve(data)
        return TzFetch.Post("%5", this.formData)
    }

    handleSuccess(response) {
        this.$message.success('创建成功')
    }

    handleError(response) {
        this.$message.success('失败')
    }

    handleEnd(response) {
        this.$message.success('处理结束')
    }

    handleRequest(response) {
        this.$message.success('自定义处理')
    }
}`


export function getViewTemplate(isCustomHandleRequest: boolean) {
    var internal_tpl = `:request-fn="handleSubmit"      
      @request-success="handleSuccess"      
      @request-error="handleError"      
      @request-end="handleEnd" `;
    var handleRequest_tpl = `@request="handleRequest"`

    return `<template>
    <tz-super-form
      :form="form"
      :form-data="formData"
      :label-width="labelWidth"
      :rules="rules"
      ${isCustomHandleRequest ? handleRequest_tpl : internal_tpl}
      %1
    ></tz-super-form>
  </template>
  
  <script src="./render.ts"></script>`
}

export function getComponentRenderTemplate(isCustomHandleRequest: boolean, isAutoPost:boolean) {
    var internal_method_tpl = `handleSubmit(data) {
        return ${ !isAutoPost? 'TzFetch.Post("%5", this.formData)':'Promise.resolve(data)'}
    }

    handleSuccess(response) {
        this.$message.success('创建成功')
    }

    handleError(response) {
        this.$message.success('失败')
    }

    handleEnd(response) {
        this.$message.success('处理结束')
    }`

    var handleRequest_method_tpl = `handleRequest(response) {
        TzFetch.Post("%5", this.formData).then((data: any) => {
            if (data) {
                this.$router.replace("/")
            } else {
                this.$message.error("提交失败")
            }
        }).catch(e => {
            this.$message.error("提交失败")
        })
        this.$message.success('自定义处理')
    }`

    return `import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup } from "../../TzSuperForm/schema/TzSuperFormSchema";
import { TzFetch } from "../../common/TzFetch";

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

    ${isCustomHandleRequest ? handleRequest_method_tpl : internal_method_tpl}
}`
}

export default { template_tpl: view_template_tpl, render_tpl: component_render_tpl }