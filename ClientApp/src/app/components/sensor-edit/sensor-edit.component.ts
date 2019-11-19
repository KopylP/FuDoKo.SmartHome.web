import { Component, Inject } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Sensor } from "../../interfaces/Sensor";
import { SensorService } from "../../services/sensor.service";
import { ControllerService } from "../../services/controller.service";
import { SensorTypeService } from "../../services/sensor-type.service";
import { SensorType } from "../../interfaces/SensorType";
import { Controller } from "../../interfaces/Controller";

@Component({
    selector: "app-sensor-edit",
    templateUrl: "./sensor-edit.component.html",
    styleUrls: ["./sensor-edit.component.less"]
})
export class SensorEditComponent {

    public editMode: boolean;
    private id: number;
    public title: string;
    private controllerId: number;
    isAction = false;

    form: FormGroup;
    sensor: Sensor;
    sensorTypes: SensorType[];
    controllers: Controller[];

    constructor(private dialogRef: MatDialogRef<SensorEditComponent, Sensor | null>,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) data,
        private sensorService: SensorService,
        private sensorTypeService: SensorTypeService) {
        this.editMode = data.editMode;
        this.id = data.id;
        this.controllerId = data.controllerId;
        this.createForm();
        this.sensor = <Sensor>{};
        this.loadSensorTypes();
        if (this.editMode) {
            this.title = "Edit sensor";
        } else {
            this.title = "Add sensor";
        }
    }

    createForm() {
        this.form = this.fb.group({
            Name: ["", Validators.required],
            Pin: ["", Validators.required],
            SensorTypeId: ["", Validators.required]
        });
    }

    updateForm() {
        this.form.setValue({
            Name: this.sensor.name,
            Pin: this.sensor.pin,
            SensorTypeId: this.sensor.sensorTypeId
        });
    }

    loadSensorTypes() {
        this.sensorTypeService.all().subscribe(res => {
            this.sensorTypes = res;
            if (this.editMode)
              this.loadData();
        });
    }

    loadData() {
        console.log(this.id);
        this.sensorService.get(this.id).subscribe(res => {
            this.sensor = res;
            if (this.editMode)
              this.updateForm();
        })
    }

    close() {
        this.dialogRef.close();
    }

    getFormControl(name: string) {
        return this.form.get(name);
    }

    isValid(name: string) {
        let e = this.getFormControl(name);
        return e && e.valid;
    }

    hasError(name: string) {
        let e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    }

    onSubmit() {
        this.isAction = true;
        const sensor = <Sensor>{};
        sensor.name = this.form.value.Name;
        sensor.pin = this.form.value.Pin;
        sensor.sensorTypeId = this.form.value.SensorTypeId;
        sensor.controllerId = this.controllerId;
        if (this.editMode) {
            sensor.id = this.sensor.id;
            sensor.status = this.sensor.status;
            sensor.controllerId = this.sensor.controllerId;
            this.sensorService.post(sensor).subscribe(res => {
                this.isAction = false;
                this.dialogRef.close(res);
            }, err => {
                    this.isAction = false;
                    this.form.setErrors({
                        "edit": err.error.message
                    });
            });
        } else {
            sensor.status = true;
            this.sensorService.put(sensor).subscribe(res => {
                this.isAction = false;
                this.dialogRef.close(res);
            }, err => {
                    this.isAction = false;
                    this.form.setErrors({
                        "edit": err.error.message
                    });
            })
        }
    }
}
