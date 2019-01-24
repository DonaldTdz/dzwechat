import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { NewsComponent } from './news/news.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';

const routes: Routes = [
    {
        path: 'news',
        component: NewsComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'news-detail/:newsType',
        component: NewsDetailComponent,
        // data: { translate: 'news-detail', permission: 'Pages', title: '新增资讯' },
        canActivate: [AppRouteGuard],
        data: { title: '咨讯新增' }
    },

    {
        path: 'news-detail/:newsType/:id',
        component: NewsDetailComponent,
        // data: { translate: 'news-detail', permission: 'Pages', title: '编辑资讯' },
        canActivate: [AppRouteGuard],
        data: { title: '咨讯信息' }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class NewsRoutingModule { }
