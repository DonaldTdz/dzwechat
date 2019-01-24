import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { DropDown, Shop, Exchange, ExchangeSummary, SelectGroup } from 'entities';
import { ShopService, ExchangeService } from 'services';
import { Router } from '@angular/router';
import { areAllEquivalent } from '@angular/compiler/src/output/output_ast';
import { AppConsts } from '@shared/AppConsts';

@Component({
    moduleId: module.id,
    selector: 'exchange',
    templateUrl: 'exchange.component.html',
    styleUrls: ['exchange.component.scss']
})
export class ExchangeComponent extends PagedListingComponentBase<any> {
    queryAll: any = {
        pageIndexAll: 1,
        pageSizeAll: 10,
        skipCountAll: function () { return (this.pageIndexAll - 1) * this.pageSizeAll; },
        totalAll: 0
    };
    search: any = { goodsName: '', orderId: '', shopId: '0', startTime: null, endTime: null, exchangeStyle: 0 }
    summaryDate: Date[] = [];
    shopTypes = [];
    summaryFormat = 'yyyy-MM-dd';
    sumSearch: any = { startTime: null, endTime: null, keyWord: null, shopType: '0' };
    searchSummary: any = { specification: '', shopId: '0' };
    dateRange = [];
    shedateFormat = 'yyyy-MM-dd';
    shops = [];
    totalItems = 0;
    loadingAll = false;
    exchangeStyles = [
        { value: 0, text: '全部' },
        { value: 1, text: '线下兑换' },
        { value: 2, text: '邮寄兑换' },
    ];
    exportLoading = false;
    exportSummaryLoading = false;
    isInit = true;
    isReset = false;
    summaryList: ExchangeSummary[] = [];
    constructor(private injector: Injector, private exchangeService: ExchangeService, private shopService: ShopService,
        private router: Router) {
        super(injector);
    }
    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {

        this.getShopTypes();
        if (this.isInit) {
            this.shopService.getShopListForDropDown().subscribe((data => {
                this.shops.push(Shop.fromJS({ id: '0', name: '全部' }));
                data.map(i => {
                    this.shops.push(i);
                })
                //排序
                // this.shops.sort((a, b) => {
                //     if (a.id > b.id) {
                //         return 1;
                //     } else if (a.id = b.id) {
                //         return 0;
                //     } else {
                //         return -1;
                //     }
                // });
            }));
            this.isInit = false;
        }

        if (this.isReset) {
            this.pageNumber = 1;
            request.skipCount = 0;
            this.isReset = false;
        }
        this.exchangeService.getExchangeDetail(this.getParameter(request.skipCount, request.maxResultCount)).finally(() => {
            finishedCallback();
        }).subscribe((data) => {
            this.dataList = data.items;
            this.totalItems = data.totalCount;
        });
    }

    /**
     * 获取兑换明细记录
     */
    getExchangeDetail() {
        this.exchangeService.getExchangeDetail(this.search).finally(() => {
            // finishedCallback();
        }).subscribe((data) => {
            this.dataList = data.items;
            this.totalItems = data.totalCount;
        });
    }
    /**
     * 选择时间
     * @param times 
     */
    changeTime(times) {
        if (times != null) {
            this.search.startTime = this.dateFormat(this.dateRange[0]);
            this.search.endTime = this.dateFormat(this.dateRange[1]);
        }
    }
    /**
     * 重置
     */
    reset() {
        this.isReset = true;
        this.dateRange = [];//[]; 
        this.search = { goodsName: '', orderId: '', shopId: '0', startTime: null, endTime: null, exchangeStyle: 0 };
        this.refresh();
    }

    /**
     * 获取查询条件
     */
    getParameter(skipCount?, maxResultCount?): any {
        var arry: any = {};
        arry.skipCount = skipCount;
        arry.maxResultCount = maxResultCount;
        arry.goodsName = this.search.goodsName;
        arry.orderId = this.search.orderId;
        arry.shopId = this.search.shopId === '0' ? '' : this.search.shopId;
        arry.startTime = this.search.startTime;
        arry.endTime = this.search.endTime;
        arry.exchangeStyle = this.search.exchangeStyle === 0 ? null : this.search.exchangeStyle;
        return arry;
    }

    /**
     * 导出任务明细
     */
    exportExchangeDetail() {
        this.exportLoading = true;
        this.exchangeService.exportExchangeDetail(this.getParameter()).subscribe((data => {
            if (data.code == 0) {
                var url = AppConsts.remoteServiceBaseUrl + data.data;
                document.getElementById('ExchangeDetailUrl').setAttribute('href', url);
                document.getElementById('btnExchangeDetailHref').click();
            } else {
                this.notify.error(data.msg);
            }
            this.exportLoading = false;
        }));
    }

    //跳转订单详细页
    goOrderDetail(id) {
        this.router.navigate(['/app/mall/order-detail', id])
    }

    getShopTypes() {
        this.exchangeService.getShopTypeAsync().subscribe((result: SelectGroup[]) => {
            this.shopTypes.push(SelectGroup.fromJS({ value: '0', text: '全部' }));
            this.shopTypes.push(...result);
            this.getSummaryList();
        });
    }

    sumChangeTime(times) {
        if (times != null) {
            this.sumSearch.startTime = this.dateFormat(this.summaryDate[0]);
            this.sumSearch.endTime = this.dateFormat(this.summaryDate[1]);
        }
    }
    getSummaryList(search?: boolean) {
        if (search) {
            this.summaryDate = [];
            this.sumSearch.startTime = null;
            this.sumSearch.endTime = null;
            this.sumSearch.shopType = '0';
            this.sumSearch.keyWord = null;
        }
        this.loadingAll = true;
        let params: any = {};
        params.FilterText = this.sumSearch.keyWord;
        params.StartTime = this.sumSearch.startTime;
        params.EndTime = this.sumSearch.endTime;
        if (this.sumSearch.shopType != '0') {
            params.ShopType = this.sumSearch.shopType;
        }
        this.exchangeService.getExchangeSummary(params).subscribe((result: PagedResultDto) => {
            this.loadingAll = false;
            this.summaryList = result.items;
            this.queryAll.totalAll = result.totalCount;
        });
    }

    exportSummary() {
        this.exportSummaryLoading = true;
        let params: any = {};
        params.FilterText = this.sumSearch.keyWord;
        params.StartTime = this.sumSearch.startTime;
        params.EndTime = this.sumSearch.endTime;
        if (this.sumSearch.shopType != '0') {
            params.ShopType = this.sumSearch.shopType;
        }
        this.exchangeService.exportSummaryExchange(params).subscribe((data => {
            if (data.code == 0) {
                var url = AppConsts.remoteServiceBaseUrl + data.data;
                document.getElementById('ExchangeSummaryUrl').setAttribute('href', url);
                document.getElementById('btnExchangeSummaryHref').click();
            } else {
                this.notify.error(data.msg);
            }
            this.exportSummaryLoading = false;
        }));
    }
}
