import { Component, OnInit, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { WeChatMessageService } from 'services';
import { WechatMessage } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'edit-wechat-message',
    templateUrl: 'edit-wechat-message.component.html',
})
export class EditWechatMessageComponent extends ModalComponentBase implements OnInit {
    @Input() id: number;
    wechatMessageE: WechatMessage = new WechatMessage();
    triggerTypes = [
        { value: 1, text: '关键字' },
        { value: 2, text: '点击事件' },
    ];
    msgTypes = [
        { value: 1, text: '文字消息' },
        { value: 2, text: '图文消息' }
    ];

    constructor(injector: Injector, private service: WeChatMessageService) {
        super(injector);
    }

    ngOnInit(): void {
        this.fetchData();
    }

    //获取单条关注消息
    fetchData(): void {
        this.service.getMessageById(this.id.toString()).subscribe((result) => {
            this.wechatMessageE = result;
            console.log(result);
        });
    }

    //保存
    save() {
        //当为文字消息时清除图文消息的相关信息
        if (this.wechatMessageE.msgType == 1) {
            this.wechatMessageE.title = '';
            this.wechatMessageE.desc = '';
            this.wechatMessageE.picLink = '';
            this.wechatMessageE.url = '';
            //当为图文消息时清除文字消息的相关信息
        } else if (this.wechatMessageE.msgType == 2) {
            this.wechatMessageE.content = '';
        }
        this.service.update(this.wechatMessageE).finally(() => {
            this.saving = false;
        }).subscribe(() => {
            this.notify.success(this.l('SavedSuccessfully'));
            this.success();
        });
    }
}
