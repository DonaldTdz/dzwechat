export class WechatSubscribe {
    id: string;
    msgType: number;
    content: string;
    title: string;
    desc: string;
    picLink: string;
    url: string;
    creationTime: Date;
    creatorUserId: number;
    lastModificationTime: Date;
    lastModifierUserId: number;
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
            this.msgType = data["msgType"];
            this.content = data["content"];
            this.title = data["title"];
            this.desc = data["desc"];
            this.picLink = data["picLink"];
            this.url = data["url"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["msgType"] = this.msgType;
        data["content"] = this.content;
        data["title"] = this.title;
        data["desc"] = this.desc;
        data["picLink"] = this.picLink;
        data["url"] = this.url;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        return data;
    }
    static fromJS(data: any): WechatSubscribe {
        let result = new WechatSubscribe();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): WechatSubscribe[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new WechatSubscribe();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new WechatSubscribe();
        result.init(json);
        return result;
    }
}
