import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { OrderService } from 'services';

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html'
})
export class OrderComponent extends PagedListingComponentBase<any> {
    keyWord: string;
    serchType: number = 0;
    isUnMailing: boolean = false;
    statusTypes = [
        { value: 0, text: '全部' },
        // { value: 1, text: '待支付' },
        { value: 2, text: '已支付' },
        { value: 3, text: '已完成' },
        { value: 4, text: '已取消' }
    ];
    constructor(injector: Injector
        , private router: Router
        , private orderService: OrderService
    ) {
        super(injector);
    }
    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    refreshData() {
        this.pageNumber = 1;
        this.refresh();
    }

    resetSearch() {
        this.pageNumber = 1;
        this.keyWord = null;
        this.isUnMailing = false;
        this.serchType = 0;
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
        params.FilterText = this.keyWord;
        params.Status = this.serchType;
        params.IsUnMailing = this.isUnMailing;
        this.orderService.getAll(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string) {
        this.router.navigate(['/app/mall/order-detail', id]);
    }
}
