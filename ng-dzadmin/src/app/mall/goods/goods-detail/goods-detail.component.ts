import { Component, OnInit } from '@angular/core';
import { Goods, SelectGroup } from 'entities';
import { GoodsService } from 'services';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';

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
    exchangeTypes = [
        { label: '线下兑换', value: '1', checked: false },
        { label: '邮寄兑换', value: '2', checked: false },
    ];
    confirmModal: NzModalRef;
    constructor(private goodsService: GoodsService
        , private notify: NotifyService
        , private router: Router
        , private actRouter: ActivatedRoute
        , private modal: NzModalService
    ) {
        this.id = this.actRouter.snapshot.params['id'];
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
        this.loading = true;
        if (!isOnline) {
            this.goods.isAction = false;
        } else {
            this.goods.isAction = isOnline;
        }
        console.log(this.goods.isAction);
        var filter = this.exchangeTypes.filter(v => v.checked == true);
        this.goods.exchangeCode = filter.map(v => {
            return v.value;
        }).join(',');
        filter.forEach(v => this.exchangeCodes += v.value + ',');
        if (this.exchangeCodes != '' && this.exchangeCodes.length != 0) {
            this.goods.exchangeCode = this.exchangeCodes.substring(0, this.exchangeCodes.length - 1);
        }
        this.goodsService.updateGoods(this.goods).finally(() => { this.loading = false; })
            .subscribe((result: Goods) => {
                this.goods = result;
                if (result.isAction) {
                    this.isOnline = true;
                } else {
                    this.isOnline = false;
                }
                // if (result.photoUrl) {
                //     this.product.showPhoto = this.host + this.product.photoUrl;
                // }
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
}
