import { NgModule } from '@angular/core';
import { ScanExchangeComponent } from './scan-exchange.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { BindShopComponent } from './bind-shop/bind-shop.component';
import { WechatUserService, ExchangeService } from '../../../services';
import { ExchangeSuccessComponent } from './exchange-success/exchange-success.component';

const COMPONENTS = [
    ScanExchangeComponent,
    BindShopComponent,
    ExchangeSuccessComponent
];

const routes: Routes = [
    { path: 'exchange', component: ScanExchangeComponent },
    { path: 'bind-shop', component: BindShopComponent },
    { path: 'exchange-success', component: ExchangeSuccessComponent },
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
