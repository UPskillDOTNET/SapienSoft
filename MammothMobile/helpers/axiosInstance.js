import axios from 'axios';
import AsyncStorage from "@react-native-async-storage/async-storage";
import { navigate } from '../navigations/RootNavigator';

let headers={};

const axiosInstance = axios.create({
    baseURL: 'http://192.168.1.80:42100/api/',
    headers
});

// intercepta todos os pedidos e aplica as seguintes definições...
// definições iguais para todos os pedidos

axiosInstance.interceptors.request.use(
    async (config) => {
        const token = await AsyncStorage.getItem('token')
        if (token) {
            config.headers.Authorization = `Bearer ${token}`
        }
        return config;
    },
    (error)=>{
        return Promise.reject(error);
    },
);

// intercepta todas as respostas e toma as seguintes ações...
// caso em que o token está expirado!

axiosInstance.interceptors.response.use(response => new Promise((resolve, reject) => {
    resolve(response);
}), error => {

    // error from client (not a problema from server)
    if(!error.response) {
        return new Promise((resolve, reject) => {
            reject(error);
        });
    }

    // error from server -> Unauthorized or Forbidden (token expired = Unauthorized)
    if(error.response.status === 401 || error.response.status === 403) {
        navigate('Logout', {tokenExpired: true})
    }
    else {
        return new Promise((resolve, reject) => {
            reject(error);
        });
    }
})

export default axiosInstance;