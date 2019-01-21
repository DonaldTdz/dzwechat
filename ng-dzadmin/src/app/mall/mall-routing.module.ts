import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { LayoutDefaultComponent } from '../../layout/default/layout-default.component';
import { GoodsComponent } from './goods/goods.component';
import { MemberComponent } from './member/member.component';
import { OrderComponent } from './order/order.component';
import { ShopComponent } from './shop/shop.component';
import { VipComponent } from './vip/vip.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { GoodsDetailComponent } from './goods/goods-detail/goods-detail.component';
import { OrderDetailComponent } from './order/order-detail/order-detail.component';
import { ShopDetailComponent } from './shop/shop-detail/shop-detail.component';
import { ExchangeComponent } from '@app/mall/exchange/exchange.component';
import { VipDetailComponent } from './vip/vip-detail/vip-detail.component';

const routes: Routes = [
    {
        path: 'goods',
        component: GoodsComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'goods-detail/:id',
        component: GoodsDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'goods-detail',
        component: GoodsDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'member',
        component: MemberComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'member-detail/:id',
        component: MemberDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'order',
        component: OrderComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'order-detail/:id',
        component: OrderDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'shop',
        component: ShopComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'shop-detail',
        component: ShopDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'shop-detail/:id',
        component: ShopDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'vip',
        component: VipComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'vip-detail/:id',
        component: VipDetailComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'exchange',
        component: ExchangeComponent,
        canActivate: [AppRouteGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MallRoutingModule { }
