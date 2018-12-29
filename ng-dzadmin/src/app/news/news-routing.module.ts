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
        canActivate: [AppRouteGuard],
    },

    // {
    //     path: 'news-detail/:newsType&:id',
    //     component: NewsDetailComponent,
    //     canActivate: [AppRouteGuard],
    // }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class NewsRoutingModule { }
