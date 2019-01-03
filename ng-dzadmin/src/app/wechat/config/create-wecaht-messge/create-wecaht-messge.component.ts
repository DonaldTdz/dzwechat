import { Component, OnInit, Injector, inject } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { WechatMessage } from 'entities/wechatmessage';
import { WeChatMessageService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'create-wecaht-messge',
    templateUrl: 'create-wecaht-messge.component.html',
    // styleUrls: ['create-wecaht-messge.component.scss']
})
export class CreateWecahtMessgeComponent extends ModalComponentBase implements OnInit {
    wechatMessage: WechatMessage = new WechatMessage();
    triggerTypes = [
        { value: 1, text: '关键字' },
        { value: 2, text: '点击事件' },
    ];
    msgTypes = [
        { value: 1, text: '文字消息' },
        { value: 2, text: '图文消息' }
    ]
    constructor(injector: Injector, private service: WeChatMessageService) {
        super(injector);
    }
    ngOnInit(): void {
        this.wechatMessage.triggertype = 1;
        this.wechatMessage.msgType = 1;
    }

    save() {
        this.wechatMessage.matchMode = 1;//默认是精准匹配
        this.service.update(this.wechatMessage).finally(() => {
            this.saving = false;
        }).subscribe(() => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.success();
        });
    }
}
