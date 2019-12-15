import { DeviceConfiguration } from "./DeviceConfiguration";

export interface Command {
    id: number;
    scriptId: number;
    timeSpan: string;
    deviceConfigurationId: number;
    name: string;
    end: boolean;
    deviceConfiguration: DeviceConfiguration;
}
