import { NgModule } from '@angular/core';
import { ScanExchangeComponent } from './scan-exchange.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';

const COMPONENTS = [ScanExchangeComponent];

const routes: Routes = [
    { path: 'exchange', component: ScanExchangeComponent },
];
@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        ...COMPONENTS
    ],
    providers: [
    ]
})
export class ScanExchangeModule {

}
