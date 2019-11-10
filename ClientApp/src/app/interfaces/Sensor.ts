import { SensorType } from "./SensorType";

export interface Sensor {
    id: number;
    name: string;
    pin: number;
    status: boolean;
    value: number;
    sensorTypeId: number;
    controllerId: number;
    sensorType: SensorType;
}
