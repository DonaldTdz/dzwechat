import { Component, Injector, OnInit } from '@angular/core';
import { JWeiXinService, ToptipsService } from 'ngx-weui';
import { AppComponentBase } from '../../../shared/app-component-base';
import { Router } from '@angular/router';
import { Shop, WechatUser } from '../../../entities';

@Component({
    selector: 'scan-exchange',
    templateUrl: 'scan-exchange.component.html'
})
export class ScanExchangeComponent extends AppComponentBase implements OnInit {
    shop: Shop;
    user: WechatUser;
    constructor(injector: Injector,
        // private shopService: ShopService,
        // private wechatUserService: WechatUserService,
        private router: Router,
        private wxService: JWeiXinService,
        private srv: ToptipsService) {
        super(injector);

    }

    ngOnInit() {
        /*    this.wxService.get().then((res) => {
               if (!res) {
                   console.warn('jweixin.js 加载失败');
                   return;
               }
               let url = this.CurrentUrl;//encodeURIComponent(location.href.split('#')[0]);
               this.settingsService.getJsApiConfig(url).subscribe(result => {
                   if (result) {
                       result.jsApiList = ['scanQRCode'];//指定调用的接口名
                       //console.log(result.toJSON());
                       // 1、通过config接口注入权限验证配置
                       wx.config(result.toJSON());
                       // 2、通过ready接口处理成功验证
                       wx.ready(() => {
                           // 注册各种onMenuShareTimeline & onMenuShareAppMessage
                       });
                       // 2、通过error接口处理失败验证
                       wx.error(() => {
                       });
                   }
               });
           });*/

        this.settingsService.getUser().subscribe(result => {
            this.user = result;
            if (this.user) {
                if (!this.user.isShopManager) {//不是店铺管理员
                    this.router.navigate(['/marketing-exchange/bind-shop']);
                } else {
                    // this.shopService.GetShopByOpenId(this.WUserParams)
                    //     .subscribe(result => {
                    //         this.shop = result;
                    //         if (!this.shop) {//如果没有店铺 需要新增
                    //             this.router.navigate(['/shops/shop-add']);
                    //         }
                    //     });
                }
            } else {
                this.router.navigate(['/personals/bind-retailer']);
            }
        });
    }
}
