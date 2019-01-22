import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { VipUser } from "entities";

@Injectable()
export class VipUserService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }
    getAll(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/VipUser/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }
    getVipPurchase(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/VipPurchase/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getVipUserById(params: any): Observable<VipUser> {
        let url_ = "/api/services/app/VipUser/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return VipUser.fromJS(data);
        }));
    }
}