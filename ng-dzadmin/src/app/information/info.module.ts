import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';

import { InfoComponent } from './info/info.component';
import { InfoRoutingModule } from './info-routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        InfoRoutingModule,
        LayoutModule,
        SharedModule,
    ],
    declarations: [
        InfoComponent
    ],
    entryComponents: [
        InfoComponent
    ],
    // providers: [LocalizationService, MenuService],
})
export class InfoModule { }
