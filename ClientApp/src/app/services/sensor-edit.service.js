"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var sensor_edit_component_1 = require("../components/sensor-edit/sensor-edit.component");
var SensorEditService = /** @class */ (function () {
    function SensorEditService(dialog) {
        this.dialog = dialog;
    }
    SensorEditService.prototype.open = function (editMode, controllerId, sensorId) {
        if (sensorId === void 0) { sensorId = null; }
        return this.dialog.open(sensor_edit_component_1.SensorEditComponent, {
            disableClose: true,
            width: "500px",
            data: {
                editMode: editMode,
                id: sensorId,
                controllerId: controllerId
            }
        });
    };
    SensorEditService = __decorate([
        core_1.Injectable()
    ], SensorEditService);
    return SensorEditService;
}());
exports.SensorEditService = SensorEditService;
//# sourceMappingURL=sensor-edit.service.js.map