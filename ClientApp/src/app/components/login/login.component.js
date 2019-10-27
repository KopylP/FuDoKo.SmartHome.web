"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var free_solid_svg_icons_1 = require("@fortawesome/free-solid-svg-icons");
var LoginComponent = /** @class */ (function () {
    function LoginComponent(router, fb, authService, baseUrl) {
        this.router = router;
        this.fb = fb;
        this.authService = authService;
        this.baseUrl = baseUrl;
        this.faUnlockAlt = free_solid_svg_icons_1.faUnlockAlt;
        this.faUser = free_solid_svg_icons_1.faUser;
        this.faUsers = free_solid_svg_icons_1.faUsers;
        this.faKey = free_solid_svg_icons_1.faKey;
        this.isSignAction = false;
        this.title = "Sign in";
        this.createForm();
    }
    LoginComponent.prototype.createForm = function () {
        this.form = this.fb.group({
            Username: ['', forms_1.Validators.required],
            Password: ['', forms_1.Validators.required]
        });
    };
    LoginComponent.prototype.onSubmit = function () {
        var _this = this;
        this.isSignAction = true;
        var url = this.baseUrl + "api/token/auth";
        var username = this.form.value.Username;
        var password = this.form.value.Password;
        this.authService.login(username, password)
            .subscribe(function (res) {
            _this.isSignAction = false;
            _this.router.navigate(["/"]);
        }, function (err) {
            _this.form.setErrors({
                "auth": "Username or password is incorrect"
            });
            _this.isSignAction = false;
        });
    };
    LoginComponent.prototype.onBack = function () {
        this.router.navigate(["home"]);
    };
    LoginComponent.prototype.getFormControl = function (name) {
        return this.form.get(name);
    };
    LoginComponent.prototype.isValid = function (name) {
        var e = this.getFormControl(name);
        return e && e.valid;
    };
    LoginComponent.prototype.hasError = function (name) {
        var e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    };
    LoginComponent = __decorate([
        core_1.Component({
            selector: "login",
            templateUrl: "./login.component.html",
            styleUrls: ["./login.component.less"]
        }),
        __param(3, core_1.Inject('BASE_URL'))
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map