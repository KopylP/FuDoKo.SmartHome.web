import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { faUserAstronaut } from "@fortawesome/free-solid-svg-icons";
import { UserHasController } from "../../interfaces/UserHasController";
import { Script } from "../../interfaces/Script";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: ['./home.component.less']
})
export class HomeComponent  {

    selectedItem: UserHasController;
    selectedScript: Script;
    faUserAstronaut = faUserAstronaut;

    onControllerChange(userHasController: any) {
        this.selectedItem = userHasController;
        this.selectedScript = null;
    }

    onDelete() {
        this.selectedItem = null;
    }

    changeScript(script: Script) {
        this.selectedScript = script;
    }

    deleteScript() {
        this.selectedScript = null;
    }
}
