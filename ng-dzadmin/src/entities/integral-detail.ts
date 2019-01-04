export class IntegralDetail {
    id: string;
    userId: string;
    initialIntegral: number;
    integral: number;
    finalIntegral: number;
    type: number;
    desc: string;
    refId: string;
    creationTime: Date;
    typeName: string;
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
            this.userId = data["userId"];
            this.initialIntegral = data["initialIntegral"];
            this.integral = data["integral"];
            this.finalIntegral = data["finalIntegral"];
            this.type = data["type"];
            this.desc = data["desc"];
            this.refId = data["refId"];
            this.creationTime = data["creationTime"];
            this.typeName = data["typeName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["userId"] = this.userId;
        data["initialIntegral"] = this.initialIntegral;
        data["integral"] = this.integral;
        data["finalIntegral"] = this.finalIntegral;
        data["type"] = this.type;
        data["desc"] = this.desc;
        data["refId"] = this.refId;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): IntegralDetail {
        let result = new IntegralDetail();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): IntegralDetail[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new IntegralDetail();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new IntegralDetail();
        result.init(json);
        return result;
    }
}