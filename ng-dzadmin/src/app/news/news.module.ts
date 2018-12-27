import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';

import { NewsComponent } from './news/news.component';
import { NewsRoutingModule } from './news-routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NewsRoutingModule,
        LayoutModule,
        SharedModule,
    ],
    declarations: [
        NewsComponent
    ],
    entryComponents: [
        NewsComponent
    ],
    // providers: [LocalizationService, MenuService],
})
export class NewsModule { }
