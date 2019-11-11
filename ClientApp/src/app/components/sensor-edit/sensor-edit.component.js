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
var SensorEditComponent = /** @class */ (function () {
    function SensorEditComponent(dialogRef, fb, data, sensorService, sensorTypeService) {
        this.dialogRef = dialogRef;
        this.fb = fb;
        this.sensorService = sensorService;
        this.sensorTypeService = sensorTypeService;
        this.isAction = false;
        this.editMode = data.editMode;
        this.id = data.id;
        this.controllerId = data.controllerId;
        this.createForm();
        this.sensor = {};
        this.loadSensorTypes();
        if (this.editMode) {
            this.title = "Edit sensor";
        }
        else {
            this.title = "Add sensor";
        }
    }
    SensorEditComponent.prototype.createForm = function () {
        this.form = this.fb.group({
            Name: ["", forms_1.Validators.required],
            Pin: ["", forms_1.Validators.required],
            SensorTypeId: ["", forms_1.Validators.required]
        });
    };
    SensorEditComponent.prototype.updateForm = function () {
        this.form.setValue({
            Name: this.sensor.name,
            Pin: this.sensor.pin,
            SensorTypeId: this.sensor.sensorTypeId
        });
    };
    SensorEditComponent.prototype.loadSensorTypes = function () {
        var _this = this;
        this.sensorTypeService.all().subscribe(function (res) {
            _this.sensorTypes = res;
            _this.loadData();
        });
    };
    SensorEditComponent.prototype.loadData = function () {
        var _this = this;
        console.log(this.id);
        this.sensorService.get(this.id).subscribe(function (res) {
            _this.sensor = res;
            if (_this.editMode)
                _this.updateForm();
        });
    };
    SensorEditComponent.prototype.close = function () {
        this.dialogRef.close();
    };
    SensorEditComponent.prototype.getFormControl = function (name) {
        return this.form.get(name);
    };
    SensorEditComponent.prototype.isValid = function (name) {
        var e = this.getFormControl(name);
        return e && e.valid;
    };
    SensorEditComponent.prototype.hasError = function (name) {
        var e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    };
    SensorEditComponent.prototype.onSubmit = function () {
        var _this = this;
        this.isAction = true;
        var sensor = {};
        sensor.name = this.form.value.Name;
        sensor.pin = this.form.value.Pin;
        sensor.sensorTypeId = this.form.value.SensorTypeId;
        sensor.controllerId = this.controllerId;
        if (this.editMode) {
            sensor.id = this.sensor.id;
            sensor.status = this.sensor.status;
            sensor.controllerId = this.sensor.controllerId;
            this.sensorService.post(sensor).subscribe(function (res) {
                _this.dialogRef.close(res);
                _this.isAction = false;
            }, function (err) {
                _this.isAction = false;
                _this.form.setErrors({
                    "edit": err.error.message
                });
            });
        }
        else {
            this.sensorService.put(sensor).subscribe(function (res) {
                console.log(res);
                _this.dialogRef.close(res);
            }, function (err) {
                _this.isAction = false;
                _this.form.setErrors({
                    "edit": err.error.message
                });
            });
        }
    };
    SensorEditComponent = __decorate([
        core_1.Component({
            selector: "app-sensor-edit",
            templateUrl: "./sensor-edit.component.html",
            styleUrls: ["./sensor-edit.component.less"]
        }),
        __param(2, core_1.Inject(material_1.MAT_DIALOG_DATA))
    ], SensorEditComponent);
    return SensorEditComponent;
}());
exports.SensorEditComponent = SensorEditComponent;
//# sourceMappingURL=sensor-edit.component.js.map