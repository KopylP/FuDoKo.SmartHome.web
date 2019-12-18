import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../interfaces/Device';
import { Controller } from '../../interfaces/Controller';
import { EditDeviceService } from '../../services/edit-device.service';

@Component({
    selector: 'app-device-list',
    templateUrl: './device-list.component.html',
    styleUrls: ['./device-list.component.less']
})
export class DeviceListComponent implements OnInit, OnChanges {

    constructor(private deviceService: DeviceService,
        private editDeviceService: EditDeviceService) { }

    @Input() controller: Controller;
    @Input() isAdmin: boolean;
    devices: Device[];

    ngOnInit() {
        console.log("Device List");
        this.loadData();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['controller'] !== 'undefined') {
            if (!changes['controller'].firstChange) {
                this.loadData();
            }
        }
    }

    loadData() {
        this.deviceService.all(this.controller.id).subscribe(res => {
            this.devices = res;
            console.log(res);
        }, err => {
            console.log(err);
        });
    }

    addDevice() {
        this.editDeviceService.open(false, this.controller.id)
            .afterClosed()
            .subscribe(res => {
                if (typeof res !== "undefined")
                  this.devices.push(res);
        });
    }

    onDeviceDeleted(device: Device) {
        const index = this.devices.findIndex(p => p.id === device.id);
        if (index !== -1) {
            this.devices.splice(index, 1);
        }
    }
}
