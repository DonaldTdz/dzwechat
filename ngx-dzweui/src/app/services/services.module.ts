import { NgModule } from '@angular/core';
import { CommonHttpClient, SettingsService } from '.';
//import { HTTP_INTERCEPTORS } from '@angular/common/http';
//import { AbpHttpInterceptor } from 'abp-ng2-module/dist/src/abpHttpInterceptor';

@NgModule({
    providers: [
        CommonHttpClient,
        SettingsService
        //{ provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },//注入处理token
    ],
})
export class ServicesModule { }
