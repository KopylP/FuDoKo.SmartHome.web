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
var material_1 = require("@angular/material");
var forms_1 = require("@angular/forms");
var EditControllerComponent = /** @class */ (function () {
    function EditControllerComponent(dialogRef, fb, controllerService, data) {
        this.dialogRef = dialogRef;
        this.fb = fb;
        this.controllerService = controllerService;
        this.isAction = false;
        this.editMode = data.editMode;
        this.id = data.id;
        this.createForm();
        this.controller = {};
        if (this.editMode) {
            this.loadData();
            this.title = "Edit controller";
        }
        else {
            this.title = "Create controller";
        }
    }
    EditControllerComponent.prototype.createForm = function () {
        this.form = this.fb.group({
            Name: ["", forms_1.Validators.required],
            Mac: ["", forms_1.Validators.required],
        });
    };
    EditControllerComponent.prototype.loadData = function () {
        var _this = this;
        this.controllerService.get(this.id).subscribe(function (res) {
            _this.controller = res;
            _this.updateForm();
        }, function (err) {
        });
    };
    EditControllerComponent.prototype.updateForm = function () {
        this.form.setValue({
            Name: this.controller.name,
            Mac: this.controller.mac
        });
    };
    EditControllerComponent.prototype.onSubmit = function () {
        var _this = this;
        this.isAction = true;
        console.log("On submit");
        var submitController = {};
        submitController.mac = this.form.value.Mac;
        submitController.name = this.form.value.Name;
        if (this.editMode) {
            submitController.id = this.controller.id;
            submitController.status = this.controller.status;
            this.controllerService.edit(submitController).subscribe(function (res) {
                _this.dialogRef.close(res);
                _this.isAction = false;
            }, function (err) {
                console.log(err);
                _this.form.setErrors({
                    "edit": err
                });
                _this.isAction = false;
            });
        }
        else {
            this.controllerService.put(submitController).subscribe(function (res) {
                _this.dialogRef.close(res);
                _this.isAction = false;
            }, function (err) {
                console.log(err);
                _this.form.setErrors({
                    "edit": err.error.message
                });
                _this.isAction = false;
            });
        }
    };
    EditControllerComponent.prototype.close = function () {
        this.dialogRef.close();
    };
    EditControllerComponent.prototype.getFormControl = function (name) {
        return this.form.get(name);
    };
    EditControllerComponent.prototype.isValid = function (name) {
        var e = this.getFormControl(name);
        return e && e.valid;
    };
    EditControllerComponent.prototype.hasError = function (name) {
        var e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    };
    EditControllerComponent = __decorate([
        core_1.Component({
            selector: 'app-edit-controller',
            templateUrl: './edit-controller.component.html',
            styleUrls: ['./edit-controller.component.less']
        }),
        __param(3, core_1.Inject(material_1.MAT_DIALOG_DATA))
    ], EditControllerComponent);
    return EditControllerComponent;
}());
exports.EditControllerComponent = EditControllerComponent;
//# sourceMappingURL=edit-controller.component.js.map