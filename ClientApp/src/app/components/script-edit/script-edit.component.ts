import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Script } from '../../interfaces/Script';
import { SensorService } from '../../services/sensor.service';
import { Sensor } from '../../interfaces/Sensor';
import { ConditionTypeService } from '../../services/condition-type.service';
import { ConditionType } from '../../interfaces/ConditionType';
import * as moment from 'moment';
import { isEmpty } from 'rxjs/operators';
import { ScriptService } from '../../services/script.service';

@Component({
    selector: 'app-script-edit',
    templateUrl: './script-edit.component.html',
    styleUrls: ['./script-edit.component.less']
})
export class ScriptEditComponent implements OnInit {

    public editMode: boolean;
    private id: number;
    public title: string;
    private controllerId: number;
    isAction = false;
    isOwn = false;
    repeatTimeValue: string = "one";
    private script: Script;
    form: FormGroup;

    dateNow = new Date();
    dateFrom: Date;
    dateTo: Date;

    useSensor = false;
    withDateTo = false;

    sensors: Sensor[];
    conditionTypes: ConditionType[];

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<ScriptEditComponent, Script | null>,
        @Inject(MAT_DIALOG_DATA) data,
        private sensorService: SensorService,
        private conditionTypeService: ConditionTypeService,
        private scriptService: ScriptService) {

        this.editMode = data.editMode;
        this.id = data.id;
        this.controllerId = data.controllerId;
        this.createForm();

        this.script = <Script>{};

        this.createForm();
        this.loadSensors();
        this.loadConditionTypes();
        if (this.editMode) {
            this.title = "Edit script";
        } else {
            this.title = "Create script";
        }
    }

    ngOnInit() {

    }


    createForm() {
        this.dateFrom = this.dateNow;
        this.dateTo = moment(this.dateFrom).add(1, "hours").toDate();
        this.form = this.fb.group({
            Name: ["", Validators.required],
            SensorId: "",
            ConditionTypeId: "",
            ConditionValue: "",
            TimeFrom: [moment(this.dateFrom).format("HH:mm"), Validators.required],
            TimeTo: [moment(this.dateTo).format("HH:mm")],
            DateFrom: [this.dateFrom, Validators.required],
            DateTo: this.dateTo,
            RepeatTimes: [1, Validators.required],
            Priority: [4, Validators.required]
        });
    }

    onDateFormChange() {
        console.log("Date Change");
        this.dateFrom = this.form.value.DateFrom;
    }

    onSubmit() {
        this.isAction = true;
        const scriptModel = <Script>{};
        scriptModel.name = this.form.value.Name;
        if (this.useSensor) {
            scriptModel.sensorId = this.form.value.SensorId;
            scriptModel.ConditionTypeId = this.form.value.ConditionTypeId;
            scriptModel.conditionValue = this.form.value.ConditionValue;
        } else {
            scriptModel.sensorId = null;
            scriptModel.ConditionTypeId = null;
            scriptModel.conditionValue = null;
        }
        const timeFrom: Date = this.form.value.DateFrom;
        let [timeFromHours, timeFromMinutes] = this.form.value.TimeFrom.split(":");
        timeFrom.setHours(timeFromHours);
        timeFrom.setMinutes(timeFromMinutes);

        scriptModel.timeFrom = timeFrom;
        if (this.withDateTo) {
            const timeTo: Date = this.form.value.DateTo;
            let [timeToHours, timeToMinutes] = this.form.value.TimeTo.split(":");
            timeTo.setHours(timeToHours);
            timeTo.setMinutes(timeToMinutes);
            scriptModel.timeTo = timeTo;
        } else {
            scriptModel.timeTo = null;
        }
        scriptModel.repeatTimes = this.form.value.RepeatTimes;
        scriptModel.priority = this.form.value.Priority;
        scriptModel.delta = 0.5;
        scriptModel.status = false;
        scriptModel.controllerId = this.controllerId;
        console.log(scriptModel);
        if (!this.editMode) {
            this.scriptService.put(scriptModel).subscribe(res => {
                this.isAction = false;
                this.dialogRef.close(res);
            }, err => {
                    this.form.setErrors({
                        edit: err.error.message
                    })
            });
        }
    }

    updateForm() {
        this.form.setValue({
            Name: this.script.name,
        });
    }

    loadData() {

    }

    onRepeatTimeChange() {
        let i = 1;
        this.isOwn = false;
        switch (this.repeatTimeValue) {
            case "one":
                i = 1;
                break;
            case "two":
                i = 2;
                break;
            case "five":
                i = 5;
                break;
            case "infinity":
                i = -1;
                break;
            case "own":
                i = 0;
                this.isOwn = true;
                break;
        }
        this.form.get("RepeatTimes").setValue(i);
    }

    loadSensors() {
        this.sensorService.all(this.controllerId).subscribe(res => {
            this.sensors = res.filter(p => p.status);
        });
    }

    loadConditionTypes() {
        this.conditionTypeService.get().subscribe(res => {
            this.conditionTypes = res;
        });
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
}
