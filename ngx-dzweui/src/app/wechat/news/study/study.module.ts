import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { StudyComponent } from './study.component';
// region: components

const COMPONENTS = [StudyComponent];

const routes: Routes = [
    { path: 'study', component: StudyComponent },
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
export class StudyModule {
}
