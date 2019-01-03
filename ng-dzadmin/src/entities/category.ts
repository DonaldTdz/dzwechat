export class Category {
    id: number;
    name: string;
    seq: number;
    creationTime: Date;
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
            this.seq = data["seq"];
            this.creationTime = data["creationTime"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["seq"] = this.seq;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): Category {
        let result = new Category();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Category[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Category();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Category();
        result.init(json);
        return result;
    }
}

export class SelectGroup {
    text: string;
    value: string;
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

    static fromJS(data: any): SelectGroup {
        let result = new SelectGroup();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): SelectGroup[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new SelectGroup();
            item.init(result);
            array.push(item);
        });

        return array;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["text"] = this.text;
        data["value"] = this.value;
        return data;
    }
}