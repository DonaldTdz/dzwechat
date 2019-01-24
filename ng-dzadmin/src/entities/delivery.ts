export class Delivery {
    id: string;
    name: string;
    phone: string;
    address: string;
    isDefault: boolean;
    creationTime: Date;
    province: string;
    city: string;
    area: string;
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
            this.name = data["name"];
            this.phone = data["phone"];
            this.address = data["address"];
            this.isDefault = data["isDefault"];
            this.creationTime = data["creationTime"];
            this.province = data["province"];
            this.city = data["city"];
            this.area = data["area"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["phone"] = this.phone;
        data["address"] = this.address;
        data["isDefault"] = this.isDefault;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): Delivery {
        let result = new Delivery();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Delivery[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Delivery();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Delivery();
        result.init(json);
        return result;
    }
}