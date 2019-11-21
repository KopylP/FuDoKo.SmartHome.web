import { Injectable } from '@angular/core';
import { ControllerAccessComponent } from '../components/controller-access/controller-access.component';
import { MatDialog } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class ControllerAccessService {
    constructor(private dialog: MatDialog) { }

    open(controllerId: number) {
        return this.dialog.open(ControllerAccessComponent, {
            disableClose: true,
            width: "800px",
            data: {
                controllerId: controllerId
            }
        });
    }
}
