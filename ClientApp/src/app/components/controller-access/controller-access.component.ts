import { Component, OnInit, Inject } from '@angular/core';
import { UserHasController } from '../../interfaces/UserHasController';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { AccessControllerService } from '../../services/access-controller.service';
import { faTimesCircle } from '@fortawesome/free-solid-svg-icons';
import { ControllerAccess } from '../../interfaces/ControllerAccess';
import { Observable } from 'rxjs';
@Component({
    selector: 'app-controller-access',
    templateUrl: './controller-access.component.html',
    styleUrls: ['./controller-access.component.less']
})
export class ControllerAccessComponent implements OnInit {


    faTimesCircle = faTimesCircle;
    usersHasControllers: UserHasController[] = [];
    title = "Access Policy";
    visible = true;
    selectable = true;
    removable = true;
    addOnBlur = true;
    readonly separatorKeysCodes: number[] = [ENTER, COMMA];

    controllerId: number;

    constructor(
        private dialogRef: MatDialogRef<ControllerAccessComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private accessControllerService: AccessControllerService,
        private snackBar: MatSnackBar
    ) {
        this.controllerId = data.controllerId;
        this.loadData();
    }

    remove(userHasController: UserHasController) {
        const index = this.usersHasControllers.indexOf(userHasController);
        if (index >= 0) {
            this.openSnackBarWithDelete(userHasController, index);
            this.usersHasControllers.splice(index, 1);
        }
        
    }

    add(event: MatChipInputEvent): void {
        const input = event.input;
        const value = event.value;

        if ((value || '').trim()) {
            const controllerAccess = <ControllerAccess>{};
            controllerAccess.controllerId = this.controllerId;
            controllerAccess.userName = value;
            this.accessControllerService.put(controllerAccess)
                .subscribe(res => {
                    this.usersHasControllers.push(res);
                }, err => {
                        this.openSnackBar(err.error.message, "Close", "snack-error-bottom");
                });
        }

        // Reset the input value
        if (input) {
            input.value = '';
        }
    }

    loadData() {
        this.accessControllerService.all(this.controllerId).subscribe(res => {
            console.log(res);
            this.usersHasControllers = res;
        }, error => {
                console.log(error);
        });
    }

    openSnackBar(message: string, action: string, snackClass: string) {
        return this.snackBar.open(message, action, {
            duration: 3000,
            verticalPosition: "bottom",
            horizontalPosition: "center",
            panelClass: [snackClass]
        });
    }


    openSnackBarWithDelete(userHasController: UserHasController, index: number) {
        const snackBar = this.openSnackBar(`User ${userHasController.user.name} hasn\`t access to your controller.`, "UNDO", "snack-primary-bottom");
        snackBar.afterDismissed().subscribe(res => {
            
            if (!res.dismissedByAction) {
                this.accessControllerService.delete(userHasController.id).subscribe(res => {

                }, err => {
                    console.log(err);
                });
            }
        });
        snackBar.onAction().subscribe(p => {
            this.usersHasControllers.splice(index, 0, userHasController);
        });
    }

    close() {
        this.dialogRef.close();
    }

    ngOnInit() {
    }

}
