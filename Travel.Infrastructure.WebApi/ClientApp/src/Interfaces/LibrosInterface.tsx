import AutoresInterface from "./AutoresInterface";
import EditorialesInterface from "./EditorialesInterface";

export interface LibrosInterface {
    id?: number;
    titulo: string;
    sinopsis: string;
    noPaginas: string;
    autores: AutoresInterface[],
    editorial: EditorialesInterface
}

export default LibrosInterface;