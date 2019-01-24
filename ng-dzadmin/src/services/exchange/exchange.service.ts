import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { ApiResult } from "entities/api-result";
import { SelectGroup } from "entities";

@Injectable()
export class ExchangeService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    /**
     * 获取图文消息
     * @param param 
     */
    getExchangeDetail(param: any): Observable<PagedResultDto> {
        let _url = "/api/services/app/Exchange/GetExchangeDetail";
        return this._commonhttp.get(_url, param).pipe(map(data => {
            var result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getExchangeSummary(param: any): Observable<PagedResultDto> {
        let _url = "/api/services/app/Exchange/GetExchangeSummary";
        return this._commonhttp.get(_url, param).pipe(map(data => {
            var result = new PagedResultDto();
            result.items = data;
            result.totalCount = data.length - 1 <= 0 ? 0 : data.length - 1;
            // result.items = data;
            return result;
        }));
    }

    /**
     * 导出兑换明细
     * @param param 
     */
    exportExchangeDetail(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/Exchange/ExportExchangeDetail';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);

        }));

    }
    exportSummaryExchange(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/Exchange/ExportExchangeSummary';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);

        }));

    }
    getExchangeByShopId(param: any): Observable<PagedResultDto> {
        let _url = "/api/services/app/Exchange/GetPagedByShopId";
        return this._commonhttp.get(_url, param).pipe(map(data => {
            var result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getShopTypeAsync(): Observable<SelectGroup[]> {
        let url_ = "/api/services/app/Exchange/GetShopTypeAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return SelectGroup.fromJSArray(data);
        }));
    }
}