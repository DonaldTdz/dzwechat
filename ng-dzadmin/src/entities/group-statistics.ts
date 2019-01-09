
export class GroupStatistics {
    groupName: string;
    total: number;
    integralTotal: number;
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
            this.total = data["total"];
            this.integralTotal = data["integralTotal"];

        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["groupName"] = this.groupName;
        data["total"] = this.total;
        data["integralTotal"] = this.integralTotal;
        return data;
    }
    static fromJS(data: any): GroupStatistics {
        let result = new GroupStatistics();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): GroupStatistics[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new GroupStatistics();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new GroupStatistics();
        result.init(json);
        return result;
    }
}
