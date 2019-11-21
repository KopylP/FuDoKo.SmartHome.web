"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var AccessControllerService = /** @class */ (function () {
    function AccessControllerService(http) {
        this.http = http;
        this.url = "api/Access/Controller/";
    }
    AccessControllerService.prototype.all = function (controllerId) {
        return this.http.get(this.url + controllerId);
    };
    AccessControllerService.prototype.put = function (controllerAccess) {
        return this.http.put(this.url, controllerAccess);
    };
    AccessControllerService.prototype.delete = function (userHasControllerId) {
        return this.http.delete(this.url + userHasControllerId);
    };
    AccessControllerService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], AccessControllerService);
    return AccessControllerService;
}());
exports.AccessControllerService = AccessControllerService;
//# sourceMappingURL=access-controller.service.js.map