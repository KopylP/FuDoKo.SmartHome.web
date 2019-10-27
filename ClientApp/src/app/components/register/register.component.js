"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var free_solid_svg_icons_1 = require("@fortawesome/free-solid-svg-icons");
var RegisterComponent = /** @class */ (function () {
    function RegisterComponent(router, fb, registerService) {
        this.router = router;
        this.fb = fb;
        this.registerService = registerService;
        this.faUser = free_solid_svg_icons_1.faUser;
        this.title = "Sign up";
        document.body.classList.add("body-class");
        this.createForm();
    }
    RegisterComponent.prototype.passwordConfirmValidator = function (control) {
        var p = control.root.get("Password");
        var pc = control.root.get("ConfirmPassword");
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
    };
    RegisterComponent.prototype.createForm = function () {
        this.form = this.fb.group({
            Username: ['', forms_1.Validators.required],
            Email: ['', [
                    forms_1.Validators.email, forms_1.Validators.required
                ]
            ],
            Name: ['', forms_1.Validators.required],
            Surname: ['', forms_1.Validators.required],
            Password: ['', forms_1.Validators.required],
            ConfirmPassword: ['', forms_1.Validators.required]
        }, {
            validator: this.passwordConfirmValidator
        });
    };
    RegisterComponent.prototype.onSubmit = function () {
        var _this = this;
        var user = {};
        user.userName = this.form.value.Username;
        user.password = this.form.value.Password;
        user.email = this.form.value.Email;
        user.name = this.form.value.Name;
        user.surname = this.form.value.Surname;
        this.registerService.register(user).subscribe(function (res) {
            _this.router.navigate(["auth"]);
        }, function (err) {
            _this.form.setErrors({
                "register": err.error.message
            });
            console.log(err);
        });
    };
    RegisterComponent.prototype.onBack = function () {
        this.router.navigate(["home"]);
    };
    RegisterComponent.prototype.getFormControl = function (name) {
        return this.form.get(name);
    };
    RegisterComponent.prototype.isValid = function (name) {
        var e = this.getFormControl(name);
        return e && e.valid;
    };
    RegisterComponent.prototype.hasError = function (name) {
        var e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    };
    RegisterComponent = __decorate([
        core_1.Component({
            selector: "register",
            templateUrl: "./register.component.html",
            styleUrls: ["./register.component.less"]
        })
    ], RegisterComponent);
    return RegisterComponent;
}());
exports.RegisterComponent = RegisterComponent;
//# sourceMappingURL=register.component.js.map