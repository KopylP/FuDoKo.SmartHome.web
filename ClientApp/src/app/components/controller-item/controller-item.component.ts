import { Component, Input, OnInit, ContentChild, ElementRef, ViewChild, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { ControllerService } from "../../services/controller.service";
import { MatMenuTrigger } from "@angular/material";
import { EditControllerService } from "../../services/edit-controller.service";
import { UserHasController } from "../../interfaces/UserHasController";


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
    constructor(private controllerService: ControllerService,
        private editControllerService: EditControllerService) {
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
        if (this.class === "selected") {
            this.trigger.openMenu();
        } else {
            this.trigger.closeMenu();
        }
        return false;
    }

    deleteController() {
        this.controllerService.delete(this.userHasController.controller.id).subscribe(res => {
            this.onDeleteController.emit(this.userHasController.controller.id);
        });
    }

    editController() {
        this.editControllerService.open(true, this.userHasController.controller.id).afterClosed().subscribe(res => {
            if (typeof res !== 'undefined') {
                this.userHasController.controller = res;
            }
        });
    }
}
