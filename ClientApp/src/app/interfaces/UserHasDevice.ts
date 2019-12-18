import { UserHasController } from "./UserHasController";
import { Device } from "./Device";

export interface UserHasDevice {
    id: number;
    userHaveControllerId: number;
    deviceId: number;
    userHasController: UserHasController;
    device: Device;
}
