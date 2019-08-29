let FileSaver = require('file-saver');

function getHeaderFilename(headers) {
    try {
        const rawFilename = headers.get("Content-Disposition").split("filename=")[1];
        return decodeURIComponent(rawFilename.trim().slice(1, -1)); // Removes "
    } catch (err) {
        return '';
    }
}

function generateErrorData(response) {
    return {
        status: response.status
    };
}

export default function fetchDownload(url, options) {

    return fetch(url, Object.assign({
        credentials: "same-origin"
    }, options)).then((res) => {
        if (!res.ok) return Promise.reject(generateErrorData(res));
        return res.blob().then((blobData) => {
            //FileSaver.saveAs(blobData, getHeaderFilename(res.headers));
            (FileSaver as any).saveAs(blobData, getHeaderFilename(res.headers));
        });
    });
}