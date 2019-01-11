import { Injectable } from '@angular/core';
import { CommonHttpClient } from '../common-httpclient';
import { map } from 'rxjs/operators';
import { News, WechatUser, ApiResult, Shop } from '../../entities';
import { Observable } from 'rxjs';

@Injectable()
export class ExchangeService {

    constructor(private commonHttpClient: CommonHttpClient) {
    }

    getShopList(params: any): Observable<Shop[]> {
        let url_ = "/api/services/app/Shop/GetShopList";
        return this.commonHttpClient.get(url_, params).pipe(map(data => {
            return Shop.fromJSArray(data.result);
        }));
    }

    getShopInfo(params: any): Observable<Shop> {
        let url_ = "/api/services/app/Shop/GetShopInfoById";
        return this.commonHttpClient.get(url_, params).pipe(map(data => {
            return Shop.fromJS(data.result);
        }));
    }
}
