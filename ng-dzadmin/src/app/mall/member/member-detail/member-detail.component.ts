import { Component, Injector, OnInit } from '@angular/core';
import { WechatUser, IntegralDetail, Order } from 'entities';
import { ActivatedRoute, Router } from '@angular/router';
import { WechatUserService, IntegralDetailService, OrderService } from 'services';
import { PagedResultDto } from '@shared/component-base';

@Component({
    selector: 'member-detail',
    templateUrl: 'member-detail.component.html'
})
export class MemberDetailComponent implements OnInit {
    id: string;
    user: WechatUser = new WechatUser();
    integralList: IntegralDetail[] = [];
    orderList: Order[] = [];
    search: any = {};
    loadingI = false;
    loadingO = false;
    queryO: any = {
        pageIndexO: 1,
        pageSizeO: 10,
        skipCountO: function () { return (this.pageIndexO - 1) * this.pageSizeO; },
        totalO: 0
    };
    queryI: any = {
        pageIndexI: 1,
        pageSizeI: 10,
        skipCountI: function () { return (this.pageIndexI - 1) * this.pageSizeI; },
        totalI: 0
    };
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private orderService: OrderService
        , private integralService: IntegralDetailService
        , private wechatService: WechatUserService) {
        this.id = this.actRouter.snapshot.params['id'];
    }
    ngOnInit(): void {
        if (this.id) {
            this.getWechatUser();
            this.getIntegralList();
        }
    }

    getWechatUser() {
        let params: any = {};
        params.id = this.id;
        this.wechatService.getWechatUserById(params).subscribe((result) => {
            this.user = result;
            this.getOrderList();
        });
    }

    getIntegralList(search?: boolean) {
        if (search) {
            this.queryI.pageIndexI = 1;
        }
        this.loadingI = true;
        let params: any = {};
        params.SkipCount = this.queryI.skipCountI();
        params.MaxResultCount = this.queryI.pageSizeI;
        params.Filter = this.search.filter;
        this.integralService.getIntegralDetailById(params).subscribe((result: PagedResultDto) => {
            this.loadingI = false;
            this.integralList = result.items;
            this.queryI.totalI = result.totalCount;
        });
    }

    getOrderList(search?: boolean) {
        if (search) {
            this.queryO.pageIndexO = 1;
        }
        this.loadingO = true;
        let params: any = {};
        params.SkipCount = this.queryO.skipCountO();
        params.MaxResultCount = this.queryO.pageSizeO;
        if (this.user.openId) {
            params.OpenId = this.user.openId;
            this.orderService.getOrderById(params).subscribe((result: PagedResultDto) => {
                this.loadingO = false;
                this.orderList = result.items;
                this.queryO.total = result.totalCount;
            });
        }
    }

    return() {
        this.router.navigate(['/app/mall//member']);
    }
}
