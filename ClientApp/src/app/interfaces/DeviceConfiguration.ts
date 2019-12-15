import { Device } from "./Device";
import { Measure } from "./Measure";

export interface DeviceConfiguration {
    id: number;
    value: number;
    deviceId: number;
    measureId: number;
    device: Device;
    measure: Measure;
}
