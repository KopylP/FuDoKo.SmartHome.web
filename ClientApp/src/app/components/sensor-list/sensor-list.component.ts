import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { SensorService } from "../../services/sensor.service";
import { Controller } from "../../interfaces/Controller";
import { Sensor } from "../../interfaces/Sensor";
import { SensorEditService } from "../../services/sensor-edit.service";
import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel } from '@aspnet/signalr';
import { AuthService } from "../../services/auth.service";
@Component({
    selector: "app-sensor-list",
    templateUrl: "./sensor-list.component.html",
    styleUrls: ["./sensor-list.component.less"]
})
export class SensorListComponent implements OnInit, OnChanges {

    @Input() controller: Controller;
    sensors: Sensor[];

    private hubConnection: HubConnection;

    constructor(private sensorCervice: SensorService,
        private sensorEditServie: SensorEditService,
        private authService: AuthService) {

    }

    loadData() {
        this.sensorCervice.all(this.controller.id).subscribe(res => {
            this.sensors = res;
            console.log(res);
        }, err => {
                console.log(err);
        });    
    }

    ngOnInit(): void {
        this.loadData();
        this.initHub();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['controller'] !== 'undefined') {
            if (!changes['controller'].firstChange) {
                this.loadData();
            }
        }
    }

    initHub() {
        this.hubConnection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Debug)
            .withUrl('/real/sensors', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                accessTokenFactory: () => this.authService.getAuth().token
            })
            .build();
        this.hubConnection
            .start()
            .then(p => {
                console.log("Server is started");
            })
            .catch(err => {
                console.log(err);
            });

        this.hubConnection.on("UpdateSensor", data => {
            const index = this.sensors.findIndex(p => p.id == data.id);
            this.sensors.splice(index, 1, data);
        });
    }

    addSensor() {
        this.sensorEditServie
            .open(false, this.controller.id)
            .afterClosed()
            .subscribe(res => {
                if (typeof res !== "undefined") {
                    this.sensors.push(res);
                }
            });
    }

    deleteSensor(id: number) {
        const index = this.sensors.findIndex(p => p.id === id);
        this.sensors.splice(index, 1);
    }

}
