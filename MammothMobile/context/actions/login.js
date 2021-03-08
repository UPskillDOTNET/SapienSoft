import axios from "axios";
import { LOGIN_FAIL, LOGIN_LOADING, LOGIN_SUCCESS } from "../../constants/actionTypes";
import AsyncStorage from '@react-native-async-storage/async-storage';

export const clearAuthState = () => (dispatch) => {
    dispatch({
        type: CLEAR_AUTH_STATE,
    })
}

export default ({Email, Password}) => dispatch => {
    dispatch({
        type: LOGIN_LOADING,
    });

    axios.post('http://192.168.1.80:42100/api/user/token', {Email, Password})
        .then((resp) => {
            if (resp.data.token === null){
                dispatch({
                    type: LOGIN_FAIL,
                    payload: resp.data,
                });
            }
            else {
                AsyncStorage.setItem('token', resp.data.token)
                AsyncStorage.setItem('username', resp.data.userName)
                dispatch({
                    type: LOGIN_SUCCESS,
                    payload: resp.data,
                });
            }
        })
        .catch((err) => {
            console.log('err: ', err)
        })
    console.log()
}