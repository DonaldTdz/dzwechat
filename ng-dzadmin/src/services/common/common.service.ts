import { Inject, Optional, Injectable } from "@angular/core";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";

@Injectable()
export class CommonService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
    }

    checkOldPassword(oldPassword: string): Observable<boolean> {
        let url_ = "/api/services/app/User/CheckOldPasswordAsync";
        let params = { 'oldPassword': oldPassword };
        return this._commonhttp.post(url_, null, params).pipe(map(data => {
            console.log(data);
            return <boolean>data;
        }));
    }

    updatePassword(password: string): Observable<boolean> {
        let url_ = "/api/services/app/User/PostUpdatePasswordAsync";
        let params = { 'password': password };
        return this._commonhttp.post(url_, null, params).pipe(map(data => {
            return <boolean>data;
        }));
    }

}
