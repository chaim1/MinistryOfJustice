import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CasesRoutingModule } from './cases-routing.module';
import { CaseListComponent } from './components/case-list/case-list.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CaseListComponent
  ],
  imports: [
    CommonModule,
    CasesRoutingModule,
    FormsModule  
  ]
})
export class CasesModule { }
