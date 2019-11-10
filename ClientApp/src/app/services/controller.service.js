"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ControllerService = /** @class */ (function () {
    function ControllerService(http) {
        this.http = http;
        this.url = "api/controller";
    }
    ControllerService.prototype.getAll = function () {
        return this.http.get(this.url);
    };
    ControllerService.prototype.get = function (id) {
        return this.http.get(this.url + '/' + id);
    };
    ControllerService.prototype.delete = function (id) {
        return this.http.delete(this.url + '/' + id);
    };
    ControllerService.prototype.edit = function (controller) {
        return this.http.post(this.url, controller);
    };
    ControllerService.prototype.put = function (controller) {
        return this.http.put(this.url, controller);
    };
    ControllerService = __decorate([
        core_1.Injectable()
    ], ControllerService);
    return ControllerService;
}());
exports.ControllerService = ControllerService;
//# sourceMappingURL=controller.service.js.map