import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { Goods, Category, SelectGroup } from "entities";
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

    getCategoryGroup(): Observable<SelectGroup[]> {
        let url_ = "/api/services/app/Category/GetCategorySelectGroup";
        return this._commonhttp.get(url_).pipe(map(data => {
            return SelectGroup.fromJSArray(data);
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

    getCategoryTrees(params: any): Observable<any> {
        let url_ = "/api/services/app/Category/GetCategoryTreesAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }

    updateCategory(params: any): Observable<Category> {
        let url_ = "/api/services/app/Category/CreateOrUpdate";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return Category.fromJS(data);
        }));
    }

    updateGoods(params: any): Observable<Goods> {
        let url_ = "/api/services/app/Good/CreateOrUpdate";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return Goods.fromJS(data);
        }));
    }
    updateGoodsStatus(params: any): Observable<Goods> {
        let url_ = "/api/services/app/Good/ChangeStatus";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return Goods.fromJS(data);
        }));
    }

    filesPostsBase64(params: any): Observable<any> {
        let url_ = "/WeChatFile/FilesPostsBase64";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return data;
        }));
    }
}
