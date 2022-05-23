import { AutoresInterface } from "../Interfaces";
import { httpClient } from "./httprequest";


const  requests = (entity: string) => {
  const getEntries = () => httpClient.getEntries(`${entity}`) ;
  const getEntry = (id: number) => httpClient.getEntry(entity, id);
  const create = (data: object) => httpClient.create(entity, data);
  const update = (id: number, data: object) => httpClient.update(entity, id, data);
  const patch = (id: number, data: object) => httpClient.patch(entity, id, data);
  const remove = (id: number) => httpClient.remove(entity, id);

  return {
    getEntries,
    getEntry,
    create,
    update,
    patch,
    remove,
  };
}

export default requests;