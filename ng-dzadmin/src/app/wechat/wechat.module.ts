import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { WechatConfigComponent } from './config/wechat-config.component';

import { WechatRoutingModule } from './wechat-routing.module';
import { LayoutModule } from '@layout/layout.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        WechatRoutingModule,
        LayoutModule,
        SharedModule,
    ],
    declarations: [
        WechatConfigComponent
    ],
    entryComponents: [
        WechatConfigComponent
    ],
    // providers: [LocalizationService, MenuService],
})
export class WechatModule { }
