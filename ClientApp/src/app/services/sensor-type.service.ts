import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SensorType } from "../interfaces/SensorType";
import { Observable } from "rxjs";

@Injectable()
export class SensorTypeService {
    url = "api/SensorType/"
    constructor(private http: HttpClient) { }

    all(): Observable<SensorType[]> {
        return this.http.get<SensorType[]>(this.url);
    }
}
