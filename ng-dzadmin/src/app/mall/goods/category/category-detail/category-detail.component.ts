import { Component, OnInit, Input, Injector, Output, EventEmitter } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Category } from 'entities';
import { GoodsService } from 'services';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';

@Component({
    selector: 'category-detail',
    templateUrl: 'category-detail.component.html'
})
export class CategoryDetailComponent {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    loading = false;
    isVisible = false;
    category: Category = new Category();
    title: string;
    constructor(private goodsService: GoodsService
        , private notify: NotifyService

    ) {
    }

    show(id?: number) {
        this.isVisible = true;
        if (id) {
            this.title = '修改分类';
            let params: any = {};
            params.Id = id;
            this.goodsService.getCategoryById(params)
                .subscribe((result) => {
                    this.category = result;
                });
        } else {
            this.category.name = null;
            this.category.seq = null;
            this.category.id = null;
            this.title = '新增分类';
        }
    }

    handleCancel = (e) => {
        this.isVisible = false;

    }

    save(): void {
        this.loading = true;
        this.goodsService.updateCategory(this.category).subscribe((result: Category) => {
            this.loading = false;
            this.notify.info('保存成功', '');
            this.modalSelect.emit();
            this.isVisible = false;
        });
    }
}