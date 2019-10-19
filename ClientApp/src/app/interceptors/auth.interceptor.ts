import { Injectable, Injector } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private injector: Injector) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let auth = this.injector.get(AuthService);
        var token = (auth.isLoggedIn()) ? auth.getAuth()!.token : null;
        if (token) {
            req = req.clone({
                setHeaders: {
                    Authorisation: `Bearer ${token}`
                }
            });
        }
        return next.handle(req);
    }

}
