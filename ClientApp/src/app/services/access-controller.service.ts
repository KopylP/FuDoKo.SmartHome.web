import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserHasController } from '../interfaces/UserHasController';
import { ControllerAccess } from '../interfaces/ControllerAccess';

@Injectable({
  providedIn: 'root'
})
export class AccessControllerService {

    constructor(private http: HttpClient) { }

    url = "api/Access/Controller/";

    all(controllerId: number): Observable<UserHasController[]> {
        return this.http.get<UserHasController[]>(this.url + controllerId);
    }

    put(controllerAccess: ControllerAccess): Observable<UserHasController> {
        return this.http.put<UserHasController>(this.url, controllerAccess);
    }

    delete(userHasControllerId: number): Observable<any> {
        return this.http.delete(this.url + userHasControllerId);
    }
}
