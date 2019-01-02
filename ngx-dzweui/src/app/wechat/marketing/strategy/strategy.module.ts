import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { StrategyComponent } from './strategy.component';
// region: components

const COMPONENTS = [StrategyComponent];

const routes: Routes = [
    { path: 'strategy', component: StrategyComponent },
];
// endregion

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        ...COMPONENTS
    ],
    providers: [
    ]
})
export class StrategyModule {
}
