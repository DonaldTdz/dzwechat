import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { Shop } from "entities";

@Injectable()
export class ShopService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }
    getAll(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Shop/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getShopById(params: any): Observable<Shop> {
        let url_ = "/api/services/app/Shop/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Shop.fromJS(data);
        }));
    }

    updateShop(params: any): Observable<Shop> {
        let url_ = "/api/services/app/Shop/CreateOrUpdate";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return Shop.fromJS(data);
        }));
    }

    /**
     * 获取直营店（用于DropDown）
     */
    getShopListForDropDown(): Observable<Shop[]> {
        let url_ = "/api/services/app/Shop/GetShopListForDropDown";
        return this._commonhttp.get(url_).pipe(map(data => {
            return Shop.fromJSArray(data);
        }));
    }
}