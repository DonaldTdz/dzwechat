export class VipUser {
    id: string;
    vipCode: string;
    name: string;
    phone: string;
    idNumber: string;
    purchaseAmount: number;
    creationTime: Date;
    wechatId: string;
    bindWechatUser: string;
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
            this.vipCode = data["vipCode"];
            this.name = data["name"];
            this.phone = data["phone"];
            this.idNumber = data["idNumber"];
            this.purchaseAmount = data["purchaseAmount"];
            this.creationTime = data["creationTime"];
            this.wechatId = data["wechatId"];
            this.bindWechatUser = data["bindWechatUser"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["vipCode"] = this.vipCode;
        data["name"] = this.name;
        data["phone"] = this.phone;
        data["idNumber"] = this.idNumber;
        data["purchaseAmount"] = this.purchaseAmount;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): VipUser {
        let result = new VipUser();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): VipUser[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new VipUser();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new VipUser();
        result.init(json);
        return result;
    }
}