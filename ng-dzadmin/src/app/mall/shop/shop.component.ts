import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/component-base';

@Component({
    selector: 'app-shop',
    templateUrl: './shop.component.html',
    styles: [],
})
export class ShopComponent extends PagedListingComponentBase<any> {
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
