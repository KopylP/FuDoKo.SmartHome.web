import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material";
import { EditControllerComponent } from "../components/edit-controller/edit-controller.component";

@Injectable()
export class EditControllerService {
    constructor(private dialog: MatDialog) {}

    open(editMode: boolean, controllerId: number | null = null) {
        return this.dialog.open(EditControllerComponent, {
            disableClose: true,
            width: "500px",
            data: {
                editMode: editMode,
                id: controllerId
            }
        });
    }
}
