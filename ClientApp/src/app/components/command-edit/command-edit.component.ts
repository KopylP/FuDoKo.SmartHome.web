import { Component, OnInit, Inject } from '@angular/core';
import { Command } from '../../interfaces/Command';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { faPenNib } from '@fortawesome/free-solid-svg-icons';
import { Script } from '../../interfaces/Script';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../interfaces/Device';
import { MeasureService } from '../../services/measure.service';
import { Measure } from '../../interfaces/Measure';
import { DeviceConfigurationService } from '../../services/device-configuration.service';
import { DeviceConfiguration } from '../../interfaces/DeviceConfiguration';
import { CommandService } from '../../services/command.service';

@Component({
    selector: 'app-command-edit',
    templateUrl: './command-edit.component.html',
    styleUrls: ['./command-edit.component.less']
})
export class CommandEditComponent implements OnInit {

    public editMode: boolean;
    private id: number;
    public title: string;
    private script: Script;
    isAction = false;
    private command: Command;

    editIcon = faPenNib;

    devices: Device[];
    measures: Measure[];

    private selectedDevice: Device;

    deviceFormGroup: FormGroup;
    measureFormGroup: FormGroup;
    commandFormGroup: FormGroup;


    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<CommandEditComponent, Command | null>,
        @Inject(MAT_DIALOG_DATA) data,
        private deviceService: DeviceService,
        private measureService: MeasureService,
        private deviceConfigurationService: DeviceConfigurationService,
        private commandService: CommandService) {
        this.editMode = data.editMode;
        this.id = data.id;
        this.script = data.script;
        this.loadDevices();
        if (this.editMode) {
            this.title = "Edit command";
        } else {
            this.title = "Create new command";
        }
    }

    ngOnInit() {
        this.deviceFormGroup = this.fb.group({
            DeviceId: ['', Validators.required]
        });
        this.measureFormGroup = this.fb.group({
            MeasureId: ['', Validators.required],
            Value: ['0', [Validators.required, Validators.max(255), Validators.min(0)]]
        });
        this.commandFormGroup = this.fb.group({
            Name: ["", Validators.required],
            End: [false, Validators.required],
            TimeSpan: ['0', [Validators.required, Validators.max(600), Validators.min(0)]]
        });
    }

    loadDevices() {
        this.deviceService.all(this.script.controllerId, true).subscribe(res => {
            this.devices = res;
        });
    }
    onStepChange(event: any) {
        if (event.selectedIndex === 1) {
            const deviceId = +this.deviceFormGroup.value.DeviceId;
            this.selectedDevice = this.devices.find(p => p.id == deviceId);
            this.measureService.all(this.selectedDevice.deviceTypeId).subscribe(res => {
                this.measures = res;
            });
        }
    }

    onSubmit() {
        const deviceConfiguration = <DeviceConfiguration>{};
        deviceConfiguration.deviceId = this.deviceFormGroup.value.DeviceId;
        deviceConfiguration.measureId = this.measureFormGroup.value.MeasureId;
        deviceConfiguration.value = this.measureFormGroup.value.Value;
        if (!this.editMode) {
            this.deviceConfigurationService.put(deviceConfiguration).subscribe(res => {
                const command = <Command>{};
                command.deviceConfigurationId = res.id;
                command.end = this.commandFormGroup.value.End;
                command.name = this.commandFormGroup.value.Name;
                const seconds = this.commandFormGroup.value.TimeSpan;
                const minutes = Math.round(seconds / 60);
                const secondsWithoutMinutes = seconds % 60;
                const timeSpan = `00:${minutes}:${secondsWithoutMinutes}`;
                command.timeSpan = timeSpan;
                command.scriptId = this.script.id;
                this.commandService.put(command).subscribe(res => {
                    this.dialogRef.close(res);
                });
            });
        }
    }
}
