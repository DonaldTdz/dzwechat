import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { WechatUser } from 'entities';
import { WechatUserService } from 'services';
import { Router } from '@angular/router';

@Component({
    selector: 'app-member',
    templateUrl: './member.component.html'
})
export class MemberComponent extends PagedListingComponentBase<WechatUser> {
    keyWord: string;
    serchType: number = 0;
    statusTypes = [
        { value: 0, text: '全部' },
        { value: 1, text: '普通会员' },
        { value: 2, text: 'Vip会员' }
    ];
    constructor(injector: Injector
        , private router: Router
        , private wechatService: WechatUserService) {
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
        this.serchType = 0;
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
        params.Status = this.serchType;
        this.wechatService.getAll(params)
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
