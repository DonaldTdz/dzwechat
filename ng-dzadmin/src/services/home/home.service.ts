import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { HomeInfo, GroupStatistics, IntegralStatic } from "entities";

@Injectable()
export class HomeService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    getHomeInfo(): Observable<HomeInfo> {
        let _url = '/api/services/app/Order/GetHomeInfo';
        return this._commonhttp.get(_url).pipe(map(data => {
            return HomeInfo.fromJS(data);
        }));
    }

    /**
     * 获取积分销售统计（按规格）
     */
    getIntegralStatisByGoods(): Observable<GroupStatistics[]> {
        let _url = '/api/services/app/Good/GetIntegralStatisByGoods';
        return this._commonhttp.get(_url).pipe(map((data) => {
            return GroupStatistics.fromJSArray(data);
        }));
    }

    /**
     * 获取商品销售统计
     */
    getGoodsStatis(): Observable<GroupStatistics[]> {
        let _url = '/api/services/app/Good/GetGoodsStatis';
        return this._commonhttp.get(_url).pipe(map((data) => {
            return GroupStatistics.fromJSArray(data);
        }));
    }

    /**
     * 获取积分变化统计（按月）
     * @param input （半年、一年）
     */
    getIntegralByMonth(input: string): Observable<IntegralStatic[]> {
        let _url = '/api/services/app/IntegralDetail/GetIntegralDetailByMonthAsync';
        let params = { 'searchMonth': input }
        return this._commonhttp.get(_url, params).pipe(map((data) => {
            return IntegralStatic.fromJSArray(data);
        }))
    }
}