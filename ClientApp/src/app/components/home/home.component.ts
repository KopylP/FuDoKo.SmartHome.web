import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { faUserAstronaut } from "@fortawesome/free-solid-svg-icons";
import { UserHasController } from "../../interfaces/UserHasController";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: ['./home.component.less']
})
export class HomeComponent  {

    selectedItem: UserHasController;
    faUserAstronaut = faUserAstronaut;

    onControllerChange(userHasController: any) {
        this.selectedItem = userHasController;
    }

    onDelete() {
        this.selectedItem = null;
    }
}
