import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConditionType } from '../interfaces/ConditionType';

@Injectable({
    providedIn: 'root'
})
export class ConditionTypeService {

    url = "api/ConditionType/";

    constructor(private http: HttpClient) {

    }

    get(): Observable<ConditionType[]> {
        return this.http.get<ConditionType[]>(this.url);
    }
}
