export class WechatMessage {
    id: string;
    keyWord: string;
    matchMode: number;
    msgType: number;
    content: string;
    triggertype: number;
    title: string;
    desc: string;
    piclink: string;
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
            this.keyWord = data["keyWord"];
            this.matchMode = data["matchMode"];
            this.msgType = data["msgType"];
            this.content = data["content"];
            this.triggertype = data["triggertype"];
            this.title = data["title"];
            this.desc = data["desc"];
            this.piclink = data["piclink"];
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
        data["keyWord"] = this.keyWord;
        data["matchMode"] = this.matchMode;
        data["msgType"] = this.msgType;
        data["content"] = this.content;
        data["triggertype"] = this.triggertype;
        data["title"] = this.title;
        data["desc"] = this.desc;
        data["piclink"] = this.piclink;
        data["url"] = this.url;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        return data;
    }
    static fromJS(data: any): WechatMessage {
        let result = new WechatMessage();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): WechatMessage[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new WechatMessage();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new WechatMessage();
        result.init(json);
        return result;
    }
}