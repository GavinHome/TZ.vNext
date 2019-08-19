interface Array<T> {
    sum(): T;
}

Array.prototype.sum = function() {
    if(this.length === 0) return 0;
    return this.reduce((x, y) => x + y);
}

