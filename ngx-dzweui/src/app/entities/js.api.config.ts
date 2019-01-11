export class JsApiConfig {
    debug: boolean;
    appId: string;
    timestamp: number;
    nonceStr: string;
    signature: string;
    jsApiList: string[];

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
            this.debug = data["debug"];
            this.appId = data["appId"];
            this.timestamp = data["timestamp"];
            this.nonceStr = data["nonceStr"];
            this.signature = data["signature"];
            this.jsApiList = data["jsApiList"];
        }
    }

    static fromJS(data: any): JsApiConfig {
        let result = new JsApiConfig();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["debug"] = this.debug;
        data["appId"] = this.appId;
        data["timestamp"] = this.timestamp;
        data["nonceStr"] = this.nonceStr;
        data["signature"] = this.signature;
        data["jsApiList"] = this.jsApiList;
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new JsApiConfig();
        result.init(json);
        return result;
    }
}