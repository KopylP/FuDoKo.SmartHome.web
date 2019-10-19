import { Injectable, Inject, PLATFORM_ID } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { isPlatformBrowser } from "@angular/common";
import { Observable, throwError } from "rxjs";
import { map, catchError } from 'rxjs/operators';
@Injectable()
export class AuthService {
  authKey: string = "auth";
  clientId: string = "FuDoKo.SmartHome";

  constructor(private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: any) {

  }
  login(username: string, password: string): Observable<boolean> | any {
    //TODO
    const url = "api/token/auth";
    const data = {
      username: username,
      password: password,
      client_id: this.clientId,
      grant_type: "password",
      scope: "offline_access profile email"
    }
    return this.http.post<TokenResponse>(url, data)
        .pipe(map((res) => {
            let token = res && res.token;
            if (token) {
                this.setAuth(res);
                return true;
            }
        }
      ),
      catchError(err => {
        return throwError(err);
      }));
  }

  logout(): boolean {
    this.setAuth(null);
    return true;
  }

  setAuth(auth: TokenResponse | null): boolean {
    if (isPlatformBrowser(this.platformId)) {
      if (auth) {
        localStorage.setItem(
          this.authKey,
          JSON.stringify(auth)
        );
      }
      else {
        localStorage.removeItem(this.authKey);
      }
    }
    return true;
  }

  getAuth(): TokenResponse | null {
    if (isPlatformBrowser(this.platformId)) {
      let i = localStorage.getItem(this.authKey);
      if (i) {
        return JSON.parse(i);
      }
      else {
        return null;
      }
    }
  }

  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.authKey) != null;
    }
    return false;
  }

}
