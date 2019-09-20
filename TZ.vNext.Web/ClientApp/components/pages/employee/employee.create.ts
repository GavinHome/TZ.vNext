import Vue from "vue";
import { Component, Prop } from 'vue-property-decorator';
import { TzSuperFormGroup } from "../../wrapper/TzSuperForm/schema/TzSuperFormSchema";
import { TzRuleMsgConst, TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
import { Message } from "element-ui"
import { TzFetch } from "../../common/TzFetch";
import remote from "../../common/TzValidators";

@Component({
    props: ["id"],
    components: {
        TzSuperForm: require("../../wrapper/TzSuperForm/index.vue.html")
    }
})
export default class EmployeeCreateComponent extends Vue {
    @Prop() id!: string

    form: TzSuperFormGroup[] = [
        {
            "key": "basic",
            "name": "basic",
            "title": "基础信息",
            "isCollapsed": false,
            "rows": [
                {
                    "key": "a26c9f3f-0a16-4d1d-9063-913faa511cb5",
                    "name": "a26c9f3f-0a16-4d1d-9063-913faa511cb5",
                    "fields": [
                        {
                            "key": "a3f27b5d-7524-4788-92fb-d05c20fb056a",
                            "name": "Code",
                            "label": "编号",
                            "type": "input",
                            "title": "编号",
                            "format": null,
                            "options": null,
                            "cols": 1,
                            "attrs": null,
                            "slots": null
                        },
                        {
                            "key": "43dd7567-9d78-4760-a763-8320b77e4803",
                            "name": "Name",
                            "label": "姓名",
                            "type": "input",
                            "title": "姓名",
                            "format": null,
                            "options": null,
                            "cols": 1,
                            "attrs": null,
                            "slots": null
                        }
                    ]
                }
            ]
        }
    ]

    formData: any = {
        Code: '',
        Name: '',
        Id: ''
    }

    get rules() {
        return {
            "Code": [
                { required: true, message: TzRuleMsgConst.EMPLOYEE_CODE_REQUIRED, trigger: "change" },
                {
                    validator: remote,
                    url: `${TzApiConst.Employee_CHECKCODE}?id=${this.formData.Id}&code=${this.formData.Code}`,
                    message: TzRuleMsgConst.EMPLOYEE_CODE_REPEATED,
                    trigger: "blur"
                },
                {
                    len: 9, message: TzRuleMsgConst.EMPLOYEE_CODE_LENGTH, trigger: "change"
                }
            ],
            "Name": [{ required: true, message: TzRuleMsgConst.EMPLOYEE_NAME_REQUIRED, trigger: "change" }]
        }
    }

    labelWidth: number = 100

    created() {
        if (this.id) {
            TzFetch.Post(TzApiConst.Employee_FINDBYID, { id: this.id }).then(data => {
                if (data) {
                    this.formData.Code = (data as any).Code
                    this.formData.Name = (data as any).Name
                    this.formData.Id = (data as any).Id
                }
                else {
                    Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
                }
            }).catch(e => {
                Message.error(TzMessageConst.DATA_FAIL_MESSAGE)
            });
        }
    }

    handleSubmit(data) {
        return Promise.resolve(data)
    }

    handleSuccess(response) {
        if (response) {
            this.$message.success('创建成功')
            setTimeout(() => {
                this.$router.replace("/employee")
            }, 3000);
        } else {
            this.$message.warning('创建失败，编号不能重复')
        }
    }

    handleError(response) {
        this.$message.error('创建失败')
    }

    handleEnd(response) {
        //this.$message.success('处理结束')
    }
}