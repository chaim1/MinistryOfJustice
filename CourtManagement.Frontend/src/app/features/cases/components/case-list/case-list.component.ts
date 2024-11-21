import { Component } from '@angular/core';
import { CaseService } from '../../services/case.service';
import { Case, CaseFilter } from 'src/app/core/models/case';

@Component({
  selector: 'app-case-list',
  templateUrl: './case-list.component.html',
  styleUrls: ['./case-list.component.scss']
})
export class CaseListComponent {
  cases: Case[] = [];
  filter: CaseFilter = {
    judgeId: '',
    status: null,
    sortBy: 'openDate',
    sortDirection: 'desc'
  };
  constructor(private caseService: CaseService) {}

  ngOnInit() {
    this.loadCases();
  }

  loadCases() {
    this.caseService.getCases(this.filter).subscribe(
      data => this.cases = data,
      error => console.error('Error loading cases:', error)
    );
  }
}
