import { Address } from "../address/Address";
import { Feature } from "../feature/Feature";
import { Gallery } from "../galery/Gallery";
import { User } from "../users/User";
import { PropertyType } from "./PropertyType";

export class Property{
    id:number;
    name:string;
    description:string;
    url:string;
    price:number;
    predictedPrice:string;
    usableSurface:number;
    surface:number;
    creationDate:Date;
    availableDate:string;
    feature:Feature;
    address:Address;
    propertyType: PropertyType;
    user: User;
    userId:number;
    images:Gallery[];
}