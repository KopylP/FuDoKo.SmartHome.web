import { Injectable } from '@angular/core';
import { ScriptEditComponent } from '../components/script-edit/script-edit.component';
import { MatDialog } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class ScriptEditService {

    constructor(private dialog: MatDialog) { }

    open(editMode: boolean, controllerId: number, scriptId: number | null = null) {
        return this.dialog.open(ScriptEditComponent, {
            disableClose: true,
            width: "650px",
            data: {
                editMode: editMode,
                id: scriptId,
                controllerId: controllerId
            }
        })
    }
}
