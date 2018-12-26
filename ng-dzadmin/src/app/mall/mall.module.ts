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
        VipComponent
    ],
    entryComponents: [
        GoodsComponent,
        MemberComponent,
        OrderComponent,
        ShopComponent,
        VipComponent
    ],
    // providers: [LocalizationService, MenuService],
})
export class MallModule { }
