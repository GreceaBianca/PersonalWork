import { Address } from "../address/Address";
import { PropertyType } from "./PropertyType";
import { PropertyFilterFeature } from "./propertyFilterFeature";

export class PropertyFilter{
    id:number;
    price:number;
    usableSurface:number;
    creationDate:Date;
    feature:PropertyFilterFeature;
    address:Address;
    propertyType: PropertyType;
    constructor(){
        this.feature=new PropertyFilterFeature();
        this.price=null;
        this.usableSurface=null;
    }
}