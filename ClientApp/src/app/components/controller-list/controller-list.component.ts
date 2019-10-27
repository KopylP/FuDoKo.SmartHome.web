import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { ControllerService } from "../../services/controller.service";

@Component({
    selector: "app-controller-list",
    templateUrl: "./controller-list.component.html",
    styleUrls: ['./controller-list.component.less']
})
export class ControllerListComponent  {

    @Output() onChanged = new EventEmitter<UserHasController>();

    userHasControllers: UserHasController[] = [];
    selectedItem: UserHasController;
    constructor(private controllerService: ControllerService) {

    }

    setSelectedItem(item) {
        this.selectedItem = item;
        this.onChanged.emit(item);
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
