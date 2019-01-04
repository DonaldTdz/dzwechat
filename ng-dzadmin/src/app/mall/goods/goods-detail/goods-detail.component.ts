import { Component, OnInit } from '@angular/core';
import { Goods, SelectGroup } from 'entities';
import { GoodsService } from 'services';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NzModalRef, NzModalService, NzMessageService, UploadFile } from 'ng-zorro-antd';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'goods-detail',
    templateUrl: 'goods-detail.component.html'
})
export class GoodsDetailComponent implements OnInit {
    id: string;
    loading = false;
    goods: Goods = new Goods();

    cardTitle: string;
    exchangeCodes: string = '';
    splitCodes: string[];
    isOnline: boolean;
    isActionTypes: any[] = [{ text: '上架', value: true }, { text: '下架', value: false }];
    categoryTypes: SelectGroup[] = [];
    photoList = [];//已存图片
    exchangeTypes = [
        { label: '线下兑换', value: '1', checked: false },
        { label: '邮寄兑换', value: '2', checked: false },
    ];
    confirmModal: NzModalRef;
    actionUrl = '';
    fileList = [];
    previewImage = '';
    previewVisible = false;

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
            this.goods.isAction = true;
            this.goods.categoryId = 1;
        }
    }

    save(isOnline?: boolean) {
        // this.loading = true;
        if (!isOnline) {
            this.goods.isAction = false;
        } else {
            this.goods.isAction = isOnline;
        }
        var filter = this.exchangeTypes.filter(v => v.checked == true);
        this.goods.exchangeCode = filter.map(v => {
            return v.value;
        }).join(',');
        this.goods.photoUrl = this.photoList.map(v => {
            // return v.url || v.response.result.data;
            return v.url;
        }).join(',');
        if (this.goods.photoUrl == '') {
            this.goods.photoUrl = null;
        }
        if (this.goods.exchangeCode == '') {
            this.goods.exchangeCode = null;
        }
        // console.log(this.fileList);
        // console.log(this.photoList);

        // console.log(this.goods.photoUrl);

        this.goodsService.updateGoods(this.goods).finally(() => { this.loading = false; })
            .subscribe((result: Goods) => {
                this.goods = result;
                if (result.isAction) {
                    this.isOnline = true;
                } else {
                    this.isOnline = false;
                }

                this.notify.info('保存成功', '');
            });
    }

    online() {
        this.confirmModal = this.modal.confirm({
            nzContent: '是否上架此商品',
            nzOnOk: () => {
                this.save(true);
            }
        });
    }

    offline(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: '是否下架此商品?',
            nzOnOk: () => {
                this.save(false);
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

    handleRemove = (file: UploadFile) => {
        if (file) {
            let i = 0;
            this.fileList.forEach(v => {
                if (v.status == 'removed') {
                    this.fileList.splice(i, 1);
                    this.photoList[i].status = 'removed';
                    // return;
                    this.removeSavePoto();
                }
                i++
            });
        }
    }

    removeSavePoto() {
        let i = 0;
        this.photoList.forEach(v => {
            if (v.status == 'removed') {
                this.photoList.splice(i, 1);
                return;
            }
            i++
        });
    }

    //图片上传返回
    handleChange(info: { file: UploadFile }): void {
        if (info.file.status === 'error') {
            this.notify.error('上传图片异常，请重试');
        }
        if (info.file.status === 'done') {
            this.photoList.push({ url: info.file.response.result.data, status: 'done' });
            this.notify.success('上传图片完成');
        }
    }
}
