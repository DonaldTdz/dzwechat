import { Injectable } from '@angular/core';
import { CommonHttpClient } from '../common-httpclient';
import { map } from 'rxjs/operators';
import { News, WechatUser, ApiResult } from '../../entities';
import { Observable } from 'rxjs';

@Injectable()
export class WechatUserService {

    constructor(private commonHttpClient: CommonHttpClient) {
    }

    BindWeChatUserAsync(params: any): Observable<ApiResult<WechatUser>> {
        let url_ = "/api/services/app/WechatUser/BindWeChatUserAsync";
        return this.commonHttpClient.post(url_, params).pipe(map(data => {
            let result = new ApiResult<WechatUser>();
            result.code = data.result.code;
            result.msg = data.result.msg;
            result.data = WechatUser.fromJS(data.result.data);
            return result;
        }));
    }

}
