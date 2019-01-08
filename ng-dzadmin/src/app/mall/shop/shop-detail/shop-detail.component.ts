import { Component, OnInit } from '@angular/core';
import { Shop } from 'entities';
import { ShopService } from 'services';
import { Router, ActivatedRoute } from '@angular/router';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { AppConsts } from '@shared/AppConsts';
import { UploadFile } from 'ng-zorro-antd';

@Component({
    selector: 'shop-detail',
    templateUrl: 'shop-detail.component.html',
    styleUrls: ['shop-detail.component.less']
})
export class ShopDetailComponent implements OnInit {
    id: string;
    loading = false;
    shop: Shop = new Shop();
    actionUrl = '';
    cardTitle: string;
    exchangeCodes: string = '';
    splitCodes: string[];
    isOnline: boolean;
    shopTypes: any[] = [{ text: '直营店', value: 1 }];

    constructor(private shopService: ShopService
        , private notify: NotifyService
        , private router: Router
        , private actRouter: ActivatedRoute
        // , private modal: NzModalService
        // , private msg: NzMessageService
    ) {
        this.actionUrl = AppConsts.remoteServiceBaseUrl + '/WeChatFile/MarketingInfoPosts?fileName=shop';
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.getShop();
    }

    getShop() {
        if (this.id) {
            this.cardTitle = "店铺详情";
            let params: any = {};
            params.Id = this.id;
            this.shopService.getShopById(params).subscribe((result) => {
                this.shop = result;
            });

        } else {
            this.cardTitle = "新建店铺";
            this.shop.type = 1;
        }
    }

    save() {
        this.loading = true;
        this.shopService.updateShop(this.shop)
            .subscribe((result: Shop) => {
                this.shop = result;
                this.shop.showCoverPhoto = result.coverPhoto;
                this.loading = false;
                this.notify.info('保存成功', '');
            });
    }

    //图片上传返回
    handleChange(info: { file: UploadFile }): void {
        if (info.file.status === 'error') {
            this.notify.error('上传图片异常，请重试');
        }
        if (info.file.status === 'done') {
            this.shop.coverPhoto = info.file.response.result.data;
            this.notify.success('上传图片完成');
        }
    }

    return() {
        this.router.navigate(['/app/mall/shop']);
    }
}
