import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { WechatConfigComponent } from './config/wechat-config.component';

const routes: Routes = [
    {
        path: 'config',
        component: WechatConfigComponent,
        canActivate: [AppRouteGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class WechatRoutingModule { }
