import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { WechatUser } from "entities";
import { PagedResultDto } from "@shared/component-base";

@Injectable()
export class WechatUserService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    getAll(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/WechatUser/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getWechatUserById(params: any): Observable<WechatUser> {
        let url_ = "/api/services/app/WechatUser/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return WechatUser.fromJS(data);
        }));
    }

}
