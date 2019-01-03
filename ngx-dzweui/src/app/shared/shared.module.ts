import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { WeUiModule } from 'ngx-weui';
import { AqmModule } from 'angular-qq-maps';
import { GesturePasswordModule } from 'ngx-gesture-password';
import { ToastrModule } from 'ngx-toastr';
import { CountdownModule } from 'ngx-countdown';
import { PageComponent } from './page/page.component';
import { PipeModule } from './pipe/pipe.module';

const COMPONENTS = [ PageComponent ];

const THIDS = [
  PipeModule,
  CountdownModule,
  ToastrModule,
  GesturePasswordModule,
  AqmModule
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    WeUiModule,
    ...THIDS
  ],
  declarations: COMPONENTS,
  exports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    WeUiModule,
    ...THIDS,
    ...COMPONENTS
  ]
})
export class SharedModule {
}
