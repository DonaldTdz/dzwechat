export class ExchangeSummary {
    shopName: string;
    specification: string;
    exchangeNum: number;

    constructor(data?: any) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }
    init(data?: any) {
        if (data) {
            this.shopName = data["shopName"];
            this.specification = data["specification"];
            this.exchangeNum = data["exchangeNum"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["shopName"] = this.shopName;
        data["specification"] = this.specification;
        data["exchangeNum"] = this.exchangeNum;
        return data;
    }
    static fromJS(data: any): ExchangeSummary {
        let result = new ExchangeSummary();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ExchangeSummary[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ExchangeSummary();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ExchangeSummary();
        result.init(json);
        return result;
    }
}