import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { VipUserService } from 'services';
import { VipUser } from 'entities';

@Component({
    selector: 'vip-detail',
    templateUrl: 'vip-detail.component.html'
})
export class VipDetailComponent extends PagedListingComponentBase<any> {
    id: string;
    user: VipUser = new VipUser();
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private vipUserService: VipUserService) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        if (this.id) {
            this.getVipUser();
            this.refresh();
        }
    }

    getVipUser() {
        let params: any = {};
        params.Id = this.id;
        this.vipUserService.getVipUserById(params).subscribe((result) => {
            this.user = result;
        });
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
        params.VipUserId = this.id;
        this.vipUserService.getVipPurchase(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            });
    }

    return() {
        this.router.navigate(['/app/mall/vip']);
    }
}
