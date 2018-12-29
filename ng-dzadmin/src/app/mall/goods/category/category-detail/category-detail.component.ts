import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Category } from 'entities';
import { GoodsService } from 'services';

@Component({
    selector: 'category-detail',
    templateUrl: 'category-detail.component.html'
})
export class CategoryDetailComponent extends ModalComponentBase implements OnInit {
    @Input() id: number;
    category: Category = new Category();
    title: string;
    constructor(
        injector: Injector
        , private goodsService: GoodsService

    ) {
        super(injector);
    }

    ngOnInit() {
        this.fetchData();
    }

    fetchData(): void {
        let params: any = {};
        params.id = this.id;
        this.goodsService.getCategoryById(params)
            .subscribe((result) => {
                this.category = result;
            });
    }


    save(): void {
        //     let tmpRoleNames = [];
        //     this.roleList.forEach((item) => {
        //       if (item.checked) {
        //         tmpRoleNames.push(item.value);
        //       }
        //     });
        //     this.category.roleNames = tmpRoleNames;
        //     this.category.surname = this.category.name;

        //     this.goodsService.update(this.category)
        //       .finally(() => { this.saving = false; })
        //       .subscribe(() => {
        //         this.notify.info(this.l('SavedSuccessfully'));
        //         this.success();
        //       });
        //   }
    }
}