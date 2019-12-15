import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeviceConfiguration } from '../interfaces/DeviceConfiguration';

@Injectable({
    providedIn: 'root'
})
export class DeviceConfigurationService {

    constructor(private http: HttpClient) { }

    private url = "api/DeviceConfiguration/";

    get(id: number): Observable<DeviceConfiguration> {
        return this.http.get<DeviceConfiguration>(this.url + id);
    }

    post(deviceConfiguration: DeviceConfiguration): Observable<DeviceConfiguration> {
        return this.http.post<DeviceConfiguration>(this.url, deviceConfiguration);
    }

    put(deviceConfiguration: DeviceConfiguration): Observable<DeviceConfiguration> {
        return this.http.put<DeviceConfiguration>(this.url, deviceConfiguration);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(this.url + id);
    }
}
