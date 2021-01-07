import { Hero } from "./Hero";

export class Planet{
    id: number;
    name:string;
    population:number;
    heroId:number;
    power:number;
    agesUntilShutDown:number;
    hero:Hero;
}