import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup, TzSuperFormType, TzSuperFormField, getComponentName } from "./schema/TzSuperFormSchema";

import 'element-ui/lib/theme-chalk/index.css'
import ElementUI from 'element-ui'
import { TzFetch } from "../../common/TzFetch";
Vue.use(ElementUI)

@Component({
    props: ["form", "formData", "rules", "isLoading", "formError", "requestFn", "isHideSubmitBtn", "isHideBackBtn", "submitBtnText", "backBtnText", "labelWidth", "single", "isCustomHandleRequest", "action", "isAutoHandlePost"],
    components: {
        TzSuperTextarea: require('./components/TzSuperTextarea.vue.html'),
        TzSuperNumber: require('./components/TzSuperNumber.vue.html'),
        TzSuperSelect: require('./components/TzSuperSelect.vue.html'),
        TzSuperDialog: require('./components/TzSuperDialog.vue.html'),
        TzSuperText: require('./components/TzSuperText.vue.html'),
        TzSuperSwitch: require('./components/TzSuperSwitch.vue.html'),
        TzSuperShell: require('./components/TzSuperShell.vue.html'),
        TzSuperTime: require('./components/TzSuperTime.vue.html'),
        TzSuperTag: require('./components/TzSuperTag.vue.html'),
        TzSuperRadio: require('./components/TzSuperRadio.vue.html'),
        TzSuperCheckbox: require('./components/TzSuperCheckbox.vue.html'),
        TzSuperDate: require('./components/TzSuperDate.vue.html'),
        TzSuperDatetime: require('./components/TzSuperDatetime.vue.html'),
        TzSuperRate: require('./components/TzSuperRate.vue.html'),
        TzSuperSlider: require('./components/TzSuperSlider.vue.html'),
        TzSuperPassword: require('./components/TzSuperPassword.vue.html'),
        TzSuperYear: require('./components/TzSuperYear.vue.html'),
        TzSuperMonth: require('./components/TzSuperMonth.vue.html'),
        TzSuperInput: require('./components/TzSuperInput.vue.html'),
        TzSuperDaterange: require('./components/TzSuperDateRange.vue.html'),
        TzSuperMonthrange: require('./components/TzSuperMonthRange.vue.html'),
        TzSuperDatetimerange: require('./components/TzSuperDateTimeRange.vue.html'),
        TzSuperDates: require('./components/TzSuperDates.vue.html'),
        TzSuperWeek: require('./components/TzSuperWeek.vue.html'),
        TzSuperTimerange: require('./components/TzSuperTimeRange.vue.html'),
        TzSuperButton: require('./components/TzSuperButton.vue.html'),
        TzSuperAutocomplete: require('./components/TzSuperAutocomplete.vue.html'),
        TzSuperGrid: require('./components/TzSuperGrid.vue.html')
    }
})
export default class TzSuperForm extends Vue {
    @Prop() form!: TzSuperFormGroup[]
    @Prop() formData!: any
    @Prop() rules!: any
    @Prop() isLoading!: boolean
    @Prop() formError!: any
    @Prop() requestFn!: Function
    @Prop({ default: false }) isHideSubmitBtn!: boolean  //是否隐藏submit按钮
    @Prop({ default: false }) isHideBackBtn!: boolean  //是否隐藏back按钮

    @Prop({ default: '提交' }) submitBtnText!: string  //提交按钮文本
    @Prop({ default: '返回' }) backBtnText!: string  //返回按钮文本

    @Prop({ default: 120 }) labelWidth!: Number  //标签宽度

    @Prop({ default: false }) single!: boolean

    @Prop({ default: false }) isCustomHandleRequest!: boolean
    @Prop({ default: true }) isAutoHandlePost!: boolean
    @Prop() action!: string

    // 是否正在请求中
    innerIsLoading: boolean = false;
    // 内部请求出错
    innerFormError: any = {};

    get ActiveCollapses() {
        return this.form.filter(x => !x.isCollapsed).map(x => x.name)
    }

    get isGroupalbe() {
        return this.single
    }

    get showSubmitBtn() {
        return !this.isHideSubmitBtn;
    }

    get showBackBtn() {
        return !this.isHideBackBtn;
    }

    get formDesc() {
        let fields: TzSuperFormField[] = [];
        this.form.forEach((f, i) => {
            f.rows.forEach((r, j) => {
                fields = fields.concat(r.fields)
            })
        })

        return fields;
    }

    getComponentName(type: TzSuperFormType) {
        return getComponentName(type)
    }

    handleValidateForm(e) {
        if (this.rules) {
            (this.$refs['form'] as any).validate((valid, invalidFields) => {
                if (valid) {
                    this.handleSubmitForm()
                } else {
                    return this.processValidError(invalidFields)
                }
            })
        } else {
            this.handleSubmitForm()
        }
    }

    // 提交表单
    async handleSubmitForm() {
        //const data = cloneDeep(this.formData)
        const data = this.formData
        for (const field in data) {
            if (this.formDesc[field] && this.formDesc[field].valueFormatter) {
                data[field] = this.form[field].valueFormatter(data[field], data)
            }
        }

        if (this.requestFn && !this.isCustomHandleRequest) {
            // 在内部请求
            if (this.innerIsLoading) return
            this.innerIsLoading = true
            try {
                ////设置了自动提交，并存在提交地址，则自动提交请求
                let res: any = data
                if (this.isAutoHandlePost && this.action) {
                    res = await TzFetch.Post(this.action, this.formData)
                }

                ////转交数据到外部函数
                const response = await this.requestFn(res)
                this.$nextTick(() => {
                    ////处理成功
                    this.$emit('request-success', response)
                })
            } catch (error) {
                // 处理异常情况
                if (error instanceof Error) {
                    // 返回的是Error类型, 则进行解析
                    try {
                        const msg = JSON.parse(error.message)
                        if (msg instanceof Object) {
                            this.innerFormError = msg
                        }
                        // eslint-disable-next-line
                    } catch {
                        this.innerFormError = error
                    }
                } else if (error instanceof Object) {
                    // 返回的是对象类型, 则直接设置
                    this.innerFormError = error
                }

                this.$emit('request-error', this.innerFormError)
            } finally {
                this.innerIsLoading = false
                this.$nextTick(() => {
                    this.$emit('request-end')
                })
            }
        } else {
            // 在外部请求
            if (this.isLoading) return
            this.$emit('request', data)
        }
    }

    // 处理表单错误
    processValidError(invalidFields) {
        return false;
    }

    goBack() {
        if (this.$router) {
            // vue-router
            this.$router.back()
        } else {
            // 浏览器history API
            history.back()
        }
    }
}