import { Injectable } from '@angular/core';
import { ControllerAccessComponent } from '../components/controller-access/controller-access.component';
import { MatDialog } from '@angular/material';
import { DeviceAccessComponent } from '../components/device-access/device-access.component';

@Injectable({
  providedIn: 'root'
})
export class DeviceAccessDialogService {
    constructor(private dialog: MatDialog) { }

    open(deviceId: number) {
        return this.dialog.open(DeviceAccessComponent, {
            disableClose: true,
            width: "800px",
            data: {
                deviceId: deviceId
            }
        });
    }
}
