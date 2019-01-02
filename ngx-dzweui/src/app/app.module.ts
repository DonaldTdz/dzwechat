import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';

import { WeUiModule } from 'ngx-weui';
import { ToastrModule } from 'ngx-toastr';
//import { AqmModule } from 'angular-qq-maps';

import { SharedModule } from './shared/shared.module';
//import { LayoutModule } from './layout/layout.module';
//import { RoutesModule } from './routes/routes.module';
import { CoreModule } from './core/core.module';
import { AppComponent } from './app.component';
import { AppConsts } from './shared/AppConsts';
import { API_BASE_URL } from './services/common-httpclient';
import { ServicesModule } from './services/services.module';
import { WechatModule } from './wechat/wechat.module';

export function getRemoteServiceBaseUrl(): string {
  return AppConsts.remoteServiceBaseUrl;
}

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    //CoreModule,
    ServicesModule,
    SharedModule,
    //RoutesModule,
    WechatModule,
    //LayoutModule,
    WeUiModule.forRoot(),
    ToastrModule.forRoot(),
    //AqmModule.forRoot({
    //  apiKey: 'I3TBZ-QTN3J-MWPFI-FERMS-IBOCQ-LBBWY'
    //})
  ],
  declarations: [
    AppComponent
  ],
  providers: [
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl },
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
}
