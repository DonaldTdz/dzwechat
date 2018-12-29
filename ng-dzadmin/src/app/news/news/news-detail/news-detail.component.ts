import { Component, OnInit, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ActivatedRoute } from '@angular/router';
import { NewsService } from 'services';
import { News } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'news-detail',
    templateUrl: 'news-detail.component.html',
})
export class NewsDetailComponent extends ModalComponentBase implements OnInit {
    title: string;
    id: string;
    newsType: string;
    news: News = null;
    constructor(injector: Injector, private actRoter: ActivatedRoute, private newsService: NewsService) {
        super(injector);
        this.id = this.actRoter.snapshot.params['id'];
        this.newsType = this.actRoter.snapshot.params['newsType'];
    }
    ngOnInit(): void {
        if (this.id) {
            this.title = '编辑资讯';
        } else {
            this.title = '新增资讯';
        }
    }

    getSingleNews() {
        this.newsService.getnewsById(this.id, this.newsType).subscribe((result) => {
            this.news = result;
        });
    }
}
