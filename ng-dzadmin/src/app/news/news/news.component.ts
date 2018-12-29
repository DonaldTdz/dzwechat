import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/component-base';
import { NewsService, PagedResultDtoOfNews } from 'services';
import { Router } from '@angular/router';
import { News } from 'entities';

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
        console.log(this.param);
        this.newsService.getNewsPage(this.param).finally(() => {
            finishedCallback();
        })
            .subscribe((result: PagedResultDtoOfNews) => {
                this.dataList = result.items;
                this.totalItems = result.totalCount;
            })
    }

    checkChangeLeaf(item) {
        this.param.newsType = item.value;
        this.refresh();
    }
    // create() {
    //     console.log('aa')
    //     this.router.navigate(['app/news/news-detail', this.param.newsType]);
    // }
    // edit(item: News) {
    //     this.router.navigate(['app/news/news-detail', { newsType: this.param.newsType, id: item.id }]);
    // }
}
