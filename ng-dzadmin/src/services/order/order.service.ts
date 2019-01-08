import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { Delivery, Order, Exchange } from "entities";

@Injectable()
export class OrderService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }
    getAll(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Order/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getOrderById(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Order/GetPagedById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getOrderInfoById(params: any): Observable<Order> {
        let url_ = "/api/services/app/Order/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Order.fromJS(data);
        }));
    }

    getAddressById(params: any): Observable<Delivery[]> {
        let url_ = "/api/services/app/Delivery/GetNoPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            let result: Delivery[] = [];
            result = data.items;
            return result;
        }));
    }

    getOrderDetailById(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/OrderDetail/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getExchangeByOrderDetailId(params: any): Observable<Exchange> {
        let url_ = "/api/services/app/Exchange/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Exchange.fromJS(data);
        }));
    }

    createExchange(params: any): Observable<Exchange> {
        let url_ = "/api/services/app/Exchange/CreateOrUpdate";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return Exchange.fromJS(data);
        }));
    }
}




