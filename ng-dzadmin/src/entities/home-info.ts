
export class HomeInfo {
    weChatUsersTotal: string;
    integralTotal: number;
    orderTotal: string;
    pendingOrderTotal: string;
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
            this.weChatUsersTotal = data["weChatUsersTotal"];
            this.integralTotal = data["integralTotal"];
            this.orderTotal = data["orderTotal"];
            this.pendingOrderTotal = data["pendingOrderTotal"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["weChatUsersTotal"] = this.weChatUsersTotal;
        data["integralTotal"] = this.integralTotal;
        data["orderTotal"] = this.orderTotal;
        data["pendingOrderTotal"] = this.pendingOrderTotal;
        return data;
    }
    static fromJS(data: any): HomeInfo {
        let result = new HomeInfo();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): HomeInfo[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new HomeInfo();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new HomeInfo();
        result.init(json);
        return result;
    }
}
