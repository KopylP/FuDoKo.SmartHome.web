import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Device } from '../../interfaces/Device';
import { DeviceService } from '../../services/device.service';
import { DeviceTypeService } from '../../services/device-type.service';
import { DeviceType } from '../../interfaces/DeviceType';

@Component({
    selector: 'app-edit-device',
    templateUrl: './edit-device.component.html',
    styleUrls: ['./edit-device.component.less']
})
export class EditDeviceComponent implements OnInit {

    public editMode: boolean;
    private id: number;
    public title: string;
    private controllerId: number;
    isAction = false;

    device: Device;

    deviceTypes: DeviceType[];

    form: FormGroup;

    constructor(private dialogRef: MatDialogRef<EditDeviceComponent, Device | null>,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) data,
        private deviceService: DeviceService,
        private deviceTypeService: DeviceTypeService) {
        this.editMode = data.editMode;
        this.id = data.id;
        this.controllerId = data.controllerId;
        this.createForm();
        this.loadDeviceTypes();
        this.device = <Device>{};

        if (this.editMode) {
            this.title = "Edit device";
        } else {
            this.title = "Add device"
        }

    }

    createForm() {
        this.form = this.fb.group({
            Name: ["", Validators.required],
            ConnectionType: ["pin", Validators.required],
            Connection: ["", Validators.required],
            DeviceTypeId: ["", Validators.required]
        });
    }

    updateForm() {
        this.form.setValue({
            Name: this.device.name,
            ConnectionType: this.device.pin == 0 ? "MAC": "pin",
            Connection: this.device.pin == 0 ? this.device.mac : this.device.pin,
            DeviceTypeId: this.device.deviceTypeId
        });
    }

    loadData() {
        this.deviceService.get(this.id).subscribe(res => {
            this.device = res;
            this.updateForm();
        }, err => {

        });
    }

    close() {
        this.dialogRef.close();
    }

    loadDeviceTypes() {
        this.deviceTypeService.all().subscribe(res => {
            this.deviceTypes = res;
        }, err => {

        });
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

    ngOnInit() {
        if (this.editMode) {
            this.loadData();
        }
    }

    onSubmit() {
        this.isAction = true;
        const device = <Device>{};
        device.pin = 0;
        device.name = this.form.value.Name;
        if (this.form.value.ConnectionType == "pin")
            device.pin = this.form.value.Connection;
        else
            device.mac = this.form.value.Connection;

        device.controllerId = this.controllerId;
        device.deviceTypeId = this.form.value.DeviceTypeId;
        device.status = true;
        if (this.editMode) {
            device.id = this.device.id;
            device.status = this.device.status;
            device.controllerId = this.device.controllerId;
            this.deviceService.post(device).subscribe(res => {
                this.isAction = false;
                this.dialogRef.close(res);
            }, err => {
                this.isAction = false;
                this.form.setErrors({
                    "edit": err.error.message
                });
            });
        } else {
            this.deviceService.put(device).subscribe(res => {
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
