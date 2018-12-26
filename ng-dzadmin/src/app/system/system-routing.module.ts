import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';

import { RolesComponent } from '@app/system/roles/roles.component';
import { UsersComponent } from '@app/system/users/users.component';

const routes: Routes = [
    {
        path: 'roles',
        component: RolesComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'users',
        component: UsersComponent,
        canActivate: [AppRouteGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SystemRoutingModule { }
