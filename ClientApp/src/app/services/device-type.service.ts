import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeviceType } from '../interfaces/DeviceType';

@Injectable({
  providedIn: 'root'
})
export class DeviceTypeService {

    link = "api/DeviceType/"

    constructor(private http: HttpClient) {

    }

    all(): Observable<DeviceType[]> {
        return this.http.get<DeviceType[]>(this.link);
    }
}
