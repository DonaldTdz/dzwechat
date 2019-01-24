import {
  Component,
  Injector,
  OnInit,
} from '@angular/core';

import { LoginService } from './login.service';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/component-base/app-component-base';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  animations: [appModuleAnimation()],
})
export class LoginComponent extends AppComponentBase implements OnInit {
  submitting = false;

  constructor(
    injector: Injector,
    public loginService: LoginService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.titleSrvice.setTitle(this.l('LogIn'));
  }

  get multiTenancySideIsTeanant(): boolean {
    return this.appSession.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this.appSession.tenantId) {
      return false;
    }
    return true;
  }

  login(): void {
    this.submitting = true;
    this.loginService.authenticate(() => {
      this.submitting = false;
    });
  }
}
