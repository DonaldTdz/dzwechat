import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';

import { TenantsComponent } from '@app/system/tenants/tenants.component';
import { UsersComponent } from '@app/system/users/users.component';
import { RolesComponent } from '@app/system/roles/roles.component';
import { CreateTenantComponent } from '@app/system/tenants/create-tenant/create-tenant.component';
import { EditTenantComponent } from '@app/system/tenants/edit-tenant/edit-tenant.component';
import { CreateRoleComponent } from '@app/system/roles/create-role/create-role.component';
import { EditRoleComponent } from '@app/system/roles/edit-role/edit-role.component';
import { CreateUserComponent } from '@app/system/users/create-user/create-user.component';
import { EditUserComponent } from '@app/system/users/edit-user/edit-user.component';
import { SystemRoutingModule } from './system-routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        SystemRoutingModule,
        LayoutModule,
        SharedModule,
    ],
    declarations: [
        TenantsComponent,
        UsersComponent,
        RolesComponent,
        CreateTenantComponent,
        EditTenantComponent,
        CreateRoleComponent,
        EditRoleComponent,
        CreateUserComponent,
        EditUserComponent,
    ],
    entryComponents: [
        CreateTenantComponent,
        EditTenantComponent,
        CreateRoleComponent,
        EditRoleComponent,
        CreateUserComponent,
        EditUserComponent,
    ],
    // providers: [LocalizationService, MenuService],
})
export class SystemModule { }
