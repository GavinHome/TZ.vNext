interface Date {
    toFormatString(format: string): string;
}

Date.prototype.toFormatString = function (format: string): string {
    if (this == null) {
        return '';
    }

    var day = this.getDay();
    var month = this.getMonth() + 1;
    var year = this.getFullYear();
    return format.replace("yyyy", year.toString()).replace("MM", month.toString()).replace("dd", day.toString())
}