import { Component, OnInit, Inject } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { faTimesCircle } from '@fortawesome/free-solid-svg-icons';
import { UserHasDevice } from '../../interfaces/UserHasDevice';
import { DeviceAccessService } from '../../services/device-access.service';
import { DeviceAccess } from '../../interfaces/DeviceAccess';
@Component({
    selector: 'app-device-access',
    templateUrl: './device-access.component.html',
    styleUrls: ['./device-access.component.less']
})
export class DeviceAccessComponent implements OnInit {


    faTimesCircle = faTimesCircle;
    usersHasDevice: UserHasDevice[] = [];
    title = "Access Policy";
    visible = true;
    selectable = true;
    removable = true;
    addOnBlur = true;
    readonly separatorKeysCodes: number[] = [ENTER, COMMA];

    deviceId: number;

    constructor(
        private dialogRef: MatDialogRef<DeviceAccessComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private deviceAccessService: DeviceAccessService,
        private snackBar: MatSnackBar
    ) {
        this.deviceId = data.deviceId;
        this.loadData();
    }

    remove(userHasDevice: UserHasDevice) {
        const index = this.usersHasDevice.indexOf(userHasDevice);
        if (index >= 0) {
            this.openSnackBarWithDelete(userHasDevice, index);
            this.usersHasDevice.splice(index, 1);
        }
    }

    add(event: MatChipInputEvent): void {
        const input = event.input;
        const value = event.value;

        if ((value || '').trim()) {
            const deviceAccess = <DeviceAccess>{};
            deviceAccess.deviceId = this.deviceId;
            deviceAccess.userName = value;
            this.deviceAccessService.put(deviceAccess)
                .subscribe(res => {
                    this.usersHasDevice.push(res);
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
        this.deviceAccessService.getUsers(this.deviceId).subscribe(res => {
            console.log(res);
            this.usersHasDevice = res;
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


    openSnackBarWithDelete(userHasDevice: UserHasDevice, index: number) {
        const snackBar = this.openSnackBar(`User ${userHasDevice.userHasController.user} hasn\`t access to your controller.`, "UNDO", "snack-primary-bottom");
        snackBar.afterDismissed().subscribe(res => {
            
            if (!res.dismissedByAction) {
                this.deviceAccessService.delete(userHasDevice.id).subscribe(res => {

                }, err => {
                    console.log(err);
                });
            }
        });
        snackBar.onAction().subscribe(p => {
            this.usersHasDevice.splice(index, 0, userHasDevice);
        });
    }

    close() {
        this.dialogRef.close();
    }

    ngOnInit() {
    }

}
