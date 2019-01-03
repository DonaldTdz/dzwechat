import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { WeChatMessageService } from 'services';
import { WechatMessage } from 'entities/wechatmessage';
import { CreateWecahtMessgeComponent } from '@app/wechat/config/create-wecaht-messge/create-wecaht-messge.component';

@Component({
    selector: 'app-wechat-config',
    templateUrl: './wechat-config.component.html',
    styles: [],
})
export class WechatConfigComponent extends PagedListingComponentBase<any> {
    param: any = {};
    mesText = '';

    constructor(injector: Injector, private messageService: WeChatMessageService) {
        super(injector);
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        this.param.mesText = this.mesText;
        this.messageService.getMessagePage(this.param).finally(() => {
            finishedCallback();
        }).subscribe((result: PagedResultDto) => {
            this.dataList = result.items;
            this.totalItems = result.totalCount;
        });
    }
    create() {
        this.modalHelper.open(CreateWecahtMessgeComponent, {}, 'md', {
            nzMask: true
        }).subscribe(isSave => {
            if (isSave) {
                this.refresh();
            }
        });
    }

    edit() {

    }
    delete(entity: WechatMessage) {
        this.message.confirm(
            "删除用户'" + entity.keyWord + "'?",
            '信息确认',
            (result: boolean) => {
                if (result) {
                    this.messageService.delete(entity.id).subscribe(() => {
                        this.notify.info('已删除消息：' + entity.keyWord);
                    });
                }
            }
        );
    }
}
