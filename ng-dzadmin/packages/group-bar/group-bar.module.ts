// Angular Imports
import { NgModule, ModuleWithProviders } from '@angular/core';

// This Module's Components
import { G2GroupBarComponent } from './group-bar.component';
import { CommonModule } from '@angular/common';
import { DelonUtilModule } from '@delon/util';

@NgModule({
    imports: [
        CommonModule,
        DelonUtilModule,
    ],
    declarations: [
        G2GroupBarComponent,
    ],
    exports: [
        G2GroupBarComponent,
    ]
})
export class G2GroupBarModule {
    static forRoot(): ModuleWithProviders {
        return { ngModule: G2GroupBarModule, providers: [] };
    }
}
