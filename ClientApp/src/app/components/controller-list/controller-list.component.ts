import { Component, OnInit } from "@angular/core";
import { ControllerService } from "../../services/controller.service";

@Component({
    selector: "app-controller-list",
    templateUrl: "./controller-list.component.html",
    styleUrls: ['./controller-list.component.less']
})
export class ControllerListComponent  {

    userHasControllers: UserHasController[] = [];

    constructor(private controllerService: ControllerService) {

    }

    ngOnInit(): void {
        this.controllerService.getAll().subscribe(res => {
            console.log(res);
            this.userHasControllers = res;
        }, err => {
                console.log(err);
        });
    }
    
}
