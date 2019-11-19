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
var SensorHubService = /** @class */ (function () {
    function SensorHubService(authService) {
        var _this = this;
        this.authService = authService;
        this.hubConnection = new signalr_1.HubConnectionBuilder()
            .configureLogging(signalr_1.LogLevel.Debug)
            .withUrl('/real/sensors', {
            skipNegotiation: true,
            transport: signalr_1.HttpTransportType.WebSockets,
            accessTokenFactory: function () { return _this.authService.getAuth().token; }
        })
            .build();
    }
    SensorHubService.prototype.init = function (onSuccess, onError) {
        if (onError === void 0) { onError = null; }
        this.hubConnection
            .start()
            .then(function (p) {
            if (onSuccess)
                onSuccess();
        })
            .catch(function (err) {
            if (onError)
                onError();
        });
    };
    SensorHubService.prototype.onUpdateSensors = function (callback) {
        this.hubConnection.on("UpdateSensor", function (data) {
            callback(data);
        });
    };
    SensorHubService.prototype.close = function () {
        this.hubConnection.stop();
    };
    SensorHubService = __decorate([
        core_1.Injectable()
    ], SensorHubService);
    return SensorHubService;
}());
exports.SensorHubService = SensorHubService;
//# sourceMappingURL=sensor-hub.service.js.map