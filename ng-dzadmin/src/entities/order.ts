export class Order {
    id: string;
    number: string;
    userId: string;
    phone: string;
    integral: number;
    status: number;
    remark: string;
    payTime: Date;
    completeTime: Date;
    cancelTime: Date;
    creationTime: Date;
    creatorUserId: string;
    deliveryName: string;
    deliveryPhone: string;
    deliveryAddress: string;
    statusName: string;
    userName: string;
    nickName: string;
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
            this.number = data["number"];
            this.userId = data["userId"];
            this.phone = data["phone"];
            this.integral = data["integral"];
            this.status = data["status"];
            this.remark = data["remark"];
            this.payTime = data["payTime"];
            this.completeTime = data["completeTime"];
            this.cancelTime = data["cancelTime"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.deliveryName = data["deliveryName"];
            this.deliveryPhone = data["deliveryPhone"];
            this.deliveryAddress = data["deliveryAddress"];
            this.statusName = data["statusName"];
            this.userName = data["userName"];
            this.nickName = data["nickName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["number"] = this.number;
        data["userId"] = this.userId;
        data["phone"] = this.phone;
        data["integral"] = this.integral;
        data["status"] = this.status;
        data["remark"] = this.remark;
        data["payTime"] = this.payTime;
        data["completeTime"] = this.completeTime;
        data["cancelTime"] = this.cancelTime;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["deliveryName"] = this.deliveryName;
        data["deliveryPhone"] = this.deliveryPhone;
        data["deliveryAddress"] = this.deliveryAddress;
        return data;
    }
    static fromJS(data: any): Order {
        let result = new Order();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Order[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Order();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Order();
        result.init(json);
        return result;
    }
}