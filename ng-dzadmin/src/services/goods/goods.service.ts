import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { Goods, Category } from "entities";
import { PagedResultDto } from "@shared/component-base";

@Injectable()
export class GoodsService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    getAll(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Good/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getGoodsById(params: any): Observable<Goods> {
        let url_ = "/api/services/app/Good/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Goods.fromJS(data);
        }));
    }

    getCategoryById(params: any): Observable<Category> {
        let url_ = "/api/services/app/Category/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Category.fromJS(data);
        }));
    }
}
