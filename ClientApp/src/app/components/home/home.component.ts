import { Component, OnInit } from "@angular/core";
import { faUserAstronaut } from "@fortawesome/free-solid-svg-icons";

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
}
