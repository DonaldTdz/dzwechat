import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { NewsService } from '../../../services';
import { LaunchComponent } from './launch.component';
// region: components
const COMPONENTS = [LaunchComponent];

const routes: Routes = [
    { path: 'launch', component: LaunchComponent },
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
    providers: [NewsService]
})
export class LaunchModule {
}
