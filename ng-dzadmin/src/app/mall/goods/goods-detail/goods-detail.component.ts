import { Component, OnInit, ViewChild } from '@angular/core';
import { Goods, SelectGroup } from 'entities';
import { GoodsService } from 'services';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NzModalRef, NzModalService, NzMessageService, UploadFile } from 'ng-zorro-antd';
import { AppConsts } from '@shared/AppConsts';
import { ImageCropperComponent, CropperSettings, Bounds, CropPosition } from 'ngx-img-cropper';
import { CropperComponent } from './cropper/cropper.component';
import { log } from 'util';
import { retry } from 'rxjs/operators';

@Component({
    selector: 'goods-detail',
    templateUrl: 'goods-detail.component.html'
})
export class GoodsDetailComponent implements OnInit {
    @ViewChild('createModal') createModal: CropperComponent;
    @ViewChild('bannerModal') bannerModal: CropperComponent;
    id: string;
    loading = false;
    goods: Goods = new Goods();

    cardTitle: string;
    exchangeCodes: string = '';
    splitCodes: string[];
    isOnline: boolean;
    isActionTypes: any[] = [{ text: '上架', value: true }, { text: '下架', value: false }];
    categoryTypes: SelectGroup[] = [];
    photoList = [];//已存图片 不带域名
    exchangeTypes = [
        { label: '线下兑换', value: '1', checked: false },
        { label: '邮寄兑换', value: '2', checked: false },
    ];
    confirmModal: NzModalRef;
    actionUrl = '';
    fileList = [];
    previewImage = '';
    previewVisible = false;
    bannerImage = '';
    bannerVisible = false;
    bannerFile = [];
    bannerList = []; //banner 不带域名
    constructor(private goodsService: GoodsService
        , private notify: NotifyService
        , private router: Router
        , private actRouter: ActivatedRoute
        , private modal: NzModalService
        , private msg: NzMessageService
    ) {
        this.id = this.actRouter.snapshot.params['id'];
        this.actionUrl = AppConsts.remoteServiceBaseUrl + '/WeChatFile/MarketingInfoPosts?fileName=goods';
    }

    ngOnInit(): void {
        this.getCategory();
    }

    getCategory() {
        this.goodsService.getCategoryGroup().subscribe((result: SelectGroup[]) => {
            this.categoryTypes = result;
            this.getGoods();
        });
    }

    getGoods() {
        if (this.id) {
            this.cardTitle = "商品详情";
            let params: any = {};
            params.Id = this.id;
            this.goodsService.getGoodsById(params).subscribe((result) => {
                this.goods = result;
                if (result.isAction == true) {
                    this.isOnline = true;
                }
                if (result.photoUrl) {
                    if (result.photoUrl.indexOf(',') != -1) {
                        const temp = [];
                        var photoList = result.photoUrl.split(',');
                        photoList.forEach(v => {
                            temp.push({ url: AppConsts.remoteServiceBaseUrl + v, status: 'done' });
                            this.photoList.push({ url: v, status: 'done' });
                        });
                        this.fileList = temp;
                    } else {
                        var temp = [];
                        temp.push({ url: AppConsts.remoteServiceBaseUrl + result.photoUrl, status: 'done' });
                        this.photoList.push({ url: result.photoUrl, status: 'done' });
                        this.fileList = temp;
                    }
                }
                if (result.bannerUrl) {
                    var tempB = [];
                    tempB.push({ url: AppConsts.remoteServiceBaseUrl + result.bannerUrl, status: 'done' });
                    this.bannerList.push({ url: result.bannerUrl, status: 'done' });
                    this.bannerFile = tempB;
                }
                // console.log(this.fileList);

                if (result.exchangeCode) {
                    this.splitCodes = result.exchangeCode.split(',');
                    let i: number = 0;
                    this.exchangeTypes.forEach(v => {
                        if (v.value == this.splitCodes[i]) {
                            v.checked = true;
                            if (i < this.splitCodes.length) {
                                i++;
                            }
                        }
                    }
                    );
                }
            });
        } else {
            this.cardTitle = "新建商品";
            this.goods.isAction = false;
            this.goods.categoryId = 1;
            this.goods.isBanner = false;
        }
    }
    save(isAction?: boolean) {
        if (isAction) {
            this.goods.isAction = true;
        }
        // save(isOnline: boolean) {
        this.loading = true;
        // if (!isOnline) {
        //     this.goods.isAction = false;
        // } else {
        //     this.goods.isAction = isOnline;
        // }
        var filter = this.exchangeTypes.filter(v => v.checked == true);
        this.goods.exchangeCode = filter.map(v => {
            return v.value;
        }).join(',');
        this.goods.photoUrl = this.photoList.map(v => {
            // return v.url || v.response.result.data;
            return v.url;
        }).join(',');
        this.goods.bannerUrl = this.bannerList.map(v => {
            return v.url;
        }).toString();
        if (this.goods.photoUrl == '') {
            this.goods.photoUrl = null;
        }
        if (this.goods.exchangeCode == '') {
            this.goods.exchangeCode = null;
        }
        if (this.goods.isBanner == false) {
            this.goods.bannerUrl = null;
        } else {
            if (!this.goods.bannerUrl || this.goods.bannerUrl == '') {
                this.goods.isBanner = false;
            }
        }
        if (this.goods.exchangeCode == null) {
            this.notify.warn('兑换方式不能为空', '');
            this.loading = false;
            return;
        }
        if (this.goods.photoUrl == null) {
            this.notify.warn('商品图片不能为空', '');
            this.loading = false;
            return;
        }
        // console.log(this.fileList);
        // console.log(this.photoList);
        // console.log(this.goods.photoUrl);

        this.goodsService.updateGoods(this.goods).subscribe((result: Goods) => {
            this.goods = result;
            if (result.isAction) {
                this.isOnline = true;
            } else {
                this.isOnline = false;
            }
            this.loading = false;
            this.notify.info('保存成功', '');
        });
    }

