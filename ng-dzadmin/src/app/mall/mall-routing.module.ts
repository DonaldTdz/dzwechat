import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { LayoutDefaultComponent } from '../../layout/default/layout-default.component';
import { GoodsComponent } from './goods/goods.component';
import { MemberComponent } from './member/member.component';
import { OrderComponent } from './order/order.component';
import { ShopComponent } from './shop/shop.component';
import { VipComponent } from './vip/vip.component';

const routes: Routes = [
    {
        path: 'goods',
        component: GoodsComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'member',
        component: MemberComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'order',
        component: OrderComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'shop',
        component: ShopComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'vip',
        component: VipComponent,
        canActivate: [AppRouteGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MallRoutingModule { }
