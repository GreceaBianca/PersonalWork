import { Partitioning } from "../partitioning/Partitioning";

export class Feature{
    id:number;
    hasBalcony:boolean;
    hasGarden:boolean;
    hasHeating:boolean;
    isForSale:boolean;
    hasParking: boolean
    numberOfBalconies: number;
    numberOfBaths: number;
    numberOfRooms: number;
    buildingSeniority:string;
    buildingType:string;
    endowment:string;
    finish:string;
    numberOfParkingSpots:number;
    partitioning:Partitioning;
    constructor(){
        this.hasBalcony=false;
        this.hasGarden=false;
        this.hasHeating=false;
        this.isForSale=false;
        this.hasParking=false
        this.numberOfBalconies=0;
        this.numberOfBaths=0;
        this.numberOfRooms=0;
        this.numberOfParkingSpots=0;
        this.partitioning=new Partitioning();
    }
}