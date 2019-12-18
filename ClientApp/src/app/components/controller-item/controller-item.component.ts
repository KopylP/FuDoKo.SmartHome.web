import { Component, Input, OnInit, ContentChild, ElementRef, ViewChild, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { ControllerService } from "../../services/controller.service";
import { MatMenuTrigger, MatSnackBar } from "@angular/material";
import { EditControllerService } from "../../services/edit-controller.service";
import { UserHasController } from "../../interfaces/UserHasController";
import { ControllerAccessService } from "../../services/controller-access.service";


@Component({
    selector: "app-controller-item",
    templateUrl: "./controller-item.component.html",
    styleUrls: ['./controller-item.component.less']
})
export class ControllerItemComponent implements OnInit, OnChanges {

    @Input() userHasController: UserHasController;
    @Input() class: string;
    @ViewChild('menuTrigger', { static: true })
    trigger: MatMenuTrigger;

    private countOfClicks = 0;

    @Output() onDeleteController = new EventEmitter<number>(); 

    @ViewChild("controllerElement", { static:true })
    controllerElement: ElementRef;

    faHome = faHome;
    constructor(
        private controllerService: ControllerService,
        private editControllerService: EditControllerService,
        private controllerAccessService: ControllerAccessService,
        private snackBar: MatSnackBar
    ) {
    }

    ngOnInit(): void {
        if (this.class === 'selected') {
            this.controllerElement.nativeElement.classList.add("div-controller-circle-background");
        } else {
            this.controllerElement.nativeElement.classList.add("div-controller-circle-outline");
        }
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['class'] !== 'undefined') {
            console.log("class changes");
            if (this.class === 'selected') {
                this.controllerElement.nativeElement.classList.remove("div-controller-circle-outline");
                this.controllerElement.nativeElement.classList.add("div-controller-circle-background");
            } else {
                this.controllerElement.nativeElement.classList.remove("div-controller-circle-background");
                this.controllerElement.nativeElement.classList.add("div-controller-circle-outline");
            }
        }
    }

    changeStatus(event) {
        const controller = this.userHasController.controller;
        this.controllerService.edit(controller).subscribe(res => {
            this.userHasController.controller = res;
        }, err => {
                console.log("Error");
        });
    }

    toggleMenu() {
        if (this.class === "selected" && this.userHasController.isAdmin) {
            this.trigger.openMenu();
        } else {
            this.trigger.closeMenu();
        }
        return false;
    }

    deleteController() {
        this.controllerService.delete(this.userHasController.controller.id).subscribe(res => {
            this.onDeleteController.emit(this.userHasController.controller.id);
        }, err => {
                this.openSnackBar(err.error.message, null, "snack-error");
        });
    }

    editController() {
        this.editControllerService.open(true, this.userHasController.controller.id).afterClosed().subscribe(res => {
            if (typeof res !== 'undefined') {
                this.userHasController.controller = res;
            }
        });
    }

    accessPolicy() {
        this.controllerAccessService.open(this.userHasController.controller.id);
    }

    openSnackBar(message: string, action: string, snackClass: string) {
        return this.snackBar.open(message, action, {
            duration: 3000,
            verticalPosition: "top",
            horizontalPosition: "right",
            panelClass: [snackClass]
        });
    }

}
