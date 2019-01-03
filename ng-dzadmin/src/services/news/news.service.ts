import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { News } from "entities";
import { PagedResultDto } from "@shared/component-base";

@Injectable()
export class NewsService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    /**
     * 获取所有资讯信息
     */
    getNewsPage(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/News/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.totalCount = data.totalCount;
            result.items = data.items;
            return result;
        }));
    }

    /**
     * 获取单条资讯信息
     * @param id 
     */
    getnewsById(id: string): Observable<News> {
        let url_ = "/api/services/app/News/GetByIdAndType";
        let params = { 'id': id };
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return News.fromJS(data);
        }))

    }

    /**
     * 新增或修改资讯
     * @param input 
     */
    update(input: News): Observable<News> {
        let url_ = "/api/services/app/News/CreateOrUpdateDto";
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    /**
     * 删除资讯
     * @param id 
     */
    delete(id: string): Observable<any> {
        let url_ = "/api/services/app/News/Delete";
        let params = { 'Id': id }
        return this._commonhttp.delete(url_, params);
    }
}




