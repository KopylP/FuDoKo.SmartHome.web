import { Component, Input, OnInit, ViewChild, Output, EventEmitter, OnChanges, SimpleChanges } from "@angular/core";
import { Sensor } from "../../interfaces/Sensor";
import { SensorService } from "../../services/sensor.service";
import { faThermometerHalf, faLightbulb, faTint, IconDefinition, faSun } from "@fortawesome/free-solid-svg-icons";
import { MatMenuTrigger } from "@angular/material";
import { SensorEditService } from "../../services/sensor-edit.service";


@Component({
    selector: "app-sensor-item",
    templateUrl: "./sensor-item.component.html",
    styleUrls: ["./sensor-item.component.less"]
})
export class SensorItemComponent implements OnInit {

    @Input() sensor: Sensor;
    @Input() isAdmin: boolean;
    faThermometerHalf = faThermometerHalf;
    faSun = faSun;
    faTint = faTint;
    selectedIcon: IconDefinition;
    @Output() onSensorDelete = new EventEmitter<number>();

    @ViewChild('menuTrigger', { static: true })
    trigger: MatMenuTrigger;

    constructor(private sensorService: SensorService,
        private sensorEditService: SensorEditService) { }

    ngOnInit(): void {
        switch (this.sensor.sensorType.typeName) {
            case "light":
                this.selectedIcon = faSun;
                break;
            case "submersion":
                this.selectedIcon = faTint;
                break;
            case "temperature":
                this.selectedIcon = faThermometerHalf;
                break;
        }
    }

    editSensor() {
        this.sensorEditService.open(true, this.sensor.controllerId, this.sensor.id)
            .afterClosed()
            .subscribe(res => {
                console.log(res);
                if (typeof res !== "undefined") {
                    this.sensor = res;
                }
            })
    }


    deleteSensor() {
        this.sensorService.delete(this.sensor.id).subscribe(res => {
            this.onSensorDelete.emit(this.sensor.id);
        });
    }

    toggleMenu() {
        if (this.isAdmin) {
            this.trigger.openMenu();
        }
        return false;
    }

    changeStatus() {
        this.sensorService.post(this.sensor).subscribe(res => {
            this.sensor = res;
        }, err => {
                console.log(err);
        })
    }

}
