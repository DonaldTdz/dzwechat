import { Component, ViewEncapsulation, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '../../../shared/app-component-base';
import { News } from '../../../entities';
import { NewsService } from '../../../services';
import { AppConsts } from '../../../shared/AppConsts';

@Component({
    selector: 'wechat-launch',
    templateUrl: './launch.component.html',
    styleUrls: ['./launch.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class LaunchComponent extends AppComponentBase implements OnInit {

    news: News[];
    hostUrl = AppConsts.remoteServiceBaseUrl;

    constructor(injector: Injector, private newsService: NewsService) {
        super(injector);
    }
    
    ngOnInit(): void {
        this.newsService.GetNewsByType(2).subscribe((res) => {
            this.news = res;
        });
    }

    goLink(link: string) {
        location.href = link;
    }
    
}