import { GET_USER_FAIL, GET_USER_LOADING, GET_USER_SUCCESS } from "../../constants/actionTypes"
import axiosInstance from '../../helpers/axiosInstance';

export default () => (dispatch) =>{
    dispatch({
        type: GET_USER_LOADING,
    });

    axiosInstance.get('user/info')
        .then((resp) => {
            dispatch({
                type: GET_USER_SUCCESS,
                payload: resp.data,
            });
        })  
        .catch((err) => {
            console.log('err:', err)
            dispatch({
                type: GET_USER_FAIL,
                payload: err.response ? err.response.data : {error: 'Something went wrong, please try again later.'}
            });
    })

};