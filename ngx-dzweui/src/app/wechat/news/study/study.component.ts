import { Component, ViewEncapsulation, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '../../../shared/app-component-base';
import { News } from '../../../entities';
import { NewsService } from '../../../services';
import { AppConsts } from '../../../shared/AppConsts';

@Component({
    selector: 'wechat-study',
    templateUrl: './study.component.html',
    styleUrls: ['./study.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class StudyComponent extends AppComponentBase implements OnInit {

    news: News[];
    hostUrl = AppConsts.remoteServiceBaseUrl;

    constructor(injector: Injector, private newsService: NewsService) {
        super(injector);
    }
    
    ngOnInit(): void {
        this.newsService.GetNewsByType(1).subscribe((res) => {
            this.news = res;
        });
    }

    goLink(link: string) {
        location.href = link;
    }
    
}