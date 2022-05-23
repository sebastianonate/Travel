import { httpClient } from "./httprequest";
import requests from "./requestShared";

const RESOURCE = "Editorial";

export const apiEditoriales = requests(RESOURCE);

export default { ...apiEditoriales };