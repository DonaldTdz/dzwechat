import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { WechatMessage } from "entities";

@Injectable()
export class WeChatMessageService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    /**
     * 获取微信消息
     * @param param 
     */
    getMessagePage(param: any): Observable<PagedResultDto> {
        let _url = "/api/services/app/WechatMessage/GetPaged";
        return this._commonhttp.get(_url, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    /**
     * 获取单个微信消息
     * @param id 
     */
    getMessageById(id: string): Observable<WechatMessage> {
        let _url = "/api/services/app/WechatMessage/GetById";
        let param = { 'id': id };
        return this._commonhttp.get(_url, param).pipe(map(data => {
            return WechatMessage.fromJS(data);
        }));
    }

    /**
     * 更新微信消息
     * @param input 
     */
    update(input: WechatMessage): Observable<WechatMessage> {
        let _url = "/api/services/app/WechatMessage/CreateOrUpdateDto";
        return this._commonhttp.post(_url, input).pipe(map(data => {
            return data;
        }))
    }


    /**
     * 删除微信消息
     * @param id 
     */
    delete(id: string): Observable<any> {
        let _url = "/api/services/app/WechatMessage/Delete";
        let param = { 'id': id };
        return this._commonhttp.delete(_url, param);
    }


}