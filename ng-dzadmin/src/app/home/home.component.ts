import { Component, Injector, AfterViewInit, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd';
import { ACLService } from '@delon/acl';
import { HomeService } from 'services';
import { HomeInfo } from 'entities';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],

  animations: [appModuleAnimation()],
})
export class HomeComponent extends AppComponentBase implements OnInit {

  homeInfo: HomeInfo = new HomeInfo();
  constructor(
    injector: Injector, private http: _HttpClient, public msg: NzMessageService,
    private aclService: ACLService, private homeService: HomeService,
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getHomeInfo();
  }
  getHomeInfo() {
    this.homeService.getHomeInfo().subscribe((data) => {
      this.homeInfo = data;
    });
  }
  // showAdvertising() {
  // }
}
