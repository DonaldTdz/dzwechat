export class ApiResult {
    code: number;
    msg: string;
    data: any;

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
            this.code = data["code"];
            this.msg = data["msg"];
            this.data = data["data"];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["code"] = this.code;
        data["msg"] = this.msg;
        data["data"] = this.data;
        return data;
    }

    static fromJS(data: any): ApiResult {
        let result = new ApiResult();
        result.init(data);
        return result;
    }

}

