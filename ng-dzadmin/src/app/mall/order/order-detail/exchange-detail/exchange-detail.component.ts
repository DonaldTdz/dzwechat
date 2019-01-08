import { Component, EventEmitter, Output } from '@angular/core';
import { Exchange, Category } from 'entities';
import { OrderService } from 'services';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';

@Component({
    selector: 'exchange-detail',
    templateUrl: 'exchange-detail.component.html'
})
export class ExchangeDetailComponent {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    loading = false;
    isVisible = false;
    exchange: Exchange = new Exchange();
    title: string = '邮寄信息';
    orderDetailId: string;
    constructor(private orderService: OrderService
        , private notify: NotifyService

    ) {
    }

    show(id: string) {
        this.isVisible = true;
        if (id) {
            this.orderDetailId = id;
            let params: any = {};
            params.Id = id;
            this.orderService.getExchangeByOrderDetailId(params)
                .subscribe((result) => {
                    this.exchange = result;
                });
        }
    }

    handleCancel = (e) => {
        this.isVisible = false;
    }

    save(): void {
        this.loading = true;
        this.exchange.orderDetailId = this.orderDetailId;
        this.exchange.exchangeCode = 2;
        this.orderService.createExchange(this.exchange).subscribe((result: Exchange) => {
            this.loading = false;
            this.notify.info('保存成功', '');
            this.modalSelect.emit();
            this.isVisible = false;
        });
    }
}
