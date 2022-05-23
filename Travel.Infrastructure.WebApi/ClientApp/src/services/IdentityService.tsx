import { ILogin } from "../Interfaces";
import { httpClient } from "./httprequest";
import requests from "./requestShared";

const RESOURCE = "identity";

export const apiIdentity = {
    ...requests(RESOURCE),
    login: (data: ILogin) => httpClient.create(`${RESOURCE}/token`, data)
};

export default {
    ...apiIdentity,
};  