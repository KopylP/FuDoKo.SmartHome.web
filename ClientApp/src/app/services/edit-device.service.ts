import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { EditDeviceComponent } from '../components/edit-device/edit-device.component';

@Injectable({
  providedIn: 'root'
})
export class EditDeviceService {

    constructor(private dialog: MatDialog) { }

    open(editMode: boolean, controllerId: number, deviceId: number | null = null) {
        return this.dialog.open(EditDeviceComponent, {
            disableClose: true,
            width: "500px",
            data: {
                editMode: editMode,
                id: deviceId,
                controllerId: controllerId
            }
        })
    }
}
