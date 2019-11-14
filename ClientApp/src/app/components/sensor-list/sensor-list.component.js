"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SensorListComponent = /** @class */ (function () {
    function SensorListComponent(sensorCervice, sensorEditServie, authService, snackBar, sensorHubService) {
        this.sensorCervice = sensorCervice;
        this.sensorEditServie = sensorEditServie;
        this.authService = authService;
        this.snackBar = snackBar;
        this.sensorHubService = sensorHubService;
    }
    SensorListComponent.prototype.loadData = function () {
        var _this = this;
        this.sensorCervice.all(this.controller.id).subscribe(function (res) {
            _this.sensors = res;
            console.log(res);
        }, function (err) {
            console.log(err);
        });
    };
    SensorListComponent.prototype.ngOnInit = function () {
        this.loadData();
        this.initHub();
    };
    SensorListComponent.prototype.ngOnChanges = function (changes) {
        if (typeof changes['controller'] !== 'undefined') {
            if (!changes['controller'].firstChange) {
                this.loadData();
            }
        }
    };
    SensorListComponent.prototype.initHub = function () {
        var _this = this;
        this.sensorHubService.init(null, function () {
            _this.openSnackBar("Ooops. Real time information from sensors is is not available.", "Refresh", "snack-error")
                .subscribe(function () { return _this.initHub(); });
        });
        //listen to edit value of sensor event
        this.sensorHubService.onUpdateSensors(function (data) {
            var index = _this.sensors.findIndex(function (p) { return p.id == data.id; });
            if (index !== -1)
                _this.sensors.splice(index, 1, data);
        });
    };
    SensorListComponent.prototype.openSnackBar = function (message, action, snackClass) {
        return this.snackBar.open(message, action, {
            duration: 5000,
            verticalPosition: "top",
            horizontalPosition: "right",
            panelClass: [snackClass]
        }).onAction();
    };
    SensorListComponent.prototype.addSensor = function () {
        var _this = this;
        this.sensorEditServie
            .open(false, this.controller.id)
            .afterClosed()
            .subscribe(function (res) {
            if (typeof res !== "undefined") {
                _this.sensors.push(res);
            }
        });
    };
    SensorListComponent.prototype.deleteSensor = function (id) {
        var index = this.sensors.findIndex(function (p) { return p.id === id; });
        this.sensors.splice(index, 1);
    };
    __decorate([
        core_1.Input()
    ], SensorListComponent.prototype, "controller", void 0);
    SensorListComponent = __decorate([
        core_1.Component({
            selector: "app-sensor-list",
            templateUrl: "./sensor-list.component.html",
            styleUrls: ["./sensor-list.component.less"]
        })
    ], SensorListComponent);
    return SensorListComponent;
}());
exports.SensorListComponent = SensorListComponent;
//# sourceMappingURL=sensor-list.component.js.map