import { Controller } from "./Controller";

export interface UserHasController {
    isAdmin: boolean;
    controller: Controller;
}
