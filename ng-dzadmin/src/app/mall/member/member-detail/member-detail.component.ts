import { Component, Injector, OnInit } from '@angular/core';
import { WechatUser, IntegralDetail, Order, Delivery } from 'entities';
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
    loadingA = false;
    addressList: Delivery[] = [];
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
            this.getOrderList();
            this.getIntegralList();
            this.getAddressList();
        }
    }

    getWechatUser() {
        let params: any = {};
        params.Id = this.id;
        this.wechatService.getWechatUserById(params).subscribe((result) => {
            this.user = result;
            // if (result.address && result.address.indexOf(',') != -1) {
            //     this.addressList = result.address.split(',');
            //     console.log(this.addressList);
            // }
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
        params.UserId = this.id;
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
        params.Id = this.id;
        this.orderService.getOrderById(params).subscribe((result: PagedResultDto) => {
            this.loadingO = false;
            this.orderList = result.items;
            this.queryO.totalO = result.totalCount;
        });
    }

    getAddressList() {
        this.loadingA = true;
        let params: any = {};
        params.UserId = this.id;
        this.orderService.getAddressById(params).subscribe((result: Delivery[]) => {
            this.loadingA = false;
            this.addressList = result;
        });
    }
    goOrderDetail(id: string) {
        this.router.navigate(['/app/mall/order-detail', id]);
    }
    return() {
        this.router.navigate(['/app/mall/member']);
    }
}
