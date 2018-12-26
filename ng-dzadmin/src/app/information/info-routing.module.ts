import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { InfoComponent } from './info/info.component';

const routes: Routes = [
    {
        path: 'info',
        component: InfoComponent,
        canActivate: [AppRouteGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class InfoRoutingModule { }
