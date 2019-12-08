import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Script } from '../interfaces/Script';

@Injectable({
  providedIn: 'root'
})
export class ScriptService {

    private url = "api/Script/";

    constructor(private http: HttpClient) { }

    all(controllerId: number): Observable<Script[]> {
        return this.http.get<Script[]>(this.url + "All/" + controllerId);
    }

    get(id: number): Observable<Script> {
        return this.http.get<Script>(this.url + id);
    }

    post(script: Script): Observable<Script> {
        return this.http.post<Script>(this.url, script);
    }

    put(script: Script): Observable<Script> {
        return this.http.put<Script>(this.url, script);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(this.url + id);
    }
}
