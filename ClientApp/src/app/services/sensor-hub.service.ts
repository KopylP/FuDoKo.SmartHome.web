import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder, LogLevel, HttpTransportType } from "@aspnet/signalr";
import { AuthService } from "./auth.service";
import { Sensor } from "../interfaces/Sensor";

@Injectable()
export class SensorHubService {

    hubConnection: HubConnection;

    constructor(private authService: AuthService) {
        this.hubConnection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Debug)
            .withUrl('/real/sensors', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                accessTokenFactory: () => this.authService.getAuth().token
            })
            .build();
    }

    init(onSuccess: () => void | null, onError: () => void | null = null) {
        this.hubConnection
            .start()
            .then(p => {
                if (onSuccess)
                  onSuccess();
            })
            .catch(err => {
                if (onError)
                  onError();
            });
    }

    onUpdateSensors(callback: (sensor: Sensor) => void) {
        this.hubConnection.on("UpdateSensor", data => {
            callback(data);
        });
    }
}
