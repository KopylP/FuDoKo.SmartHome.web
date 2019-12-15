import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Script } from '../interfaces/Script';
import { Command } from '../interfaces/Command';

@Injectable({
  providedIn: 'root'
})
export class CommandService {

    constructor(private http: HttpClient) { }
    private url = "api/Command/"
    all(scriptId: number): Observable<Command[]> {
        return this.http.get<Command[]>(this.url + "All/" + scriptId);
    }

    get(id: number): Observable<Command> {
        return this.http.get<Command>(this.url + id);
    }

    post(command: Command): Observable<Command> {
        return this.http.post<Command>(this.url, command);
    }

    put(command: Command): Observable<Command> {
        return this.http.put<Command>(this.url, command);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(this.url);
    }
}
