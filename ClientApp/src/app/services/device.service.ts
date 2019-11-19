import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device } from '../interfaces/Device';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

    constructor(private http: HttpClient) { }

    link = "api/Device/";

    all(controllerId: number): Observable<Device[]> {
        return this.http.get<Device[]>(this.link + "All/" + controllerId);
    }

    get(id: number): Observable<Device> {
        return this.http.get<Device>(this.link + id);
    }

    post(device: Device): Observable<Device> {
        return this.http.post<Device>(this.link, device);
    }

    put(device: Device): Observable<Device> {
        return this.http.put<Device>(this.link, device);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(this.link + id);
    }
}
