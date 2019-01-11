import { NgModule } from '@angular/core';
import { ScanExchangeComponent } from './scan-exchange.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { BindShopComponent } from './bind-shop/bind-shop.component';
import { WechatUserService, ExchangeService } from '../../../services';

const COMPONENTS = [
    ScanExchangeComponent,
    BindShopComponent,
];

const routes: Routes = [
    { path: 'exchange', component: ScanExchangeComponent },
    { path: 'bind-shop', component: BindShopComponent },
];
@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        ...COMPONENTS
    ],
    providers: [
        WechatUserService
        , ExchangeService
    ]
})
export class ScanExchangeModule {

}
