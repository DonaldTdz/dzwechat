import { HomeComponent } from "./index.component";

export const routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'marketing-strategy', loadChildren: './marketing/strategy/strategy.module#StrategyModule' },
    { path: 'news-study', loadChildren: './news/study/study.module#StudyModule' },
    { path: 'news-launch', loadChildren: './news/launch/launch.module#LaunchModule' },
    { path: 'news-product', loadChildren: './news/product/product.module#ProductModule' },
    { path: 'vip', loadChildren: './vip/user/user.module#UserModule' },
    { path: '**', redirectTo: '' }
];

