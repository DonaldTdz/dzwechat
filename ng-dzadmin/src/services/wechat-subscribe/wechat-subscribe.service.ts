import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { WechatSubscribe } from "entities";

@Injectable()
export class WechatSubscribeService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    /**
     * 获取图文消息
     * @param param 
     */
    getSubscribe(): Observable<WechatSubscribe> {
        let _url = "/api/services/app/WechatSubscribe/GetWechatSubscribeInfo";
        return this._commonhttp.get(_url).pipe(map(data => {
            return WechatSubscribe.fromJS(data);
        }));
    }

    /**
     * 获取单个图文消息
     * @param id 
     */
    getSubscribeById(id: string): Observable<WechatSubscribe> {
        let _url = "/api/services/app/WechatSubscribe/GetById";
        let param = { 'id': id };
        return this._commonhttp.get(_url, param).pipe(map(data => {
            return WechatSubscribe.fromJS(data);
        }));
    }

    /**
     * 更新图文消息
     * @param input 
     */
    update(input: WechatSubscribe): Observable<WechatSubscribe> {
        let _url = "/api/services/app/WechatSubscribe/CreateOrUpdateDto";
        return this._commonhttp.post(_url, input).pipe(map(data => {
            return data;
        }))
    }


    /**
     * 删除图文消息
     * @param id 
     */
    delete(id: string): Observable<any> {
        let _url = "/api/services/app/WechatSubscribe/Delete";
        let param = { 'id': id };
        return this._commonhttp.delete(_url, param);
    }
}