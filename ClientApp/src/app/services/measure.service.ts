import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Measure } from '../interfaces/Measure';

@Injectable({
  providedIn: 'root'
})
export class MeasureService {

    constructor(private http: HttpClient) { }

    private url = "api/Measure/";

    all(deviceTypeId: number): Observable<Measure[]> {
        return this.http.get<Measure[]>(this.url + deviceTypeId);
    }
}
