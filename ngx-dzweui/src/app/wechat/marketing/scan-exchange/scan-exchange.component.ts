import { Component, Injector, OnInit } from '@angular/core';
import { JWeiXinService, ToptipsService, DialogService, SkinType, DialogConfig } from 'ngx-weui';
import { AppComponentBase } from '../../../shared/app-component-base';
import { Router, Params } from '@angular/router';
import { Shop, WechatUser, OrderDetail, Order } from '../../../entities';
import { ExchangeService } from '../../../services';
import { AppConsts } from '../../../shared/AppConsts';

@Component({
    selector: 'scan-exchange',
    templateUrl: 'scan-exchange.component.html'
})
export class ScanExchangeComponent extends AppComponentBase implements OnInit {
    shop: Shop;
    user: WechatUser;
    orderDetailList: OrderDetail[] = [];
    order: Order = new Order();
    orderBarCode: string;// 订单码
    public DEFCONFIG: DialogConfig = <DialogConfig>{
        skin: 'ios',
        backdrop: true,
        cancel: null,
        confirm: null,
    };
    config = <DialogConfig>{
        title: '拒绝确认',
        content: '请填写拒绝理由，注意简洁明了',
        inputPlaceholder: '拒绝理由',
        inputError: '必填',
        inputRequired: true,
        skin: 'auto',
        type: 'prompt',
        confirm: '拒绝',
        cancel: '取消',
        input: 'textarea',
        inputValue: undefined,
        inputAttributes: {
            maxlength: 140,
            cn: 1
        },
        inputRegex: null
    }
    constructor(injector: Injector,
        private exchangeService: ExchangeService,
        private router: Router,
        private wxService: JWeiXinService,
        private dia: DialogService,
        private srv: ToptipsService) {
        super(injector);
        this.activatedRoute.params.subscribe((params: Params) => {
            this.orderBarCode = params['orderId'];
        });
    }

    ngOnInit() {
        this.wxService.get().then((res) => {
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
        });

        // if (!this.settingsService.openId) {
        //     let params: any = {};
        //     this.exchangeService.GetAuthorizationUrl(params).subscribe((res) => {
        //         location.href = res;
        //     });
        // }

        this.settingsService.getUser().subscribe(result => {
            this.user = result;
            // if (this.user.bindStatus == 1) {
            if (!this.user.isShopManager) {//不是店铺管理员
                this.router.navigate(['/marketing-exchange/bind-shop']);
            } else {
                if (!this.user.shopId) { //绑定店铺
                    this.router.navigate(['/marketing-exchange/bind-shop']);
                } else {
                    let params: any = {};
                    params.shopId = this.user.shopId;
                    this.exchangeService.getShopInfo(params)
                        .subscribe(result => {
                            this.shop = result;
                            if (this.orderBarCode) {
                                this.getOrderInfo();
                            }
                        });
                }
            }
            // }
            //  else {
            //     location.href = AppConsts.remoteServiceBaseUrl + '/Wechat/ExchangeAuth';
            // }
        });
    }

    onSave() {
        let params: any = {};
        params.orderId = this.orderBarCode;
        params.openId = this.settingsService.openId;
        this.exchangeService.getOrderDetailInfo(params).subscribe(result => {
            this.orderDetailList = result;
        });
    }

    //调用微信扫一扫
    wxScanQRCode(): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            wx.scanQRCode({
                needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
                //scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
                scanType: ['qrCode'],
                success: ((res) => {
                    resolve(res.resultStr);
                })
            });
        });
    }

    scanBarCode() {
        this.wxScanQRCode().then((res) => {
            this.setOrderBarCode(res);
        });
    }

    /**
     * 扫码
     */
    setOrderBarCode(res: string) {
        if (res.indexOf('&') != -1) {
            this.orderBarCode = res.split('&')[1].split('=')[1];
            this.getOrderInfo();
        }
        // this.orderBarCode = res;
        // this.getOrderInfo();
    }

    getOrderInfo() {
        let param: any = {};
        param.orderId = this.orderBarCode;
        this.exchangeService.getOrderInfo(param).subscribe(result => {
            if (result) {
                this.order = result;
                if (this.order) {
                    this.getOrderDetail();
                }
            } else {
                this.srv['warn']('没找到匹配订单');
            }
        });
    }
    getOrderDetail() {
        //获取订单详情
        let params: any = {};
        params.orderId = this.orderBarCode;
        params.openId = this.settingsService.openId;
        this.exchangeService.getOrderDetailInfo(params).subscribe(result => {
            if (result) {
                this.orderDetailList = result;
            } else {
                this.srv['warn']('没找到匹配商品');
            }
        });
    }

    onShowBySrv(type: SkinType, backdrop: boolean = true) {
        this.DEFCONFIG = {
            confirm: '是',
            cancel: '否'
        };
        this.config = Object.assign({}, this.DEFCONFIG, <DialogConfig>{
            skin: type,
            backdrop: backdrop,
            content: '确定兑换商品?'
        });
        this.dia.show(this.config).subscribe((res: any) => {
            if (res.value == true) {
                let params: any = {};
                params.Id = this.orderBarCode;
                this.exchangeService.exchangeGoods(params).subscribe(result => {
                    if (result.code == 0) {
                        this.router.navigate(['/marketing-exchange/exchange-success']);
                    } else {
                        this.srv['warn']('兑换失败，请重试');
                    }
                });
            }
        });
        return false;
    }
}