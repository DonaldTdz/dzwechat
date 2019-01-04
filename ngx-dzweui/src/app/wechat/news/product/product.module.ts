import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { NewsService } from '../../../services';
import { ProductComponent } from './product.component';
// region: components
const COMPONENTS = [ProductComponent];

const routes: Routes = [
    { path: 'product', component: ProductComponent },
];
// endregion

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        ...COMPONENTS
    ],
    providers: [NewsService]
})
export class ProductModule {
}
