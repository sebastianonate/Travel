import requests from "./requestShared";
const RESOURCE = "Libro";
export const apiLibros = requests(RESOURCE);

export default { ...apiLibros };