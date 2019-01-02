import { Component, ViewChild, TemplateRef, Output, EventEmitter } from '@angular/core';
import { NzTreeNode, NzFormatEmitEvent, NzTreeComponent, NzDropdownContextComponent, NzDropdownService } from 'ng-zorro-antd';

@Component({
    selector: 'category',
    templateUrl: 'category.component.html',
    styleUrls: ['category.component.less']
})
export class CategoryComponent {
    @Output() selectedCategory = new EventEmitter<any>();
    @ViewChild('treeCom') treeCom: NzTreeComponent;
    // @ViewChild('createModal') createModal: CreateTagComponent;
    // @ViewChild('editModal') editModal: EditTagComponent;
    dropdown: NzDropdownContextComponent;
    activedNode: NzTreeNode;
    tempNode: string = 'root';
    rkeyNode: number;
    search: any = {};
    nodes = [];
    constructor(
        //  private productService: ProductServiceProxy
        private nzDropdownService: NzDropdownService
    ) {
        // super(injector);
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
    }

    contextMenu($event: MouseEvent, template: TemplateRef<void>, node: any): void {
        if (node.key != 'root') {
            this.dropdown = this.nzDropdownService.create($event, template);
            this.rkeyNode = node.key;
        }
    }

    showEdit(): void {
        if (this.dropdown) {
            this.dropdown.close();
        }
        // this.editModal.show(this.rkeyNode);
    }
    showCreate(): void {
        // this.createModal.show();
    }

    getCreateData() {
        this.getTreeAsync();
    }

    selectDropdown(type: string): void {
        this.dropdown.close();
    }

    getTreeAsync() {
        // this.productService.getTagsTreeAsync().subscribe(res => {
        //     this.nodes = res;
        //     this.refreshData('root');
        // });
    }
}
