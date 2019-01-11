import { NgModule, Injector, APP_INITIALIZER } from '@angular/core';
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
import { SettingsService } from './services';

export function getRemoteServiceBaseUrl(): string {
  return AppConsts.remoteServiceBaseUrl;
}

export function StartupServiceFactory(injector: Injector): Function {
  return () => {
    let settingSer = injector.get(SettingsService);
    return settingSer.load();
  };
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
    SettingsService,
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl },
    {
      provide: APP_INITIALIZER,
      useFactory: StartupServiceFactory,
      deps: [Injector],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
}
