import axios from "axios";
export const ENDPOINT = process.env.REACT_APP_ENPOINT;
interface IHEADERS {
    "Content-Type": string;
    Accept: string;
    Authorization?: string;
}
let HEADERS = {
    "Content-Type": "application/json",
    Accept: "application/json",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept"
} as IHEADERS;

//Agregar token si existe
const token = localStorage.getItem("token");
HEADERS = !token ? HEADERS : { ...HEADERS, Authorization: `Bearer ${token}` };

export const http = axios.create({
    baseURL: ENDPOINT,
    timeout: 50000,
    headers: HEADERS,
    validateStatus: (status) => status < 500,
});

http.interceptors.response.use((anyResponse) => {
    if (anyResponse.request.responseURL.split('/')[3] != 'login' && anyResponse?.status === 401) {
        localStorage.clear()
        window.location.reload()
    }
    return anyResponse
})

//Encabezados de la peticiones
//Metodos generales para utilizar

export const httpClient = {
    getEntries: async (path: string) => {
        const res = await http.get(`${ENDPOINT}/${path}`);
        return res.data;
    },
    getEntry: async (path: string, id: number) => {
        const res = await http.get(`${ENDPOINT}/${path}/${id}`);
        return res.data;
    },
    create: async (path: string, data: object) => {
        const res = await http.post(`${ENDPOINT}/${path}`, data);
        return res.data;
    },
    update: async (path: string, id: number, data: object) => {
        const res = await http.put(`${ENDPOINT}/${path}/${id}`, data);
        return res.data;
    },
    patch: async (path: string, id: number, data: object) => {
        const res = await http.patch(`${ENDPOINT}/${path}/${id}`, data);
        return res.data;
    },
    remove: async (path: string, id: number) => {
        const res = await http.delete(`${ENDPOINT}/${path}/${id}`);
        return res.data;
    },
    get: async (path: string) => {
        const res = await http.get(`${ENDPOINT}/${path}`)
        if (res.status >= 400) throw res;
        return res.data
    },
    post: async (path: string, data: object, config: object ) => {
        const params = [`${ENDPOINT}/${path}`, data] as any;
        if (config) params.push(config);
        const result = await http.post({...params});
        if (result.status >= 400) throw result;
        return result;
    },
};

export default httpClient;
