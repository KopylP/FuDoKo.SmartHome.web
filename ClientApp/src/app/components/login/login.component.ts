import { Component, Inject } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: "login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.less"]
})
export class LoginComponent {
  title: string;
  form: FormGroup;

  constructor(private router: Router,
    private fb: FormBuilder,
    private authService: AuthService,
    @Inject('BASE_URL') private baseUrl: string) {

    this.title = "Log in";

    this.createForm();
  }

  createForm() {
    this.form = this.fb.group({
      Username: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }

  onSubmit() {
    const url = this.baseUrl + "api/token/auth";
    const username = this.form.value.Username;
    const password = this.form.value.Password;

    this.authService.login(username, password)
      .subscribe(res => {
        alert(this.authService.getAuth()!.token)
      },
      err => {
        this.form.setErrors({
          "auth": "Username or password is incorrect"
        });
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
