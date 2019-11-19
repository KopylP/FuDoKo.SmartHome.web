import { DeviceType } from "./DeviceType";

export interface Device {
    id: number;
    name: string;
    pin: number;
    mac: string;
    status: boolean;
    controllerId: number;
    deviceTypeId: number;
    deviceType: DeviceType;
}
