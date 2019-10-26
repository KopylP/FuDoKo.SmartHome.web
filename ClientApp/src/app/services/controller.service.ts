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

    get(id: number): Observable<UserHasController>{
        return this.http.get<UserHasController>(this.url + '/' + id);
    }
}