    online() {
        this.confirmModal = this.modal.confirm({
            nzContent: '是否上架此商品',
            nzOnOk: () => {
                this.loading = true;
                this.goods.isAction = true;
                this.goodsService.updateGoodsStatus(this.goods).subscribe((result: Goods) => {
                    this.goods.isAction = result.isAction;
                    this.goods.offlineTime = result.offlineTime;
                    this.goods.onlineTime = result.onlineTime;
                    if (result.isAction) {
                        this.isOnline = true;
                    } else {
                        this.isOnline = false;
                    }
                    this.loading = false;
                    this.notify.info('保存成功', '');
                });
            }
        });
    }

    offline(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: '是否下架此商品?',
            nzOnOk: () => {
                this.loading = true;
                this.goods.isAction = false;
                this.goodsService.updateGoodsStatus(this.goods).subscribe((result: Goods) => {
                    this.goods.isAction = result.isAction;
                    this.goods.offlineTime = result.offlineTime;
                    this.goods.onlineTime = result.onlineTime;
                    if (result.isAction) {
                        this.isOnline = true;
                    } else {
                        this.isOnline = false;
                    }
                    this.loading = false;
                    this.notify.info('保存成功', '');
                });
            }
        });
    }

    return() {
        this.router.navigate(['/app/mall/goods']);
    }

    handlePreview = (file: UploadFile) => {
        this.previewImage = file.url || file.thumbUrl;
        this.previewVisible = true;
    }

    bannerPreview = (file: UploadFile) => {
        this.bannerImage = file.url || file.thumbUrl;
        this.bannerVisible = true;
    }

    handleRemove = (file: UploadFile) => {
        this.confirmModal = this.modal.confirm({
            nzContent: '是否删除当前图片?',
            nzOnOk: () => {
                if (file) {
                    let tflist = JSON.parse(JSON.stringify(this.fileList));
                    for (const key in tflist) {
                        if (tflist[key].status == 'removed') {
                            tflist.splice(parseInt(key), 1);
                            this.photoList.splice(parseInt(key), 1);
                            break;
                        }
                    }
                    this.fileList = tflist;
                    /*let i = 0;
                    this.fileList.forEach((v) => {
                        if (v.status == 'removed') {
                            // const temp = v;
                            // const list = this.fileList;
                            this.fileList.splice(i, 1);
                            this.photoList.splice(i, 1);
                            return;
                            // this.photoList[i].status = 'removed';
                            // this.removeSavePoto();
                        }
                        i++
                    });*/
                }
            }
        });
    }

    // removeSavePoto() {
    //     let i = 0;
    //     this.photoList.forEach(v => {
    //         if (v.status == 'removed') {
    //             this.photoList.splice(i, 1);
    //             return;
    //         }
    //         i++
    //     });
    // }

    bannerRemove = (file: UploadFile) => {
        this.confirmModal = this.modal.confirm({
            nzContent: '是否删除当前图片?',
            nzOnOk: () => {
                if (file) {
                    this.bannerFile = [];
                    this.bannerList = [];
                }
            }
        });
    }

    goCreate(): void {
        this.createModal.show();
    }

    goBanner(): void {
        this.bannerModal.show();
    }

    getBannerData(e) {
        let params: any = {}
        params.fileName = 'url.jpg';
        params.imageBase64 = e.image;
        this.goodsService.filesPostsBase64(params).subscribe((res) => {
            if (res && res.code == 0) {
                const temp = [];
                temp.push({ url: AppConsts.remoteServiceBaseUrl + res.data, status: 'done' });
                this.bannerFile = temp;
                this.bannerList.push({ url: res.data, status: 'done' });
                this.notify.success('上传图片完成');
            } else {
                console.error(res);
            }
        });
    }
    getCropperData(e) {
        let params: any = {}
        params.fileName = 'url.jpg';
        params.imageBase64 = e.image;
        this.goodsService.filesPostsBase64(params).subscribe((res) => {
            if (res && res.code == 0) {
                let temp = [];
                this.fileList.forEach(
                    v => {
                        temp.push({ url: v.url, status: 'done' });
                    }
                );
                temp.push({ url: AppConsts.remoteServiceBaseUrl + res.data, status: 'done' });
                this.fileList = temp;
                this.photoList.push({ url: res.data, status: 'done' });
                this.notify.success('上传图片完成');
            } else {
                console.error(res);
            }
        });
    }
}
