export class Shop {
    id: string;
    name: string;
    address: string;
    type: number;
    tel: string;
    desc: string;
    retailerId: string;
    coverPhoto: string;
    longitude: number;
    latitude: number;
    verificationCode: string;
    creationTime: Date;
    typeName: string;
    showCoverPhoto: string;
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
            this.address = data["address"];
            this.type = data["type"];
            this.tel = data["tel"];
            this.desc = data["desc"];
            this.retailerId = data["retailerId"];
            this.coverPhoto = data["coverPhoto"];
            this.longitude = data["longitude"];
            this.latitude = data["latitude"];
            this.verificationCode = data["verificationCode"];
            this.creationTime = data["creationTime"];
            this.typeName = data["typeName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["address"] = this.address;
        data["type"] = this.type;
        data["tel"] = this.tel;
        data["desc"] = this.desc;
        data["retailerId"] = this.retailerId;
        data["coverPhoto"] = this.coverPhoto;
        data["longitude"] = this.longitude;
        data["latitude"] = this.latitude;
        data["verificationCode"] = this.verificationCode;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): Shop {
        let result = new Shop();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Shop[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Shop();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Shop();
        result.init(json);
        return result;
    }
}