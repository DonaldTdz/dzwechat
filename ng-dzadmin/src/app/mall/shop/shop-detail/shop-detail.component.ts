import { Component, Injector } from '@angular/core';
import { Shop } from 'entities';
import { ShopService, ExchangeService } from 'services';
import { Router, ActivatedRoute } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { UploadFile } from 'ng-zorro-antd';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';

@Component({
    selector: 'shop-detail',
    templateUrl: 'shop-detail.component.html',
    styleUrls: ['shop-detail.component.less']
})
export class ShopDetailComponent extends PagedListingComponentBase<any> {
    id: string;
    loading = false;
    shop: Shop = new Shop();
    actionUrl = '';
    cardTitle: string;
    exchangeCodes: string = '';
    splitCodes: string[];
    isOnline: boolean;
    keyWord: string;
    shopTypes: any[] = [{ text: '直营店', value: 1 }];
    dateRange: Date[] = [];
    sumSearch: any = { startTime: null, endTime: null };
    shedateFormat = 'yyyy-MM-dd';

    constructor(private injector: Injector
        , private shopService: ShopService
        , private exchangeService: ExchangeService
        // , private notify: NotifyService
        , private router: Router
        , private actRouter: ActivatedRoute
        // , private modal: NzModalService
        // , private msg: NzMessageService
    ) {
        super(injector);
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
                if (this.shop.id) {
                    this.resetSearch();
                }
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

    refreshData() {
        this.pageNumber = 1;
        this.refresh();
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    changeTime(times) {
        if (times != null) {
            this.sumSearch.startTime = this.dateFormat(this.dateRange[0]);
            this.sumSearch.endTime = this.dateFormat(this.dateRange[1]);
        }
    }

    resetSearch() {
        this.pageNumber = 1;
        this.keyWord = null;
        this.dateRange = [];
        this.sumSearch.startTime = null;
        this.sumSearch.endTime = null;
        this.refresh();
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.ShopId = this.shop.id;
        params.FilterText = this.keyWord;
        params.StartTime = this.sumSearch.startTime;
        params.EndTime = this.sumSearch.endTime;

        this.exchangeService.getExchangeByShopId(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }

    return() {
        this.router.navigate(['/app/mall/shop']);
    }

    goOrderDetail(id: string) {
        this.router.navigate(['/app/mall/order-detail', id]);
    }
}
