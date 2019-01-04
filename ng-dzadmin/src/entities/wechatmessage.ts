export class WechatMessage {
    id: string;
    keyWord: string;
    matchMode: number;
    msgType: number;
    content: string;
    triggerType: number;
    title: string;
    desc: string;
    picLink: string;
    url: string;
    matchModeName: string;
    msgTypeName: string;
    triggerTypeName: string;
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
            this.triggerType = data["triggerType"];
            this.title = data["title"];
            this.desc = data["desc"];
            this.picLink = data["picLink"];
            this.url = data["url"];
            this.matchModeName = data["matchModeName"];
            this.msgTypeName = data["msgTypeName"];
            this.triggerTypeName = data["triggerTypeName"];
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
        data["triggerType"] = this.triggerType;
        data["title"] = this.title;
        data["desc"] = this.desc;
        data["picLink"] = this.picLink;
        data["url"] = this.url;
        data["matchModeName"] = this.matchModeName;
        data["msgTypeName"] = this.msgTypeName;
        data["triggerTypeName"] = this.triggerTypeName;
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