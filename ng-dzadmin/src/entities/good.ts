export class Goods {
    id: string;
    specification: string;
    photoUrl: string;
    desc: string;
    stock: number;
    unit: string;
    exchangeCode: string;
    categoryId: number;
    integral: number;
    barCode: string;
    searchCount: number;
    isAction: boolean;
    creationTime: Date;
    creatorUserId: number;
    categoryName: string;
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
            this.specification = data["specification"];
            this.photoUrl = data["photoUrl"];
            this.desc = data["desc"];
            this.stock = data["stock"];
            this.unit = data["unit"];
            this.exchangeCode = data["exchangeCode"];
            this.categoryId = data["categoryId"];
            this.integral = data["integral"];
            this.barCode = data["barCode"];
            this.searchCount = data["searchCount"];
            this.isAction = data["isAction"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.categoryName = data["categoryName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["specification"] = this.specification;
        data["photoUrl"] = this.photoUrl;
        data["desc"] = this.desc;
        data["stock"] = this.stock;
        data["unit"] = this.unit;
        data["exchangeCode"] = this.exchangeCode;
        data["categoryId"] = this.categoryId;
        data["integral"] = this.integral;
        data["barCode"] = this.barCode;
        data["searchCount"] = this.searchCount;
        data["isAction"] = this.isAction;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        return data;
    }
    static fromJS(data: any): Goods {
        let result = new Goods();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Goods[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Goods();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Goods();
        result.init(json);
        return result;
    }
}