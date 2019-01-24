import { NgModule } from '@angular/core';
//import { HTTP_INTERCEPTORS } from '@angular/common/http';
//import { AbpHttpInterceptor } from 'abp-ng2-module/dist/src/abpHttpInterceptor';
import { CommonHttpClient } from './common-httpclient';

@NgModule({
    providers: [
        CommonHttpClient,
        //{ provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
    ],
})
export class ServicesModule { }
