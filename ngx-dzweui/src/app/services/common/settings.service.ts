import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { JsApiConfig, WechatUser } from '../../entities';
import { CommonHttpClient } from '../common-httpclient';
import { map } from 'rxjs/operators';

@Injectable()
export class SettingsService {

    private user: WechatUser;
    openId: string;
    private jsApiConfig: JsApiConfig;

    constructor(private _commonhttp: CommonHttpClient) { }

    load(): Promise<any> {
        if (this.openId) {
            return new Promise<any>((resolve, reject) => {
                resolve(null);
            });
        } else {
            return new Promise<any>((resolve, reject) => {
                this._commonhttp.get('/Wechat/GetCurrentUserOpenId').subscribe(ret => {
                    if (!ret.success) {
                        console.error('openid获取失败');
                        resolve(null);
                        return;
                    }
                    if (ret.result.code != 0) {
                        console.error(ret.result.msg);
                        resolve(null);
                        return;
                    }
                    this.setUserId(ret.result.data.openId);
                    resolve(ret);
                });
            });
        }
    };

    setUserId(oid: string) {
        this.openId = oid;
    }

    setUser(val: any) {
        this.user = WechatUser.fromJS(val);
    }

    getUser(): Observable<WechatUser> {
        if (this.user) {
            return of(this.user);
        }
        if (this.openId) {
            return this.GetWeChatUserAsync(this.openId).pipe(map((data) => {
                this.setUser(data)
                return this.user;
            }));
        }
        return of(<WechatUser>null);

    }
    getJsApiConfig(url): Observable<JsApiConfig> {
        return this._commonhttp.get('/Wechat/GetJsApiConfigAsync', { url: url }).pipe(map(ret => {
            if (!ret.success) {
                console.error('jsapi 获取失败');
                return null;
            }
            this.jsApiConfig = JsApiConfig.fromJS({
                debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: ret.result.appId, // 必填，公众号的唯一标识
                timestamp: parseInt(ret.result.timestamp), // 必填，生成签名的时间戳
                nonceStr: ret.result.nonceStr, // 必填，生成签名的随机串
                signature: ret.result.signature,// 必填，签名
                jsApiList: [] // 必填，需要使用的JS接口列表
            });
            return this.jsApiConfig;
        }));
    }

    GetWeChatUserAsync(oId: string): Observable<WechatUser> {
        let params: any = {};
        params.openId = oId;
        return this._commonhttp.get('/api/services/app/WechatUser/GetWeChatUserAsync', params).pipe(map(data => {
            return WechatUser.fromJS(data.result);
        }));
    }
}
