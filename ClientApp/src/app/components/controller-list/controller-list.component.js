"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ControllerListComponent = /** @class */ (function () {
    function ControllerListComponent(controllerService, editControllerService) {
        this.controllerService = controllerService;
        this.editControllerService = editControllerService;
        this.onChanged = new core_1.EventEmitter();
        this.userHasControllers = [];
    }
    ControllerListComponent.prototype.setSelectedItem = function (item) {
        this.selectedItem = item;
        this.onChanged.emit(item);
    };
    ControllerListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.controllerService.getAll().subscribe(function (res) {
            console.log(res);
            _this.userHasControllers = res;
        }, function (err) {
            console.log(err);
        });
    };
    ControllerListComponent.prototype.addController = function () {
        var _this = this;
        console.log("click");
        this.editControllerService.open(false).afterClosed().subscribe(function (res) {
            if (typeof res !== 'undefined') {
                _this.userHasControllers.push({
                    isAdmin: true,
                    controller: res
                });
            }
        });
    };
    ControllerListComponent.prototype.deleteController = function (id) {
        var userHasController = this.userHasControllers.filter(function (p) { return p.controller.id === id; })[0];
        var index = this.userHasControllers.indexOf(userHasController);
        this.userHasControllers.splice(index, 1);
    };
    __decorate([
        core_1.Output()
    ], ControllerListComponent.prototype, "onChanged", void 0);
    ControllerListComponent = __decorate([
        core_1.Component({
            selector: "app-controller-list",
            templateUrl: "./controller-list.component.html",
            styleUrls: ['./controller-list.component.less']
        })
    ], ControllerListComponent);
    return ControllerListComponent;
}());
exports.ControllerListComponent = ControllerListComponent;
//# sourceMappingURL=controller-list.component.js.map