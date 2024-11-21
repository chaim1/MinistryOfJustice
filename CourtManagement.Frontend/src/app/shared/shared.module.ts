import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { LoadingComponent } from './components/loading/loading.component';
import { StatusFormatterPipe } from './pipes/status-formatter.pipe';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    LoadingComponent,
    StatusFormatterPipe
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
