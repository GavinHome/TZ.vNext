import Vue from "vue";
import { UploadInfo } from "../model/UploadInfo";
import { Component, Prop } from "vue-property-decorator";
import { Upload, Button, Message } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import fetchDownload from "fetch-download"
import { TzMessageConst } from "../common/TzCommonConst";
import { TzFetch } from "../common/TzFetch";

Vue.use(Upload);
Vue.use(Button);

@Component({
    props: ["value", "tips", "name", "limit"],
    model: {
        prop: 'value',
        event: 'change'
    }
})
export default class TzUploader extends Vue {
    @Prop([]) value!: UploadInfo[];
    @Prop({ default: '' }) tips!: string;
    @Prop({ default: Number.MAX_VALUE }) limit!: number;
    @Prop({ default: 'files' }) name!: string;
    onPreview(file) {
        fetchDownload("/File/DownloadFile", {
            method: "post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ id: file.response ? file.response[0].Id : file.id })
        }).catch((err) => {
            console.error(err); // {status: 404}            
            Message.error(TzMessageConst.DOWNLOAD_FAIL_MESSAGE)
        });
    }

    onRemove(file, files) {
        TzFetch.Post("/File/DelFile", { id: file.response ? file.response[0].Id : file.id }).then(res => {
            if (!res) {
                Message.error(TzMessageConst.DELETEFILE_FAIL_MESSAGE)
            }
        }).catch(e => {
            Message.error(TzMessageConst.DELETEFILE_FAIL_MESSAGE)
        });

        //this.$emit('change', files.map(({ response }) => response))

        // this.$emit('change', files.map(({ response } ) => Object({
        //   id: response.Id,
        //   name: response.Name,
        //   url: response.Url,
        //   ext: response.Ext,
        //   createAt: response.CreateAt
        // })))
    }

    onChange(file, files) {
        console.log(file)
    }

    onSuccess(res, file, files) {
        if (res.length <= 0) {
            Message.error(TzMessageConst.UPLOAD_FAIL_MESSAGE)
            return false;
        }

        // this.$emit('change', file)
    }

    onError(err, file, files) {
        Message.error(TzMessageConst.UPLOAD_FAIL_MESSAGE)
    }

    onProgress(e, file, files) {
        console.log(e)
    }

    beforeUpload(file) {
        console.log(file)
    }

    beforeRemove(file, files) {
        console.log(file)
    }

    onExceed(file, files) {
        Message.error(TzMessageConst.UPLOAD_EXCEED_MESSAGE);
    }
}
