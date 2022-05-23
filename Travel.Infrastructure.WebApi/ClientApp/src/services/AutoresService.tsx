import requests from "./requestShared";
const RESOURCE = "Autor";
export const apiAutores = requests(RESOURCE);

export default { ...apiAutores };