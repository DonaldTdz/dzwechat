import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/component-base';

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html',
    styles: [],
})
export class OrderComponent extends PagedListingComponentBase<any> {
    constructor(injector: Injector) {
        super(injector);
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {

    }
}
