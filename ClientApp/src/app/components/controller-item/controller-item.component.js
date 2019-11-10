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
var ControllerItemComponent = /** @class */ (function () {
    function ControllerItemComponent(controllerService, editControllerService) {
        this.controllerService = controllerService;
        this.editControllerService = editControllerService;
        this.countOfClicks = 0;
        this.onDeleteController = new core_1.EventEmitter();
        this.faHome = free_solid_svg_icons_1.faHome;
    }
    ControllerItemComponent.prototype.ngOnInit = function () {
        if (this.class === 'selected') {
            this.controllerElement.nativeElement.classList.add("div-controller-circle-background");
        }
        else {
            this.controllerElement.nativeElement.classList.add("div-controller-circle-outline");
        }
    };
    ControllerItemComponent.prototype.ngOnChanges = function (changes) {
        if (typeof changes['class'] !== 'undefined') {
            console.log("class changes");
            if (this.class === 'selected') {
                this.controllerElement.nativeElement.classList.remove("div-controller-circle-outline");
                this.controllerElement.nativeElement.classList.add("div-controller-circle-background");
            }
            else {
                this.controllerElement.nativeElement.classList.remove("div-controller-circle-background");
                this.controllerElement.nativeElement.classList.add("div-controller-circle-outline");
            }
        }
    };
    ControllerItemComponent.prototype.changeStatus = function (event) {
        var _this = this;
        var controller = this.userHasController.controller;
        this.controllerService.edit(controller).subscribe(function (res) {
            _this.userHasController.controller = res;
        }, function (err) {
            console.log("Error");
        });
    };
    ControllerItemComponent.prototype.toggleMenu = function () {
        if (this.class === "selected") {
            this.trigger.openMenu();
        }
        else {
            this.trigger.closeMenu();
        }
        return false;
    };
    ControllerItemComponent.prototype.deleteController = function () {
        var _this = this;
        this.controllerService.delete(this.userHasController.controller.id).subscribe(function (res) {
            _this.onDeleteController.emit(_this.userHasController.controller.id);
        });
    };
    ControllerItemComponent.prototype.editController = function () {
        var _this = this;
        this.editControllerService.open(true, this.userHasController.controller.id).afterClosed().subscribe(function (res) {
            if (typeof res !== 'undefined') {
                _this.userHasController.controller = res;
            }
        });
    };
    __decorate([
        core_1.Input()
    ], ControllerItemComponent.prototype, "userHasController", void 0);
    __decorate([
        core_1.Input()
    ], ControllerItemComponent.prototype, "class", void 0);
    __decorate([
        core_1.ViewChild('menuTrigger', { static: true })
    ], ControllerItemComponent.prototype, "trigger", void 0);
    __decorate([
        core_1.Output()
    ], ControllerItemComponent.prototype, "onDeleteController", void 0);
    __decorate([
        core_1.ViewChild("controllerElement", { static: true })
    ], ControllerItemComponent.prototype, "controllerElement", void 0);
    ControllerItemComponent = __decorate([
        core_1.Component({
            selector: "app-controller-item",
            templateUrl: "./controller-item.component.html",
            styleUrls: ['./controller-item.component.less']
        })
    ], ControllerItemComponent);
    return ControllerItemComponent;
}());
exports.ControllerItemComponent = ControllerItemComponent;
//# sourceMappingURL=controller-item.component.js.map