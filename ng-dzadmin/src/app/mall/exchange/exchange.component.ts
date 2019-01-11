import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/component-base';
import { DropDown, Shop, Exchange } from 'entities';
import { ShopService, ExchangeService } from 'services';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'exchange',
    templateUrl: 'exchange.component.html',
    styleUrls: ['exchange.component.scss']
})
export class ExchangeComponent extends PagedListingComponentBase<any> {

    search: any = { goodsName: '', orderId: '', shopId: '0', startTime: null, endTime: null, exchangeStyle: 0 };
    dateRange = [];
    shedateFormat = 'yyyy-MM-dd';
    shops = [];
    totalItems = 0;
    excahngeStyles = [
        { value: 0, text: '全部' },
        { value: 1, text: '线下兑换' },
        { value: 2, text: '邮寄兑换' },
    ]
    constructor(private injector: Injector, private exchangeService: ExchangeService, private shopService: ShopService,
        private router: Router) {
        super(injector);
    }
    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        this.shopService.getShopListForDropDown().subscribe((data => {
            this.shops = data;
            this.shops.push({ id: '0', name: '全部' });
            this.shops.sort(i => {
                return i.id
            })
        }));

        this.search.skipCount = request.skipCount;
        this.search.maxResultCount = request.maxResultCount;
        this.search.shopId = this.search.shopId === '0' ? '' : this.search.shopId;
        this.search.exchangeStyle = this.search.exchangeStyle === 0 ? null : this.search.exchangeStyle;
        this.exchangeService.getExchangeDetail(this.getParameter()).finally(() => {
            finishedCallback();
        }).subscribe((data) => {
            console.log(data);
            this.dataList = data.items;
            this.totalItems = data.totalCount;
        });
    }

    getExchangeDetail() {
        this.exchangeService.getExchangeDetail(this.search).finally(() => {
            // finishedCallback();
        }).subscribe((data) => {
            this.dataList = data.items;
            this.totalItems = data.totalCount;
        });
    }
    changeTime(times) {
        if (times != null) {
            this.search.startTime = this.dateFormat(this.dateRange[0]);
            this.search.endtime = this.dateFormat(this.dateRange[1]);
        }
    }
    reset() {
        this.search = { goodsName: '', orderId: '', shop: '0', startTime: null, endTime: null, exchangeStyle: 0 };
        this.refresh();
    }

    getParameter(): any {
        var arry: any = {};
        // arry.skipCount = request.skipCount;
        // arry.maxResultCount = request.maxResultCount;
        arry.goodsName = this.search.goodsName;
        arry.orderId = this.search.orderId;
        arry.shopId = this.search.shopId === '0' ? '' : this.search.shopId;
        arry.startTime = this.search.startTime;
        arry.endTime = this.search.endTime;
        arry.exchangeStyle = this.search.excahngeStyles;

        return arry;
    }

    //跳转订单详细页
    goOrderDetail(id) {
        this.router.navigate(['/app/mall/order-detail', id])
    }
}
