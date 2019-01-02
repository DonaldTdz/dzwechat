import { Component, Injector, ViewChild, OnInit } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { Goods } from 'entities';
import { Router } from '@angular/router';
import { GoodsService } from 'services';

@Component({
    selector: 'app-goods',
    templateUrl: './goods.component.html'
})
export class GoodsComponent extends PagedListingComponentBase<Goods>{
    categoryName: string;
    keyWord: string;
    constructor(injector: Injector
        , private router: Router
        , private goodService: GoodsService) {
        super(injector);
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    refreshData() {
        // this.keyWord = null;
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
        // params.Filter = this.search.filter;
        this.goodService.getAll(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string) {
        this.router.navigate(['/app/mall/member-detail', id]);
    }
}
