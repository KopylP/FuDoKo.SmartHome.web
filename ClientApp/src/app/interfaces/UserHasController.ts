import { Controller } from "./Controller";
import { RegisterUser } from "./RegisterUser";
export interface UserHasController {
    id?: number;
    isAdmin: boolean;
    controller: Controller;
    user?: RegisterUser;
    userId?: string;
}
