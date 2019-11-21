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
var keycodes_1 = require("@angular/cdk/keycodes");
var material_1 = require("@angular/material");
var free_solid_svg_icons_1 = require("@fortawesome/free-solid-svg-icons");
var ControllerAccessComponent = /** @class */ (function () {
    function ControllerAccessComponent(dialogRef, data, accessControllerService, snackBar) {
        this.dialogRef = dialogRef;
        this.accessControllerService = accessControllerService;
        this.snackBar = snackBar;
        this.faTimesCircle = free_solid_svg_icons_1.faTimesCircle;
        this.usersHasControllers = [];
        this.title = "Access Policy";
        this.visible = true;
        this.selectable = true;
        this.removable = true;
        this.addOnBlur = true;
        this.separatorKeysCodes = [keycodes_1.ENTER, keycodes_1.COMMA];
        this.controllerId = data.controllerId;
        this.loadData();
    }
    ControllerAccessComponent.prototype.remove = function (userHasController) {
        var index = this.usersHasControllers.indexOf(userHasController);
        if (index >= 0) {
            this.openSnackBarWithDelete(userHasController, index);
            this.usersHasControllers.splice(index, 1);
        }
    };
    ControllerAccessComponent.prototype.add = function (event) {
        var _this = this;
        var input = event.input;
        var value = event.value;
        if ((value || '').trim()) {
            var controllerAccess = {};
            controllerAccess.controllerId = this.controllerId;
            controllerAccess.userName = value;
            this.accessControllerService.put(controllerAccess)
                .subscribe(function (res) {
                _this.usersHasControllers.push(res);
            }, function (err) {
                _this.openSnackBar(err.error.message, "Close", "snack-error-bottom");
            });
        }
        // Reset the input value
        if (input) {
            input.value = '';
        }
    };
    ControllerAccessComponent.prototype.loadData = function () {
        var _this = this;
        this.accessControllerService.all(this.controllerId).subscribe(function (res) {
            console.log(res);
            _this.usersHasControllers = res;
        }, function (error) {
            console.log(error);
        });
    };
    ControllerAccessComponent.prototype.openSnackBar = function (message, action, snackClass) {
        return this.snackBar.open(message, action, {
            duration: 3000,
            verticalPosition: "bottom",
            horizontalPosition: "center",
            panelClass: [snackClass]
        });
    };
    ControllerAccessComponent.prototype.openSnackBarWithDelete = function (userHasController, index) {
        var _this = this;
        var snackBar = this.openSnackBar("User " + userHasController.user.name + " hasn`t access to your controller.", "UNDO", "snack-primary-bottom");
        snackBar.afterDismissed().subscribe(function (res) {
            if (!res.dismissedByAction) {
                _this.accessControllerService.delete(userHasController.id).subscribe(function (res) {
                }, function (err) {
                    console.log(err);
                });
            }
        });
        snackBar.onAction().subscribe(function (p) {
            _this.usersHasControllers.splice(index, 0, userHasController);
        });
    };
    ControllerAccessComponent.prototype.close = function () {
        this.dialogRef.close();
    };
    ControllerAccessComponent.prototype.ngOnInit = function () {
    };
    ControllerAccessComponent = __decorate([
        core_1.Component({
            selector: 'app-controller-access',
            templateUrl: './controller-access.component.html',
            styleUrls: ['./controller-access.component.less']
        }),
        __param(1, core_1.Inject(material_1.MAT_DIALOG_DATA))
    ], ControllerAccessComponent);
    return ControllerAccessComponent;
}());
exports.ControllerAccessComponent = ControllerAccessComponent;
//# sourceMappingURL=controller-access.component.js.map