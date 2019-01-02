import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { Delivery } from "entities";

@Injectable()
export class OrderService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
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

    getAddressById(params: any): Observable<Delivery[]> {
        let url_ = "/api/services/app/Delivery/GetNoPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            let result: Delivery[] = [];
            result = data.items;
            return result;
        }));
    }
}




