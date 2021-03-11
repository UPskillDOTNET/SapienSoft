import { CREATE_BOOKING_FAIL, CREATE_BOOKING_LOADING, CREATE_BOOKING_SUCCESS } from "../../constants/actionTypes";
import axiosInstance from '../../helpers/axiosInstance';

export default (item) => (dispatch) => {

    const data = { start: item.start, end: item.end, slotId: item.slotId }
    
    dispatch({
        type: CREATE_BOOKING_LOADING,
    });

    var parkId = ''
    if (item.parkName === 'Park'){
        parkId = '1'
    }
    else {
        parkId = '2'
    }
    console.log('parkId', parkId)

    axiosInstance.post(`reservations/${parkId}`, data)
        .then((resp) => {
            console.log('resp.data: ', resp.data)
            dispatch({
                type: CREATE_BOOKING_SUCCESS,
                payload: resp.data,
            });
        })
        .catch((err) => {
            console.log('err: ', err)
            dispatch({
                type: CREATE_BOOKING_FAIL,
                payload: err.response ? err.response.data : {error: 'Service is down, try again later.'},
            });
        });
};