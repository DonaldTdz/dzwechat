export class WechatUser {
    id: string;
    nickName: string;
    openId: string;
    unionId: string;
    userType: number;
    userId: string;
    userName: string;
    phone: string;
    headImgUrl: string;
    address: string;
    integral: number;
    vipUserId: string;
    isShopManager: boolean;
    shopId: string;
    authTime: Date;
    bindStatus: number;
    bindTime: Date;
    unBindTime: Date;
    userTypeName: string;
    bindStatusName: string;
    wxOpenId: string;
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
            this.nickName = data["nickName"];
            this.openId = data["openId"];
            this.unionId = data["unionId"];
            this.userType = data["userType"];
            this.userId = data["userId"];
            this.userName = data["userName"];
            this.phone = data["phone"];
            this.headImgUrl = data["headImgUrl"];
            this.address = data["address"];
            this.integral = data["integral"];
            this.vipUserId = data["vipUserId"];
            this.isShopManager = data["isShopManager"];
            this.shopId = data["shopId"];
            this.authTime = data["authTime"];
            this.bindStatus = data["bindStatus"];
            this.bindTime = data["bindTime"];
            this.unBindTime = data["unBindTime"];
            this.userTypeName = data["userTypeName"];
            this.bindStatusName = data["bindStatusName"];
            this.wxOpenId = data["wxOpenId"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["nickName"] = this.nickName;
        data["openId"] = this.openId;
        data["unionId"] = this.unionId;
        data["userType"] = this.userType;
        data["userId"] = this.userId;
        data["userName"] = this.userName;
        data["phone"] = this.phone;
        data["headImgUrl"] = this.headImgUrl;
        data["address"] = this.address;
        data["integral"] = this.integral;
        data["vipUserId"] = this.vipUserId;
        data["isShopManager"] = this.isShopManager;
        data["shopId"] = this.shopId;
        data["authTime"] = this.authTime;
        data["bindStatus"] = this.bindStatus;
        data["bindTime"] = this.bindTime;
        data["unBindTime"] = this.unBindTime;
        data["wxOpenId"] = this.wxOpenId;
        return data;
    }
    static fromJS(data: any): WechatUser {
        let result = new WechatUser();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): WechatUser[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new WechatUser();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new WechatUser();
        result.init(json);
        return result;
    }
}