import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class ControllerService {
    url = "api/controller";
    constructor(private http: HttpClient) {

    }

    getAll(): Observable<UserHasController[]> {   
        return this.http.get<UserHasController[]>(this.url);
    }

    get(id: number): Observable<Controller>{
        return this.http.get<UserHasController>(this.url + '/' + id);
    }

    disable(id: number): Observable<any> {
        return this.http.delete(this.url + '/' + id);
    }

    edit(controller: Controller): Observable<Controller> {
        return this.http.post<Controller>(this.url, controller);
    }

    put(controller: Controller): Observable<Controller> {
        return this.http.put<Controller>(this.url, controller);
    }
}
