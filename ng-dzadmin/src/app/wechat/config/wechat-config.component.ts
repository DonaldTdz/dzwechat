import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { WeChatMessageService } from 'services';
import { WechatMessage } from 'entities/wechatmessage';
import { CreateWecahtMessgeComponent } from '@app/wechat/config/create-wecaht-messge/create-wecaht-messge.component';
import { EditWechatMessageComponent } from '@app/wechat/config/edit-wechat-message/edit-wechat-message.component';
import { WechatSubscribeService } from 'services/wechat-subscribe/wechat-subscribe.service';
import { WechatSubscribe } from 'entities';

@Component({
    selector: 'app-wechat-config',
    templateUrl: './wechat-config.component.html',
    styles: [],
})
export class WechatConfigComponent extends PagedListingComponentBase<any> {
    param: any = { triType: 0, msType: 0 };
    mesText = '';
    triggerTypesMe = [
        { value: 0, text: '全部' },
        { value: 1, text: '关键字' },
        { value: 2, text: '点击事件' },
    ];
    msgTypesMe = [
        { value: 0, text: '全部' },
        { value: 1, text: '文字消息' },
        { value: 2, text: '图文消息' }
    ];
    //图文消息
    wechatSubscribe: WechatSubscribe = new WechatSubscribe();
    isConfirmLoadingDe = false;
    isConfirmLoadingSa = false;
    isDelete = false;
    msgTypes = [
        { value: 1, text: '文字消息' },
        { value: 2, text: '图文消息' }
    ];
    constructor(injector: Injector, private messageService: WeChatMessageService, private subscribeService: WechatSubscribeService) {
        super(injector);
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        //图文消息
        this.getSubScribe();
        //关注回复
        this.messageService.getMessagePage(this.getParameter()).finally(() => {
            finishedCallback();
        }).subscribe((result: PagedResultDto) => {
            this.dataList = result.items;
            this.totalItems = result.totalCount;
        });
    }
    //#region 消息回复
    create() {
        this.modalHelper.open(CreateWecahtMessgeComponent, {}, 'md', {
            nzMask: true
        }).subscribe(isSave => {
            if (isSave) {
                this.refresh();
            }
        });
    }

    edit(item: WechatMessage): void {
        this.modalHelper.open(EditWechatMessageComponent, { id: item.id }, 'md', {
            nzMask: true
        }).subscribe(isSave => {
            if (isSave) {
                this.refresh();
            }
        });
    }
    delete(entity: WechatMessage) {
        this.message.confirm(
            "删除回复'" + entity.keyWord + "'?",
            '信息确认',
            (result: boolean) => {
                if (result) {
                    this.messageService.delete(entity.id).subscribe(() => {
                        this.notify.success('删除成功！');
                        this.refresh();
                    });
                }
            }
        );
    }
    getParameter(): any {
        var arry: any = {};
        arry.MesText = this.param.mesText;
        arry.TriggerType = this.param.triType == 0 ? null : this.param.triType;
        arry.MsgType = this.param.msType === 0 ? null : this.param.msType;
        return arry;
    }
    //#endregion

    //#region  图文消息

    getSubScribe() {
        this.subscribeService.getSubscribe().subscribe((data) => {
            this.wechatSubscribe = data;
            if (!this.wechatSubscribe.id) {
                this.wechatSubscribe.msgType = 1;
            } else {
                this.isDelete = true;
            }
        });
    }

    saveSub() {
        //当为文字消息时清除图文消息的相关信息
        if (this.wechatSubscribe.msgType == 1) {
            this.wechatSubscribe.title = '';
            this.wechatSubscribe.desc = '';
            this.wechatSubscribe.picLink = '';
            this.wechatSubscribe.url = '';
            //当为图文消息时清除文字消息的相关信息
        } else if (this.wechatSubscribe.msgType == 2) {
            this.wechatSubscribe.content = '';
        }
        this.isConfirmLoadingSa = true;
        this.subscribeService.update(this.wechatSubscribe).finally(() => {
            this.saving = false; this.isConfirmLoadingSa = false;
        }).subscribe(() => {
            this.notify.success(this.l('SavedSuccessfully'));
            this.getSubScribe();
        })
    }

    deleteSub() {
        this.message.confirm(
            "删除图文消息?",
            '信息确认',
            (result: boolean) => {
                if (result) {
                    this.subscribeService.delete(this.wechatSubscribe.id).subscribe(() => {
                        this.notify.success('删除成功！');
                        this.getSubScribe();
                        this.isDelete = false;
                    });
                }
            }
        )
    }

    //#endregion
}
