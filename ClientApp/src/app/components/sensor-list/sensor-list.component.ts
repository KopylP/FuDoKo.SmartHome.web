import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter, OnDestroy } from "@angular/core";
import { SensorService } from "../../services/sensor.service";
import { Controller } from "../../interfaces/Controller";
import { Sensor } from "../../interfaces/Sensor";
import { SensorEditService } from "../../services/sensor-edit.service";
import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel } from '@aspnet/signalr';
import { AuthService } from "../../services/auth.service";
import { Observable } from "rxjs";
import { MatSnackBar } from "@angular/material";
import { SensorHubService } from "../../services/sensor-hub.service";
@Component({
    selector: "app-sensor-list",
    templateUrl: "./sensor-list.component.html",
    styleUrls: ["./sensor-list.component.less"]
})
export class SensorListComponent implements OnInit, OnChanges, OnDestroy {

    @Input() controller: Controller;
    sensors: Sensor[];

    private hubConnection: HubConnection;

    constructor(private sensorCervice: SensorService,
        private sensorEditServie: SensorEditService,
        private authService: AuthService,
        private snackBar: MatSnackBar,
        private sensorHubService: SensorHubService) {

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
        
        this.sensorHubService.init(null, () => {
            this.openSnackBar("Ooops. Real time information from sensors is is not available.", "Refresh", "snack-error")
                .subscribe(() => this.initHub());
        });
        //listen to edit value of sensor event
        this.sensorHubService.onUpdateSensors(data => {
            const index = this.sensors.findIndex(p => p.id == data.id);
            if (index !== -1)
                this.sensors.splice(index, 1, data);
        });
    }

    openSnackBar(message: string, action: string, snackClass: string): Observable<void> {
        return this.snackBar.open(message, action, {
            duration: 5000,
            verticalPosition: "top",
            horizontalPosition: "right",
            panelClass: [snackClass]
        }).onAction();
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

    ngOnDestroy(): void {
        this.sensorHubService.close();
    }

}
