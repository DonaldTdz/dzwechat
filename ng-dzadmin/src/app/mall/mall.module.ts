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
import { WechatUserService, IntegralDetailService, OrderService, GoodsService, ShopService, ExchangeService, VipUserService } from 'services';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { CategoryComponent } from './goods/category/category.component';
import { CategoryDetailComponent } from './goods/category/category-detail/category-detail.component';
import { GoodsDetailComponent } from './goods/goods-detail/goods-detail.component';
import { OrderDetailComponent } from './order/order-detail/order-detail.component';
import { ExchangeDetailComponent } from './order/order-detail/exchange-detail/exchange-detail.component';
import { ShopDetailComponent } from './shop/shop-detail/shop-detail.component';
import { ImageCropperModule } from 'ngx-img-cropper';
import { CropperComponent } from './goods/goods-detail/cropper/cropper.component';
import { BannerCropperComponent } from './goods/goods-detail/banner-cropper/banner-cropper.component';
import { ExchangeComponent } from '@app/mall/exchange/exchange.component';
import { VipDetailComponent } from './vip/vip-detail/vip-detail.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        MallRoutingModule,
        LayoutModule,
        SharedModule,
        ImageCropperModule,
    ],
    declarations: [
        GoodsComponent,
        MemberComponent,
        OrderComponent,
        ShopComponent,
        VipComponent,
        MemberDetailComponent,
        CategoryComponent,
        CategoryDetailComponent,
        GoodsDetailComponent,
        OrderDetailComponent,
        ExchangeDetailComponent,
        ShopDetailComponent,
        // ImageCropperComponent,
        CropperComponent,
        BannerCropperComponent,
        ExchangeComponent,
        VipDetailComponent,
    ],
    entryComponents: [
        GoodsComponent,
        MemberComponent,
        OrderComponent,
        ShopComponent,
        VipComponent,
        MemberDetailComponent,
        CategoryComponent,
        CategoryDetailComponent,
        GoodsDetailComponent,
        OrderDetailComponent,
        ExchangeDetailComponent,
        ShopDetailComponent,
        // ImageCropperComponent,
        // ImageCropperModule,
        CropperComponent,
        BannerCropperComponent,
        ExchangeComponent,
        VipDetailComponent,
    ],
    providers: [WechatUserService, IntegralDetailService, OrderService, GoodsService, ShopService, ExchangeService, VipUserService]
})
export class MallModule { }
