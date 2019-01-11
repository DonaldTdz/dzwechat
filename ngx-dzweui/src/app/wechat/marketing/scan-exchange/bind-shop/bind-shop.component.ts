import { Component, Injector, ViewChild, OnInit } from '@angular/core';
import { ToptipsService, ToptipsComponent, ToastComponent } from 'ngx-weui';
import { Router } from '@angular/router';
import { AppConsts } from '../../../../shared/AppConsts';
import { AppComponentBase } from '../../../../shared/app-component-base';
import { WechatUserService, ExchangeService } from '../../../../services';
import { Shop } from '../../../../entities';

@Component({
    selector: 'bind-shop',
    templateUrl: 'bind-shop.component.html'
})
export class BindShopComponent extends AppComponentBase implements OnInit {
    res: any = {};
    @ViewChild('toptips') toptips: ToptipsComponent;
    @ViewChild('loading') loadingToast: ToastComponent;
    host = AppConsts.remoteServiceBaseUrl;
    shopList: Shop[] = [];
    constructor(injector: Injector,
        private wechatUserService: WechatUserService,
        private exchangeService: ExchangeService,
        private router: Router,
        private srv: ToptipsService) {
        super(injector);
    }
    ngOnInit() {
        this.getShopList();
    }

    getShopList() {
        let params: any = {};
        this.exchangeService.getShopList(params)
            .subscribe((result) => {
                if (result) {
                    this.res.shopId = result[0].id;
                    console.log(result);
                    this.shopList = result;
                }
            });
    }

    onSave() {
        this.res.openId = this.settingsService.openId;
        this.loadingToast._showd = true;
        this.wechatUserService.BindWeChatUserAsync(this.res).subscribe(result => {
            this.loadingToast.onHide();
            if (result.code == 0) {//成功
                this.srv['success']('绑定成功');
                this.settingsService.setUser(result.data);
                this.router.navigate(["/marketing-exchange/exchange"]);
            } else {//失败
                this.srv['warn'](result.msg);
            }
        });
    }
}
