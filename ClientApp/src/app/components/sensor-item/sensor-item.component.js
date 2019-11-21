"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var free_solid_svg_icons_1 = require("@fortawesome/free-solid-svg-icons");
var SensorItemComponent = /** @class */ (function () {
    function SensorItemComponent(sensorService, sensorEditService) {
        this.sensorService = sensorService;
        this.sensorEditService = sensorEditService;
        this.faThermometerHalf = free_solid_svg_icons_1.faThermometerHalf;
        this.faSun = free_solid_svg_icons_1.faSun;
        this.faTint = free_solid_svg_icons_1.faTint;
        this.onSensorDelete = new core_1.EventEmitter();
    }
    SensorItemComponent.prototype.ngOnInit = function () {
        switch (this.sensor.sensorType.typeName) {
            case "light":
                this.selectedIcon = free_solid_svg_icons_1.faSun;
                break;
            case "submersion":
                this.selectedIcon = free_solid_svg_icons_1.faTint;
                break;
            case "temperature":
                this.selectedIcon = free_solid_svg_icons_1.faThermometerHalf;
                break;
        }
    };
    SensorItemComponent.prototype.editSensor = function () {
        var _this = this;
        this.sensorEditService.open(true, this.sensor.controllerId, this.sensor.id)
            .afterClosed()
            .subscribe(function (res) {
            console.log(res);
            if (typeof res !== "undefined") {
                _this.sensor = res;
            }
        });
    };
    SensorItemComponent.prototype.deleteSensor = function () {
        var _this = this;
        this.sensorService.delete(this.sensor.id).subscribe(function (res) {
            _this.onSensorDelete.emit(_this.sensor.id);
        });
    };
    SensorItemComponent.prototype.toggleMenu = function () {
        if (this.isAdmin) {
            this.trigger.openMenu();
        }
        return false;
    };
    SensorItemComponent.prototype.changeStatus = function () {
        var _this = this;
        this.sensorService.post(this.sensor).subscribe(function (res) {
            _this.sensor = res;
        }, function (err) {
            console.log(err);
        });
    };
    __decorate([
        core_1.Input()
    ], SensorItemComponent.prototype, "sensor", void 0);
    __decorate([
        core_1.Input()
    ], SensorItemComponent.prototype, "isAdmin", void 0);
    __decorate([
        core_1.Output()
    ], SensorItemComponent.prototype, "onSensorDelete", void 0);
    __decorate([
        core_1.ViewChild('menuTrigger', { static: true })
    ], SensorItemComponent.prototype, "trigger", void 0);
    SensorItemComponent = __decorate([
        core_1.Component({
            selector: "app-sensor-item",
            templateUrl: "./sensor-item.component.html",
            styleUrls: ["./sensor-item.component.less"]
        })
    ], SensorItemComponent);
    return SensorItemComponent;
}());
exports.SensorItemComponent = SensorItemComponent;
//# sourceMappingURL=sensor-item.component.js.map