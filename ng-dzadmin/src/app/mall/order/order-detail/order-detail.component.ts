import { Component, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router, ActivatedRoute } from '@angular/router';
import { OrderService } from 'services';
import { Order } from 'entities';
import { ExchangeDetailComponent } from './exchange-detail/exchange-detail.component';

@Component({
    selector: 'order-detail',
    templateUrl: 'order-detail.component.html'
})
export class OrderDetailComponent extends PagedListingComponentBase<any> {
    @ViewChild('createModal') createModal: ExchangeDetailComponent;
    id: string;
    order: Order = new Order();
    constructor(injector: Injector
        , private router: Router
        , private orderService: OrderService
        , private actRouter: ActivatedRoute
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        if (this.id) {
            this.getOrder();
            this.refreshData();
        }
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    refreshData() {
        // this.keyWord = null;
        this.pageNumber = 1;
        this.refresh();
    }

    getOrder() {
        let params: any = {};
        params.Id = this.id;
        this.orderService.getOrderInfoById(params).subscribe((result) => {
            this.order = result;
        });
    }
    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.OrderId = this.id;
        this.orderService.getOrderDetailById(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }

    goExchange(orderDetailId: string): void {
        this.createModal.show(orderDetailId);
    }

    return() {
        this.router.navigate(['/app/mall/order']);
    }

    getCreateData() {
        this.refresh();
        this.getOrder();
    }
}
