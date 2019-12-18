import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserHasDevice } from '../interfaces/UserHasDevice';
import { DeviceAccess } from '../interfaces/DeviceAccess';

@Injectable({
  providedIn: 'root'
})
export class DeviceAccessService {

    constructor(private http: HttpClient) { }

    url = "api/Access/Device/";

    getUsers(id): Observable<UserHasDevice[]> {
        return this.http.get<UserHasDevice[]>(this.url + id);
    }

    put(deviceAccess: DeviceAccess): Observable<UserHasDevice> {
        return this.http.put<UserHasDevice>(this.url, deviceAccess);
    }

    delete(userHasDeviceId: number): Observable<any> {
        return this.http.delete(this.url + "UserHasDevice/" + userHasDeviceId);
    }
}
