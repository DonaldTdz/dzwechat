import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/component-base';

@Component({
    selector: 'app-wechat-config',
    templateUrl: './wechat-config.component.html',
    styles: [],
})
export class WechatConfigComponent extends PagedListingComponentBase<any> {
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
