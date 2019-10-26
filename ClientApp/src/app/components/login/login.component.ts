import { Component, Inject, ViewEncapsulation, OnDestroy } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";
import { faUnlockAlt, faUser, faUserPlus, faUsers, faKey } from '@fortawesome/free-solid-svg-icons';
@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.less"],
    encapsulation: ViewEncapsulation.None
})
export class LoginComponent {

    faUnlockAlt = faUnlockAlt;
    faUser = faUser;
    faUsers = faUsers;
    faKey = faKey;
    title: string;
    form: FormGroup;
    isSignAction: boolean = false;

    constructor(private router: Router,
        private fb: FormBuilder,
        private authService: AuthService,
        @Inject('BASE_URL') private baseUrl: string) {

        this.title = "Sign in";

        this.createForm();
    }

    createForm() {
        this.form = this.fb.group({
            Username: ['', Validators.required],
            Password: ['', Validators.required]
        });
    }

    onSubmit() {
        this.isSignAction = true;
        const url = this.baseUrl + "api/token/auth";
        const username = this.form.value.Username;
        const password = this.form.value.Password;

        this.authService.login(username, password)
            .subscribe(res => {
                alert(this.authService.getAuth()!.token);
                this.isSignAction = false;
            },
                err => {
                    this.form.setErrors({
                        "auth": "Username or password is incorrect"
                    });
                    this.isSignAction = false;
                });
    }

    onBack() {
        this.router.navigate(["home"]);
    }

    getFormControl(name: string) {
        return this.form.get(name);
    }

    isValid(name: string) {
        let e = this.getFormControl(name);
        return e && e.valid;
    }

    hasError(name: string) {
        let e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    }

    

}
