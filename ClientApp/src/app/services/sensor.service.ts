import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Sensor } from "../interfaces/Sensor";

@Injectable()
export class SensorService {

    constructor(private http: HttpClient) { }

    url = "api/Sensor/";

    all(controllerId: number) {
        return this.http.get<Sensor[]>(this.url + "All/" + controllerId);
    }

    get(id: number) {
        return this.http.get<Sensor>(this.url + id);
    }

    post(sensor: Sensor) {
        return this.http.post<Sensor>(this.url, sensor);
    }
  
    put(sensor: Sensor) {
        return this.http.put<Sensor>(this.url, sensor);
    }

    delete(id: number) {
        return this.http.delete(this.url + id);
    }

}
