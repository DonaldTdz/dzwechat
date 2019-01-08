import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { ShopService } from 'services';

@Component({
    selector: 'app-shop',
    templateUrl: './shop.component.html'
})
export class ShopComponent extends PagedListingComponentBase<any> {
    keyWord: string;
    constructor(injector: Injector
        , private router: Router
        , private shopService: ShopService
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
        this.shopService.getAll(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }

    goCreate() {
        this.router.navigate(['/app/mall/shop-detail']);
    }

    goDetail(id: string) {
        this.router.navigate(['/app/mall/shop-detail', id]);
    }
}
