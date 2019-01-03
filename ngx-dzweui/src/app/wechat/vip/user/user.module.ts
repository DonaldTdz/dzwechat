import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { UserComponent } from './user.component';
// region: components

const COMPONENTS = [UserComponent];

const routes: Routes = [
    { path: 'user', component: UserComponent },
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
export class UserModule {
}
