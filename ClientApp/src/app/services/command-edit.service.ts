import { Injectable } from '@angular/core';
import { CommandEditComponent } from '../components/command-edit/command-edit.component';
import { MatDialog } from '@angular/material';
import { Script } from '../interfaces/Script';

@Injectable({
  providedIn: 'root'
})
export class CommandEditService {

    constructor(private dialog: MatDialog) { }

    open(editMode: boolean, script: Script, commandId: number | null = null) {
        return this.dialog.open(CommandEditComponent, {
            disableClose: false,
            width: "600px",
            data: {
                editMode: editMode,
                id: commandId,
                script
            }
        })
    }
}
