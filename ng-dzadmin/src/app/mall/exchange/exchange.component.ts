import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/component-base';
import { DropDown } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'exchange',
    templateUrl: 'exchange.component.html',
    styleUrls: ['exchange.component.scss']
})
export class ExchangeComponent extends PagedListingComponentBase<any> {

    search: any = { goodsName: '', orderId: '', shop: 0, startTime: null, endTime: null, exchangeStyle: 0 };
    dateRange = [];
    shedateFormat = 'yyyy-MM-dd';
    shops: DropDown[] = [];
    constructor(private injector: Injector) {
        super(injector);
    }
    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {

    }

    changeTime(times) {
        if (times != null) {
            this.search.startTime = this.dateFormatHH(this.dateRange[0]);
            this.search.endtime = this.dateFormatHH(this.dateRange[1]);
        }
    }
}
