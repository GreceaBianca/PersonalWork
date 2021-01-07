import { NavMenuItem } from "./NavMenuItem";
import { UserRoles } from "../../../models/users/UserRole";

export const appMenuItems: NavMenuItem[] = [
  {
    name: "Proprietati",
    route: { path: "properties",
    data: { minUserRole: UserRoles.Users } }
  },
  {
    name: "Acasa",
    route: { path: "home" }
  },
  {
    name: "Contact",
    route: { path: "contact" }
  },
  {
    name: "Login",
    route: { path: "login" }
  },
  {
    name: "Cont",
    route: { path: "account",
    data: { minUserRole: UserRoles.Users }
   }
  }
];
