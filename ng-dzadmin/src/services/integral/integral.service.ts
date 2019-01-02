import { IntegralDetail } from "entities";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";


@Injectable()
export class IntegralDetailService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    getIntegralDetailById(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/IntegralDetail/GetPagedById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }))

    }
}




