import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { WechatConfigComponent } from './config/wechat-config.component';

import { WechatRoutingModule } from './wechat-routing.module';
import { LayoutModule } from '@layout/layout.module';
import { WeChatMessageService, WechatSubscribeService } from 'services';
import { CreateWecahtMessgeComponent } from '@app/wechat/config/create-wecaht-messge/create-wecaht-messge.component';
import { EditWechatMessageComponent } from '@app/wechat/config/edit-wechat-message/edit-wechat-message.component';

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
        EditWechatMessageComponent,
    ],
    entryComponents: [
        WechatConfigComponent,
        CreateWecahtMessgeComponent,
        EditWechatMessageComponent,
    ],
    providers: [WeChatMessageService, WechatSubscribeService],
})
export class WechatModule { }
