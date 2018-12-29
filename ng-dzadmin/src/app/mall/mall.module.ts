import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { GoodsComponent } from './goods/goods.component';
import { MemberComponent } from './member/member.component';
import { OrderComponent } from './order/order.component';
import { ShopComponent } from './shop/shop.component';
import { VipComponent } from './vip/vip.component';
import { MallRoutingModule } from './mall-routing.module';
import { WechatUserService, IntegralDetailService, OrderService, GoodsService } from 'services';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { CategoryComponent } from './goods/category/category.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        MallRoutingModule,
        LayoutModule,
        SharedModule,
    ],
    declarations: [
        GoodsComponent,
        MemberComponent,
        OrderComponent,
        ShopComponent,
        VipComponent,
        MemberDetailComponent,
        CategoryComponent,
        // ProductComponent,
    ],
    entryComponents: [
        GoodsComponent,
        MemberComponent,
        OrderComponent,
        ShopComponent,
        VipComponent,
        MemberDetailComponent,
        CategoryComponent,
        // ProductComponent,
    ],
    providers: [WechatUserService, IntegralDetailService, OrderService, GoodsService]
})
export class MallModule { }
