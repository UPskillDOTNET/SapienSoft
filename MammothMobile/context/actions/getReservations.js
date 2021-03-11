import { GET_RESERVATIONS_FAIL, GET_RESERVATIONS_LOADING, GET_RESERVATIONS_SUCCESS } from "../../constants/actionTypes";
import axiosInstance from '../../helpers/axiosInstance';

export default () => (dispatch) => {
    dispatch({
        type: GET_RESERVATIONS_LOADING,
    });
    
    axiosInstance.get('reservations/user')
        .then((resp) => {
            dispatch({
                type: GET_RESERVATIONS_SUCCESS,
                payload: resp.data,
            });
        })
        .catch((err) => {
            dispatch({
                type: GET_RESERVATIONS_FAIL,
                payload: err.response ? err.response.data : {error: 'Something went wrong.'},
            });
        });
}