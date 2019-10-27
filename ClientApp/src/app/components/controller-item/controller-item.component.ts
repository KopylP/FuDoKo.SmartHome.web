import { Component, Input, OnInit, ContentChild, ElementRef, ViewChild, OnChanges, SimpleChanges } from "@angular/core";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { ControllerService } from "../../services/controller.service";


@Component({
    selector: "app-controller-item",
    templateUrl: "./controller-item.component.html",
    styleUrls: ['./controller-item.component.less']
})
export class ControllerItemComponent implements OnInit, OnChanges {

    @Input() userHasController: UserHasController;
    @Input() class: string;



    @ViewChild("controllerElement", { static:true })
    controllerElement: ElementRef;

    faHome = faHome;
    constructor(private controllerService: ControllerService) {
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
            }
        }
    }

    changeStatus(event) {
        const checked = event.currentTarget.checked;
        const controller = this.userHasController.controller;
        if (checked) {
            this.controllerService.edit(controller).subscribe(res => {
                console.log("Controller is updated");
            });
        } else {
            this.controllerService.disable(controller.id).subscribe(res => {
                console.log("Controller is disabled!");
            });
        }
    }


}
