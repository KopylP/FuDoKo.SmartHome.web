"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var signalr_1 = require("@aspnet/signalr");
var SensorListComponent = /** @class */ (function () {
    function SensorListComponent(sensorCervice, sensorEditServie, authService) {
        this.sensorCervice = sensorCervice;
        this.sensorEditServie = sensorEditServie;
        this.authService = authService;
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
        this.hubConnection = new signalr_1.HubConnectionBuilder()
            .configureLogging(signalr_1.LogLevel.Debug)
            .withUrl('/real/sensors', {
            skipNegotiation: true,
            transport: signalr_1.HttpTransportType.WebSockets,
            accessTokenFactory: function () { return _this.authService.getAuth().token; }
        })
            .build();
        this.hubConnection
            .start()
            .then(function (p) {
            console.log("Server is started");
        })
            .catch(function (err) {
            console.log(err);
        });
        this.hubConnection.on("UpdateSensor", function (data) {
            var index = _this.sensors.findIndex(function (p) { return p.id == data.id; });
            _this.sensors.splice(index, 1, data);
        });
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