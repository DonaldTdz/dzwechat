import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { routes } from './wechat.route';
import { HomeComponent } from './index.component';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forRoot(routes, { useHash: true })
    ],
    declarations: [
        HomeComponent
    ],
    providers: [
    ],
    entryComponents: [
    ],
    exports: [
        RouterModule
    ]
})
export class WechatModule {
}
