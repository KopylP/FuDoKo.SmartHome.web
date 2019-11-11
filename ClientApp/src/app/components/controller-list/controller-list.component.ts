import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { ControllerService } from "../../services/controller.service";
import { EditControllerService } from "../../services/edit-controller.service";
import { UserHasController } from "../../interfaces/UserHasController";

@Component({
    selector: "app-controller-list",
    templateUrl: "./controller-list.component.html",
    styleUrls: ['./controller-list.component.less']
})
export class ControllerListComponent  {

    @Output() onChanged = new EventEmitter<UserHasController>();
    @Output() onDeleteController = new EventEmitter<number>();

    userHasControllers: Array<UserHasController> = [];
    selectedItem: UserHasController;
    constructor(private controllerService: ControllerService,
        private editControllerService: EditControllerService) {

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

    addController() {
        console.log("click");
        this.editControllerService.open(false).afterClosed().subscribe(res => {
            if (typeof res !== 'undefined') {
                this.userHasControllers.push({
                    isAdmin: true,
                    controller: res
                });
            }
        });
    }

    deleteController(id: number) {
        const userHasController = this.userHasControllers.filter(p => p.controller.id === id)[0];
        const index = this.userHasControllers.indexOf(userHasController);
        this.userHasControllers.splice(index, 1);
        this.onDeleteController.emit(this.selectedItem.controller.id);
        this.selectedItem = null;
    }
}
