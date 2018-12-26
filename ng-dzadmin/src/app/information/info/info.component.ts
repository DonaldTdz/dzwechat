import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/component-base';

@Component({
    selector: 'app-info',
    templateUrl: './info.component.html',
    styles: [],
})
export class InfoComponent extends PagedListingComponentBase<any> {
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
