import { Property } from "../properties/Property";

export class Address{
    id:number;
    streetName:string;
    streetNumber:number;
    floor:number;
    latitude:number;
    longitude:number;
    country: string;
    county: string;
    isArchived:boolean;
    propertyId: number;
    property:Property;
}