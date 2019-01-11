export class DropDown {
    text: string;
    value: number;

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
            this.text = data["text"];
            this.value = data["value"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["text"] = this.text;
        data["value"] = this.value;
        return data;
    }
    static fromJS(data: any): DropDown {
        let result = new DropDown();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): DropDown[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new DropDown();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new DropDown();
        result.init(json);
        return result;
    }
}