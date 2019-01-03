import { Injector, ElementRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

export abstract class AppComponentBase {

    activatedRoute: ActivatedRoute;

    query: any = {
        pageIndex: 1,
        pageSize: 10,
        skipCount: function () { return (this.pageIndex - 1) * this.pageSize; },
        total: 0,
        sorter: ''
    };

    constructor(injector: Injector) {
        this.activatedRoute = injector.get(ActivatedRoute);
    }

    get IsIOS(){
        let ua = navigator.userAgent.toLowerCase();
        return /mac os/.test(ua);
    }

    get CurrentUrl() {
        if(this.IsIOS){
            return encodeURIComponent(location.href.split('#')[0]+'index.html');
        } else {
            return encodeURIComponent(location.href.split('#')[0]);
        }
    }

}
