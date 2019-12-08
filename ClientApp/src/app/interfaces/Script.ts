import { Sensor } from "./Sensor";
import { ConditionType } from "./ConditionType";

export interface Script {
    id: number;//+
    conditionValue?: number;//+
    name: string;//+
    ConditionTypeId?: number;//+
    sensorId?: number;//+
    controllerId: number;//+
    status: boolean;//+
    timeFrom: Date;//+
    timeTo?: Date;//+
    delta?: number;//+
    repeatTimes: number;//
    priority: number;//
    sensor: Sensor;
    conditionType: ConditionType; 
}
