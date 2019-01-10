export class IntegralStatic {
    groupName: string;
    growIntegral: number;
    depleteIntegral: number;

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
            this.groupName = data["groupName"];
            this.growIntegral = data["growIntegral"];
            this.depleteIntegral = data["depleteIntegral"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["groupName"] = this.groupName;
        data["growIntegral"] = this.growIntegral;
        data["depleteIntegral"] = this.depleteIntegral;
        return data;
    }
    static fromJS(data: any): IntegralStatic {
        let result = new IntegralStatic();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): IntegralStatic[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new IntegralStatic();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new IntegralStatic();
        result.init(json);
        return result;
    }
}