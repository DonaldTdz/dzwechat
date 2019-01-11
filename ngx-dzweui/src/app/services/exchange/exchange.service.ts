import { Injectable } from '@angular/core';
import { CommonHttpClient } from '../common-httpclient';
import { map } from 'rxjs/operators';
import { News, WechatUser, ApiResult, Shop, OrderDetail, Order } from '../../entities';
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

    getOrderDetailInfo(params: any): Observable<OrderDetail[]> {
        let url_ = "/api/services/app/Shop/GetExchangeGoodsByIdAsync";
        return this.commonHttpClient.get(url_, params).pipe(map(data => {
            return OrderDetail.fromJSArray(data.result);
        }));
    }

    getOrderInfo(params: any): Observable<Order> {
        let url_ = "/api/services/app/Exchange/GetOrderByIdAsync";
        return this.commonHttpClient.get(url_, params).pipe(map(data => {
            return Order.fromJS(data.result);
        }));
    }

    exchangeGoods(params: any): Observable<ApiResult<any>> {
        let url_ = "/api/services/app/Exchange/ExchangeGoods";
        return this.commonHttpClient.post(url_, params).pipe(map(data => {
            let result = new ApiResult<any>();
            result.code = data.result.code;
            result.msg = data.result.msg;
            result.data = data.result.data;
            return result;
        }));
    }
}
