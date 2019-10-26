import { Component, Input } from "@angular/core";
import { faHome } from "@fortawesome/free-solid-svg-icons";


@Component({
    selector: "app-controller-item",
    templateUrl: "./controller-item.component.html",
    styleUrls: ['./controller-item.component.less']
})
export class ControllerItemComponent {

    @Input() userHasController: UserHasController;
    faHome = faHome;
    constructor() {}
}
