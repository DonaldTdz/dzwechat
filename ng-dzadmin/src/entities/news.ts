export class News {
    id: string;
    title: string;
    author: string;
    type: number;
    coverPhoto: string;
    content: string;
    pushStatus: number;
    pushTime: string;
    linkType: number;
    linkAddress: string;
    isDeleted: boolean;
    creationTime: Date;
    creatorUserId: number;
    lastModificationTime: Date;
    lastModifierUserId: number;
    deletionTime: Date;
    deleterUserId: number;
    typeName: string;
    pushStatusName: string;
    linkTypeName: string;
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
            this.title = data["title"];
            this.author = data["author"];
            this.type = data["type"];
            this.coverPhoto = data["coverPhoto"];
            this.content = data["content"];
            this.pushStatus = data["pushStatus"];
            this.pushTime = data["pushTime"];
            this.linkType = data["linkType"];
            this.linkAddress = data["linkAddress"];
            this.isDeleted = data["isDeleted"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
            this.deletionTime = data["deletionTime"];
            this.deleterUserId = data["deleterUserId"];
            this.typeName = data["typeName"];
            this.pushStatusName = data["pushStatusName"];
            this.linkTypeName = data["linkTypeName"];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["author"] = this.author;
        data["type"] = this.type;
        data["coverPhoto"] = this.coverPhoto;
        data["content"] = this.content;
        data["pushStatus"] = this.pushStatus;
        data["pushTime"] = this.pushTime;
        data["linkType"] = this.linkType;
        data["linkAddress"] = this.linkAddress;
        data["isDeleted"] = this.isDeleted;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        return data;
    }

    static fromJS(data: any): News {
        let result = new News();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): News[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new News();
            item.init(result);
            array.push(item);
        });
        return array;
    }

    clone() {
        const json = this.toJSON();
        let result = new News();
        result.init(json);
        return result;
    }

}

