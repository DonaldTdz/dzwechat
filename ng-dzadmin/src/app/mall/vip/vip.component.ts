import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { VipUserService } from 'services';

@Component({
    selector: 'app-vip',
    templateUrl: './vip.component.html'
})
export class VipComponent extends PagedListingComponentBase<any> {
    keyWord: string;
    constructor(injector: Injector
        , private router: Router
        , private vipUserService: VipUserService) {
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
        this.vipUserService.getAll(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }


    goDetail(id: string) {
        this.router.navigate(['/app/mall/vip-detail', id]);
    }

    goUserInfo(id: string) {
        this.router.navigate(['/app/mall/member-detail', id]);
    }
}
