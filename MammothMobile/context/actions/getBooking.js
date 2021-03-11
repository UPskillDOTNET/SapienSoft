import { BOOKING_FAIL, BOOKING_LOADING, BOOKING_SUCCESS } from "../../constants/actionTypes";
import axiosInstance from '../../helpers/axiosInstance';

export default (startDateTime, endDateTime) => (dispatch) => {

    dispatch({
        type: BOOKING_LOADING,
    });

    axiosInstance.get(`reservations/available?start=${startDateTime.toISOString()}&end=${endDateTime.toISOString()}`)
        .then((resp) => {
            dispatch({
                type: BOOKING_SUCCESS,
                payload: resp.data,
            });
        })
        .catch((err) => {
            console.log('err: ', err)
            dispatch({
                type: BOOKING_FAIL,
                payload: err.response ? err.response.data : {error: 'Service is down, try again later.'},
            });
        });
};