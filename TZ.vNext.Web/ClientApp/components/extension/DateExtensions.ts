interface Date {
    toFormatString(format: string): string;
}

Date.prototype.toFormatString = function (format: string): string {
    if (this == null) {
        return "";
    }

    var day = this.getDate();
    var month = this.getMonth() + 1;
    var year = this.getFullYear();
    					
    var hours = this.getHours();
    var minutes = this.getMinutes();
    var seconds = this.getSeconds();

    var result = format.replace("yyyy", year.toString()).replace("MM", month.toString()).replace("dd", day.toString())
    .replace("HH", hours.toString()).replace("mm", minutes.toString()).replace("ss", seconds.toString())

    return result
}