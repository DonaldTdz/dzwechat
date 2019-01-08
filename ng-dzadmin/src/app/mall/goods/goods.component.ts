import { Component, Injector, ViewChild, OnInit } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { Goods } from 'entities';
import { Router } from '@angular/router';
import { GoodsService } from 'services';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'app-goods',
    templateUrl: './goods.component.html'
})
export class GoodsComponent extends PagedListingComponentBase<Goods>{
    categoryName: string;
    keyWord: string;
    cNodeKey: string;
    //图片放大
    previewImage = '';
    previewVisible = false;
    constructor(injector: Injector
        , private router: Router
        , private goodService: GoodsService) {
        super(injector);
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    refreshData() {
        // this.keyWord = null;
        this.pageNumber = 1;
        this.refresh();
    }

    resetSearch() {
        this.pageNumber = 1;
        this.keyWord = null;
        this.refresh();
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.Filter = this.keyWord;
        if (!this.cNodeKey) {
            this.cNodeKey = 'root';
        }
        params.NodeKey = this.cNodeKey;
        this.goodService.getAll(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items.map((i) => {
                    if (i.photoUrl) {
                        if (i.photoUrl.indexOf(',') != -1) {
                            let firstPhoto: string = i.photoUrl.split(',')[0];
                            i.showPhotoUrl = AppConsts.remoteServiceBaseUrl + firstPhoto;
                        } else {
                            i.showPhotoUrl = AppConsts.remoteServiceBaseUrl + i.photoUrl;
                        }
                    }
                    return i;
                });
                this.totalItems = result.totalCount;
            });
    }
    goCreate() {
        this.router.navigate(['/app/mall/goods-detail']);
    }
    goDetail(id: string) {
        this.router.navigate(['/app/mall/goods-detail', id]);
    }
    //图片放大
    handlePreview = (url: string) => {
        this.previewImage = url;
        this.previewVisible = true;
    }
    getNodeKey(event) {
        this.cNodeKey = event;
        this.resetSearch();
    }
}
