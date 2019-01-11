export class Exchange {
    id: string;
    orderDetailId: string;
    exchangeCode: number;
    shopId: string;
    userId: string;
    wechatUserId: string;
    creationTime: Date;
    logisticsCompany: string;
    logisticsNo: string;
    shopName: string;
    orderNumber: string;
    specification: string;
    exchangeCodeName: string;
    orderId: string;
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
            this.id = data["id"];
            this.orderDetailId = data["orderDetailId"];
            this.exchangeCode = data["exchangeCode"];
            this.shopId = data["shopId"];
            this.userId = data["userId"];
            this.creationTime = data["creationTime"];
            this.logisticsCompany = data["logisticsCompany"];
            this.logisticsNo = data["logisticsNo"];
            this.wechatUserId = data["wechatUserId"];
            this.shopName = data["shopName"];
            this.orderNumber = data["orderNumber"];
            this.specification = data["specification"];
            this.exchangeCodeName = data["exchangeCodeName"];
            this.orderId = data["orderId"];

        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["orderDetailId"] = this.orderDetailId;
        data["exchangeCode"] = this.exchangeCode;
        data["shopId"] = this.shopId;
        data["userId"] = this.userId;
        data["creationTime"] = this.creationTime;
        data["logisticsCompany"] = this.logisticsCompany;
        data["logisticsNo"] = this.logisticsNo;
        data["wechatUserId"] = this.wechatUserId;
        data["shopName"] = this.shopName;
        data["orderNumber"] = this.orderNumber;
        data["specification"] = this.specification;
        data["exchangeCodeName"] = this.exchangeCodeName;
        data["orderId"] = this.orderId;

        return data;
    }
    static fromJS(data: any): Exchange {
        let result = new Exchange();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Exchange[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Exchange();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Exchange();
        result.init(json);
        return result;
    }
}