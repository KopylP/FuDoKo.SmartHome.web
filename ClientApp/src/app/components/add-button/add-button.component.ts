import { Component } from "@angular/core";
import { faPlus } from "@fortawesome/free-solid-svg-icons";

@Component({
    selector: "add-button",
    templateUrl: "./add-button.component.html",
    styleUrls: ["./add-button.component.less"]
})
export class AddButtonComponent {
    faPlus = faPlus;
}
