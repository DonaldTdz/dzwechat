import { Injectable } from '@angular/core';
import { CommonHttpClient } from '../common-httpclient';
import { map } from 'rxjs/operators';
import { News } from '../../entities';

@Injectable()
export class NewsService {

    constructor(private commonHttpClient: CommonHttpClient) {
    }

    /**
     * @param type
     * 1 烟语课堂
     * 2 新品快讯
     * 3 产品大全
     */
    GetNewsByType(type: 1 | 2 | 3) {
        const url = '/api/services/app/News/GetNewsByTypeAsync';
        return this.commonHttpClient.get(url, { newsType: type }).pipe(map((data) => {
            //console.log(data);
            if(data && data.result){
                return News.fromJSArray(data.result);
            }
            const news: News[] = [];
            return news;
        }));
    }

}
