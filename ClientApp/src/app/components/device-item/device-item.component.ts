import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Device } from '../../interfaces/Device';
import { MatMenuTrigger, MatSnackBar } from '@angular/material';
import { IconDefinition, faLightbulb, faMenorah } from '@fortawesome/free-solid-svg-icons';
import { DeviceService } from '../../services/device.service';
import { EditDeviceService } from '../../services/edit-device.service';
import { DeviceAccessDialogService } from '../../services/device-access-dialog.service';

@Component({
    selector: 'app-device-item',
    templateUrl: './device-item.component.html',
    styleUrls: ['./device-item.component.less']
})
export class DeviceItemComponent implements OnInit {

    @Input() device: Device;
    @Input() isAdmin: boolean;
    @Output() onDeviceDeleted: EventEmitter<Device> = new EventEmitter <Device>();

    @ViewChild('menuTrigger', { static: true })
    trigger: MatMenuTrigger;

    selectedIcon: IconDefinition;

    faLightbulb = faLightbulb;
    faMenorah = faMenorah;


    constructor(private deviceService: DeviceService,
        private editDeviceService: EditDeviceService,
        private snackBar: MatSnackBar,
        private deviceAccessDialogService: DeviceAccessDialogService) { }

    ngOnInit() {
        switch (this.device.deviceType.typeName) {
            case "Lamp":
                this.selectedIcon = faLightbulb;
                break;
            case "LED lamp":
                this.selectedIcon = faMenorah;
                break;
            case "Switch":
                this.selectedIcon = faLightbulb;
                break;

        }
    }

    toggleMenu() {
        if (this.isAdmin) {
            this.trigger.openMenu();
        }
        return false;
    }

    changeStatus() {
        this.deviceService.post(this.device).subscribe(res => {
            this.device = res;
        }, err => {
            console.log(err);
        })
    }

    editDevice() {
        this.editDeviceService
            .open(true, this.device.controllerId, this.device.id)
            .afterClosed()
            .subscribe(res => {
                if (typeof res !== "undefined") {
                    console.log(res);
                    this.device = res;
                }
            });
    }

    accessPolicy() {
        this.deviceAccessDialogService.open(this.device.id);
    }

    deleteDevice() {
        this.deviceService
            .delete(this.device.id)
            .subscribe(res => {
                this.onDeviceDeleted.emit(this.device);
            }, err => {
                    this.openSnackBar(err.error.message, null, "snack-error-bottom");
            });
    }
    
    getClass() {
        return this.device.deviceType.typeName.toLowerCase().replace(" ", "-");
    }

    openSnackBar(message: string, action: string, snackClass: string) {
        return this.snackBar.open(message, action, {
            duration: 3000,
            verticalPosition: "bottom",
            horizontalPosition: "center",
            panelClass: [snackClass]
        });
    }

}
