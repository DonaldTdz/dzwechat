import { Component, Injector, AfterViewInit, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd';
import { ACLService } from '@delon/acl';
import { HomeService } from 'services';
import { HomeInfo, GroupStatistics } from 'entities';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],

  animations: [appModuleAnimation()],
})
export class HomeComponent extends AppComponentBase implements OnInit {

  homeInfo: HomeInfo = new HomeInfo();
  integralStatis: GroupStatistics[] = [];
  goodsStatis: GroupStatistics[] = [];
  integralMoth: GroupStatistics[] = [];
  integralStatisData = [];
  goodsStatisData = [];
  inTotal: number;
  goodTotal: number;
  inMoth = { growthTotal: null, depleteTotal: null }
  searchMoth = 1;
  tags = [
    { value: 1, text: '近半年' },
    { value: 2, text: '近一年' },
  ];
  constructor(
    injector: Injector, private http: _HttpClient, public msg: NzMessageService,
    private aclService: ACLService, private homeService: HomeService,
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getHomeInfo();
    this.getGoodsStatis();
    this.getIntegralStatisByGoods();
  }
  getHomeInfo() {
    this.homeService.getHomeInfo().subscribe((data) => {
      this.homeInfo = data;
    });
  }
  //#region 商品销售前十
  getGoodsStatis() {
    this.homeService.getGoodsStatis().subscribe((data) => {
      this.goodsStatis = data;
      this.goodTotal = 0;
      data.forEach(i => {
        this.goodTotal += i.total;
      });
      const goodsData = [];
      this.goodsStatis.forEach(item => {
        goodsData.push({
          x: item.groupName,
          y: item.total
        });
      })
      this.goodsStatisData = goodsData;
    });
  }
  //#endregion

  //#region 积分销售前十
  getIntegralStatisByGoods() {
    this.homeService.getIntegralStatisByGoods().subscribe((data) => {
      this.integralStatis = data;
      this.inTotal = 0;
      data.forEach(i => {
        this.inTotal += i.integralTotal;
      });
      const interalData = [];
      this.integralStatis.forEach(item => {
        interalData.push({
          x: item.groupName,
          y: item.integralTotal
        });
      })
      this.integralStatisData = interalData;
    });

  }
  //#endregion

  //#region   积分统计（按月）
  changeCategory() {

  }
  //#endregion


  // showAdvertising() {
  // }
}
