interface StringConstructor {  
    isNullOrEmpty: (val: any) => boolean;
    formatMoney: (val: string, n: number) => string;
}

String.isNullOrEmpty = function (val: any): boolean {  
    if (val === undefined || val === null || val.trim() === '') {
        return true;
    }
    return false;
};


String.formatMoney = function(val: string, n: number){
    var result = '';
    var money = parseFloat(val);
    if(isNaN(money)){
        return result;
    }

    var moneyStr = money.toFixed(n);
    var integer = moneyStr.split('.')[0];
    var digits = moneyStr.split('.')[1];
    result = integer.replace(/(\d)(?=(?:\d{3})+$)/g, '$1,');
    if(n >= 1){
        result += `.${digits}`;
    }
    
    return result;
}

