import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase, PagedResultDto } from '@shared/component-base';
import { NewsService } from 'services';
import { Router } from '@angular/router';
import { News } from 'entities';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'app-news',
    templateUrl: './news.component.html',
    styles: [],
})
export class NewsComponent extends PagedListingComponentBase<any> {
    newsType = [
        { value: 1, text: '烟语课堂', selected: true },
        { value: 2, text: '新品快讯', selected: false },
        { value: 3, text: '产品大全', selected: false },
    ];
    param: any = { newsType: 1 };
    //图片放大
    previewImage = '';
    previewVisible = false;

    constructor(injector: Injector, private newsService: NewsService, private router: Router) {
        super(injector);
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        this.param.skipCount = request.skipCount;
        this.param.maxResultCount = request.maxResultCount;
        this.newsService.getNewsPage(this.param).finally(() => {
            finishedCallback();
        })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items.map((i) => {
                    i.showCoverPhoto = AppConsts.remoteServiceBaseUrl + i.coverPhoto;
                    return i;
                });
                this.totalItems = result.totalCount;
            })
    }

    checkChangeLeaf(item) {
        this.param.newsType = item.value;
        this.refresh();
    }
    create() {
        this.router.navigate(['app/news/news-detail', this.param.newsType]);
    }
    edit(item: News) {
        this.router.navigate(['app/news/news-detail', this.param.newsType, item.id]);
    }
    //图片放大
    handlePreview = (url: string) => {
        this.previewImage = url;
        this.previewVisible = true;
    }
}
