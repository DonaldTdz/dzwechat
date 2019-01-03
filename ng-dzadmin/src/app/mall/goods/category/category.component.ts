import { Component, ViewChild, TemplateRef, Output, EventEmitter, OnInit, Injector } from '@angular/core';
import { NzTreeNode, NzFormatEmitEvent, NzTreeComponent, NzDropdownContextComponent, NzDropdownService } from 'ng-zorro-antd';
import { GoodsService } from 'services';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

@Component({
    selector: 'category',
    templateUrl: 'category.component.html',
    styleUrls: ['category.component.less']
})
export class CategoryComponent implements OnInit {
    @Output() modalSelect = new EventEmitter<any>();
    @ViewChild('treeCom') treeCom: NzTreeComponent;
    @ViewChild('createModal') createModal: CategoryDetailComponent;
    dropdown: NzDropdownContextComponent;
    activedNode: NzTreeNode;
    tempNode: string = 'root';
    rkeyNode: number;
    search: any = {};
    nodes = [];
    constructor(private goodsService: GoodsService
        , private nzDropdownService: NzDropdownService
    ) {
    }

    ngOnInit(): void {
        this.getTreeAsync();
    }
    /*商品类型*/
    openFolder(data: NzTreeNode | NzFormatEmitEvent): void {
        if (data instanceof NzTreeNode) {
            data.isExpanded = !data.isExpanded;
        } else {
            data.node.isExpanded = !data.node.isExpanded;
        }
    }

    activeNode(data: NzFormatEmitEvent): void {
        if (this.activedNode) {
            this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);
        }
        data.node.isSelected = true;
        this.activedNode = data.node;
        this.tempNode = data.node.key;
        this.search = {};
        this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);
        // this.refreshData(data.node.key);
        this.modalSelect.emit(data.node.key);
    }

    contextMenu($event: MouseEvent, template: TemplateRef<void>, node: any): void {
        if (node.key != 'root') {
            this.dropdown = this.nzDropdownService.create($event, template);
            this.rkeyNode = node.key;
        }
    }

    goDetail(): void {
        if (this.dropdown) {
            this.dropdown.close();
        }
        this.createModal.show(this.rkeyNode);
    }

    goCreate(): void {
        this.createModal.show();
    }

    getCreateData() {
        this.getTreeAsync();
    }

    selectDropdown(type: string): void {
        this.dropdown.close();
    }

    getTreeAsync() {
        let params: any = {};
        this.goodsService.getCategoryTrees(params).subscribe(res => {
            this.nodes = res;
            this.modalSelect.emit('root');
        });
    }
}
