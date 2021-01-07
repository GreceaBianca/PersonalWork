import { Sidekick } from "./Sidekick";

export class Hero{
    id:number;
    name:string;
    age:number;
    power:number;
    isEvil:boolean;
    sidekickId:number;
    sidekick:Sidekick;
}