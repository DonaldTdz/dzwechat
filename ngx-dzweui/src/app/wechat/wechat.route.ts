import { HomeComponent } from "./index.component";

export const routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'marketing', loadChildren: './marketing/strategy/strategy.module#StrategyModule' },
    { path: 'news', loadChildren: './news/study/study.module#StudyModule' },
    { path: 'vip', loadChildren: './vip/user/user.module#UserModule' },
    { path: '**', redirectTo: '' }
];

