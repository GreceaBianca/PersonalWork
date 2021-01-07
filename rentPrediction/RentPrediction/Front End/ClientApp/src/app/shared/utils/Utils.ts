export function clone(data) {
    return data != null ? JSON.parse(JSON.stringify(data)) : data;
  }
  export function compareEntitiesById(obj1: any, obj2: any) {
    return obj1 && obj2 ? obj1.id === obj2.id : obj1 === obj2;
  }