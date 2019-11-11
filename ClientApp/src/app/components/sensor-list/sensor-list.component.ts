import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { SensorService } from "../../services/sensor.service";
import { Controller } from "../../interfaces/Controller";
import { Sensor } from "../../interfaces/Sensor";
import { SensorEditService } from "../../services/sensor-edit.service";

@Component({
    selector: "app-sensor-list",
    templateUrl: "./sensor-list.component.html",
    styleUrls: ["./sensor-list.component.less"]
})
export class SensorListComponent implements OnInit, OnChanges {

    @Input() controller: Controller;
    sensors: Sensor[];

    constructor(private sensorCervice: SensorService,
        private sensorEditServie: SensorEditService) {

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
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['controller'] !== 'undefined') {
            if (!changes['controller'].firstChange) {
                this.loadData();
            }
        }
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
