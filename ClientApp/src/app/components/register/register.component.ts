import { Component, Inject } from "@angular/core";
import { FormGroup, FormBuilder, Validators, FormControl, ValidatorFn } from "@angular/forms";
import { Router } from "@angular/router";
import { RegisterService } from "../../services/register.service";

@Component({
    selector: "register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.less"]
})
export class RegisterComponent {
    title: string;
    form: FormGroup;

    constructor(private router: Router,
        private fb: FormBuilder,
        private registerService: RegisterService) {

        this.title = "Sign up";

        this.createForm();
    }

    passwordConfirmValidator(control: FormControl): ValidatorFn {
        let p = control.root.get("Password");
        let pc = control.root.get("ConfirmPassword");

        if (p && pc) {
            if (p.value !== pc.value) {
                pc.setErrors({
                    "PasswordMismatch": true
                });
            }
        }
        else {
            pc.setErrors(null);
        }
        return null;
    }

    createForm() {
        this.form = this.fb.group({
            Username: ['', Validators.required],
            Email: ['', [
                Validators.email, Validators.required
              ]
            ],
            Name: ['', Validators.required],
            Surname: ['', Validators.required],
            Password: ['', Validators.required],
            ConfirmPassword: ['', Validators.required]
        }, {
            validator: this.passwordConfirmValidator
        });
    }

    onSubmit() {
        const user = <RegisterUser>{};
        user.userName = this.form.value.Username;
        user.password = this.form.value.Password;
        user.email = this.form.value.Email;
        user.name = this.form.value.Name;
        user.surname = this.form.value.Surname;
        this.registerService.register(user).subscribe(res => {
            this.router.navigate(["auth"]);
        }, err => {
            this.form.setErrors({
                "register": err.message
            });
            console.log(err);
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
