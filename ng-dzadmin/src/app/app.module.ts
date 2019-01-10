import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from '@app/app-routing.module';
import { LayoutModule } from '@layout/layout.module';
import { HomeComponent } from '@app/home/home.component';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { HomeService } from 'services';
import { G2GroupBarModule } from '../../packages/group-bar';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule,
    G2GroupBarModule,
  ],
  declarations: [
    HomeComponent,
  ],
  entryComponents: [
  ],
  providers: [HomeService],
})
export class AppModule { }
