import { UserRole } from "./UserRole";
import { UserBrief } from "./UserBrief";

export class User extends UserBrief {
    id: number;
    firstName: string;
    lastName:string;
    email: string;
    phoneNo:string;
    userRole: UserRole;
    token:string;
    roleId:number;
  }