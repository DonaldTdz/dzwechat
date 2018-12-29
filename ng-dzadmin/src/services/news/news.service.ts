import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { News } from "entities";

@Injectable()
export class NewsService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    /**
     * 获取所有资讯信息
     */
    getNewsPage(param: any): Observable<PagedResultDtoOfNews> {
        let url_ = "/api/services/app/News/GetPaged?";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            return PagedResultDtoOfNews.fromJS(data);
        }));
    }

    /**
     * 获取单条资讯信息
     * @param id 
     */
    getnewsById(id: string, newsType: string): Observable<News> {
        let url_ = "/api/services/app/News/GetByIdAndType?";
        let params = { 'id': id, 'newsType': newsType }
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return News.fromJS(data);
        }))

    }
}
export class PagedResultDtoOfNews implements IPagedResultDtoOfNews {
    totalCount: number | undefined;
    items: News[] | undefined;

    constructor(data?: IPagedResultDtoOfNews) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.totalCount = data["totalCount"];
            if (data["items"] && data["items"].constructor === Array) {
                this.items = [];
                for (let item of data["items"])
                    this.items.push(News.fromJS(item));
            }
        }
    }

    static fromJS(data: any): PagedResultDtoOfNews {
        data = typeof data === 'object' ? data : {};
        let result = new PagedResultDtoOfNews();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["totalCount"] = this.totalCount;
        if (this.items && this.items.constructor === Array) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }

    clone(): PagedResultDtoOfNews {
        const json = this.toJSON();
        let result = new PagedResultDtoOfNews();
        result.init(json);
        return result;
    }
}
export interface IPagedResultDtoOfNews {
    totalCount: number | undefined;
    items: News[] | undefined;
}




