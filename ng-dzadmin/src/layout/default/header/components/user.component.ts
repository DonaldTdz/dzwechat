import { Component, Inject, Injector, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SettingsService } from '@delon/theme';
import { AppComponentBase } from '@shared/component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { ChangePasswordComponent } from '@layout/default/change-password/change-password.component';
// import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';

@Component({
  selector: 'header-user',
  template: `
  <nz-dropdown nzPlacement="bottomRight">
    <div class="alain-default__nav-item d-flex align-items-center px-sm" nz-dropdown>
      <nz-avatar  nzSize="small" class="mr-sm"  [nzSrc]="'assets/avatar.jpg'" ></nz-avatar>
    </div>
    <div nz-menu class="width-sm">
      <div nz-menu-item (click)="chanagepwd()">
        <i nz-icon type="lock" class="mr-sm"></i>
        修改密码
      </div>
      <li nz-menu-divider></li>
      <div nz-menu-item (click)="logout()">
        <i nz-icon type="logout" class="mr-sm"></i>
        {{l('Logout')}}
      </div>
    </div>
  </nz-dropdown>
  <change-password-modal #changePasswordModal (modalSave)="callBack()"></change-password-modal>
  `,
})
export class HeaderUserComponent extends AppComponentBase {

  @ViewChild('changePasswordModal') changePasswordModal: ChangePasswordComponent;

  constructor(
    injector: Injector,
    private _authService: AppAuthService
  ) {
    super(injector);
  }

  logout(): void {
    this._authService.logout();
  }

  chanagepwd(): void {
    this.changePasswordModal.show();
  }

  callBack(): void {
  }
}

