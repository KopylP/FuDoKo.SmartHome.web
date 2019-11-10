"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var edit_controller_component_1 = require("../components/edit-controller/edit-controller.component");
var EditControllerService = /** @class */ (function () {
    function EditControllerService(dialog) {
        this.dialog = dialog;
    }
    EditControllerService.prototype.open = function (editMode, controllerId) {
        if (controllerId === void 0) { controllerId = null; }
        return this.dialog.open(edit_controller_component_1.EditControllerComponent, {
            disableClose: true,
            width: "500px",
            data: {
                editMode: editMode,
                id: controllerId
            }
        });
    };
    EditControllerService = __decorate([
        core_1.Injectable()
    ], EditControllerService);
    return EditControllerService;
}());
exports.EditControllerService = EditControllerService;
//# sourceMappingURL=edit-controller.service.js.map