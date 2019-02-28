import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import { Button, Message } from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import fetchDownload from "fetch-download"
import { TzMessageConst } from "../common/TzCommonConst";

Vue.use(Button);

@Component({
    props: ["url", "title", "id"]
})
export default class TzDownload extends Vue {
    @Prop() url!: string;
    @Prop() title!: string;
    @Prop() id!: string;

    onDownload(file) {
        if (!this.url) {
            Message.error(TzMessageConst.DOWNLOAD_FAIL_MESSAGE)
        }

        let url: string = this.url

        if (!url) {
            url = "/File/DownloadTemplateFile"
        }

        fetchDownload(url, {
            method: "post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ key: this.id })
        }).catch((err) => {
            console.error(err);
            Message.error(TzMessageConst.DOWNLOAD_FAIL_MESSAGE)
        });
    }
}
