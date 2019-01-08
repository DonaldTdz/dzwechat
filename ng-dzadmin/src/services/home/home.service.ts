import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { HomeInfo } from "entities";

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
}