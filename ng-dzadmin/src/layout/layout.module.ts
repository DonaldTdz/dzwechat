import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { HeaderComponent } from './default/header/header.component';
import { SidebarComponent } from './default/sidebar/sidebar.component';
import { HeaderFullScreenComponent } from './default/header/components/fullscreen.component';
import { HeaderI18nComponent } from './default/header/components/i18n.component';
import { HeaderStorageComponent } from './default/header/components/storage.component';
import { HeaderUserComponent } from './default/header/components/user.component';
import { YoYoSidebarNavComponent } from './default/sidebar/components/yoyo-sidebar-nav.component';
import { LayoutDefaultComponent } from './default/layout-default.component';
import { ChangePasswordComponent } from './default/change-password/change-password.component';
import { HeaderNotifyComponent } from '@layout/default/header/components/notify.component';
import { OrderService } from 'services';

const COMPONENTS = [
  HeaderComponent,
  SidebarComponent,
  LayoutDefaultComponent
];

const HEADERCOMPONENTS = [
  HeaderFullScreenComponent,
  HeaderI18nComponent,
  HeaderStorageComponent,
  HeaderUserComponent,
  ChangePasswordComponent,
  HeaderNotifyComponent,
];

const SIDEBARCOMPONENTS = [
  YoYoSidebarNavComponent
]

// passport


@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
    ...COMPONENTS,
    ...HEADERCOMPONENTS,
    ...SIDEBARCOMPONENTS,
  ],
  exports: [
    ...COMPONENTS
  ],
  providers: [OrderService],
})
export class LayoutModule { }
