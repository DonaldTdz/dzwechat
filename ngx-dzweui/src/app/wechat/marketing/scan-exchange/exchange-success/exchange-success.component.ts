import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'exchange-success',
    templateUrl: 'exchange-success.component.html'
})
export class ExchangeSuccessComponent {
    constructor(private router: Router) {
    }

    ngOnInit() {
        // this.activatedRoute.params.subscribe((params: Params) => {
        //     this.userIntegral = params['userIntegral'];
        //     this.retailerIntegral = params['retailerIntegral'];
        // });
    }

    goBack() {
        this.router.navigate(['/marketing-exchange/exchange']);
    }
}
