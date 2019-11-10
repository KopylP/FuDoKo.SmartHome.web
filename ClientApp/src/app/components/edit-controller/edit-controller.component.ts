import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ControllerService } from "../../services/controller.service";
import { Controller } from "../../interfaces/Controller"
@Component({
    selector: 'app-edit-controller',
    templateUrl: './edit-controller.component.html',
    styleUrls: ['./edit-controller.component.less']
})
export class EditControllerComponent {
    private editMode: boolean;
    private id: number;
    public title: string;
    isAction = false;

    form: FormGroup;
    private controller: Controller;
    constructor(private dialogRef: MatDialogRef<EditControllerComponent>,
        private fb: FormBuilder,
        private controllerService: ControllerService,
        @Inject(MAT_DIALOG_DATA) data) {
        this.editMode = data.editMode;
        this.id = data.id;
        this.createForm();
        this.controller = <Controller>{};
        if (this.editMode) {
            this.loadData();
            this.title = "Edit controller";
        } else {
            this.title = "Create controller";
        }
    }

    createForm() {
        this.form = this.fb.group({
            Name: ["", Validators.required],
            Mac: ["", Validators.required],
        });
    }

    loadData() {
        
        this.controllerService.get(this.id).subscribe(res => {
            this.controller = res;
            this.updateForm();
        }, err => {

        });
    }

    updateForm() {
        this.form.setValue({
            Name: this.controller.name,
            Mac: this.controller.mac
        });
    }

    onSubmit() {
        this.isAction = true;
        console.log("On submit");
        const submitController = <Controller>{};
        submitController.mac = this.form.value.Mac;

        submitController.name = this.form.value.Name;

        if (this.editMode) {
            submitController.id = this.controller.id;
            submitController.status = this.controller.status;
            this.controllerService.edit(submitController).subscribe(res => {
                this.dialogRef.close(res);
                this.isAction = false;
            }, err => {
                    console.log(err);
                    this.form.setErrors({
                        "edit": err
                    });
                    this.isAction = false;
            });
        } else {
            this.controllerService.put(submitController).subscribe(res => {
                this.dialogRef.close(res);
                this.isAction = false;
            }, err => {
                    console.log(err);
                    this.form.setErrors({
                        "edit": err.error.message
                    });
                    this.isAction = false;
            });
        }
    }

    close() {
        this.dialogRef.close();
    }

    getFormControl(name: string) {
        return this.form.get(name);
    }

    isValid(name: string) {
        let e = this.getFormControl(name);
        return e && e.valid;
    }

    hasError(name: string) {
        let e = this.getFormControl(name);
        return e && (e.dirty || e.touched) && !e.valid;
    }
}
