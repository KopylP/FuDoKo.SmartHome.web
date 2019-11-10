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
    function SensorListComponent(sensorCervice) {
        this.sensorCervice = sensorCervice;
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
    };
    SensorListComponent.prototype.addSensor = function () {
    };
    SensorListComponent.prototype.deleteSensor = function () {
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