import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { RegisterUser } from "../interfaces/RegisterUser";

@Injectable()
export class RegisterService {

  constructor(private http: HttpClient) {

    }
    register(user: RegisterUser): Observable<RegisterUser> {
    const url = "api/user";
    return this.http.put<RegisterUser>(url, user);
  }
}
