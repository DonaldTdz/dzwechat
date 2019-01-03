import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { WechatConfigComponent } from './config/wechat-config.component';

import { WechatRoutingModule } from './wechat-routing.module';
import { LayoutModule } from '@layout/layout.module';
import { WeChatMessageService } from 'services';
import { CreateWecahtMessgeComponent } from '@app/wechat/config/create-wecaht-messge/create-wecaht-messge.component';

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
        WechatConfigComponent,
        CreateWecahtMessgeComponent,
    ],
    entryComponents: [
        WechatConfigComponent,
        CreateWecahtMessgeComponent,
    ],
    providers: [WeChatMessageService],
})
export class WechatModule { }
