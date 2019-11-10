import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material";
import { SensorEditComponent } from "../components/sensor-edit/sensor-edit.component";

@Injectable()
export class SensorEditService {

    constructor(private dialog: MatDialog) { }

    open(editMode: boolean, controllerId: number,  sensorId: number | null = null) {
        return this.dialog.open(SensorEditComponent, {
            disableClose: true,
            width: "500px",
            data: {
                editMode: editMode,
                id: sensorId,
                controllerId: controllerId
            }
        })
    }
}
