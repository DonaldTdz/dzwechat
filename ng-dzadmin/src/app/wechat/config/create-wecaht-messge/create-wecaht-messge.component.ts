import { Component, OnInit, Injector, inject } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { WeChatMessageService } from 'services';
import { WechatMessage } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'create-wecaht-messge',
    templateUrl: 'create-wecaht-messge.component.html',
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
        this.wechatMessage.triggerType = 1;
        this.wechatMessage.msgType = 1;
    }

    save() {
        //当为文字消息时清除图文消息的相关信息
        if (this.wechatMessage.msgType == 1) {
            this.wechatMessage.title = '';
            this.wechatMessage.desc = '';
            this.wechatMessage.picLink = '';
            this.wechatMessage.url = '';
            //当为图文消息时清除文字消息的相关信息
        } else if (this.wechatMessage.msgType == 2) {
            this.wechatMessage.content = '';
        }
        this.wechatMessage.matchMode = 1;//默认是精准匹配
        this.service.update(this.wechatMessage).finally(() => {
            this.saving = false;
        }).subscribe(() => {
            this.notify.success(this.l('SavedSuccessfully'));
            this.success();
        });
    }
}
