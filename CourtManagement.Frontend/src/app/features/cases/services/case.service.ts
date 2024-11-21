import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Case, CaseFilter } from 'src/app/core/models/case';

@Injectable({
  providedIn: 'root'
})
export class CaseService {

  private apiUrl = 'http://localhost:5163/api/cases';

  constructor(private http: HttpClient) {}

  getCases(filter: CaseFilter): Observable<Case[]> {
    let params = new HttpParams();
    
    if (filter.judgeId) {
      params = params.set('judgeId', filter.judgeId);
    }
    if (filter.status !== null) {
      params = params.set('status', filter.status);
    }
    params = params.set('sortBy', filter.sortBy);
    params = params.set('sortDirection', filter.sortDirection);

    return this.http.get<Case[]>(this.apiUrl, { params });
  }
}
