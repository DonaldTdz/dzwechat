export class OrderDetail {
    id: string;
    orderId: string;
    goodsId: string;
    specification: string;
    integral: number;
    unit: string;
    num: number;
    status: number;
    exchangeTime: Date;
    creationTime: Date;
    statusName: string;
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
            this.orderId = data["orderId"];
            this.goodsId = data["goodsId"];
            this.specification = data["specification"];
            this.integral = data["integral"];
            this.unit = data["unit"];
            this.num = data["num"];
            this.status = data["status"];
            this.exchangeTime = data["exchangeTime"];
            this.creationTime = data["creationTime"];
            this.statusName = data["statusName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["orderId"] = this.orderId;
        data["goodsId"] = this.goodsId;
        data["specification"] = this.specification;
        data["integral"] = this.integral;
        data["unit"] = this.unit;
        data["num"] = this.num;
        data["status"] = this.status;
        data["exchangeTime"] = this.exchangeTime;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): OrderDetail {
        let result = new OrderDetail();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): OrderDetail[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new OrderDetail();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new OrderDetail();
        result.init(json);
        return result;
    }
}