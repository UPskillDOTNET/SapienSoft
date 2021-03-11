import { GET_TRANSACTIONS_FAIL, GET_TRANSACTIONS_LOADING, GET_TRANSACTIONS_SUCCESS } from "../../constants/actionTypes";
import axiosInstance from '../../helpers/axiosInstance';

export default () => (dispatch) => {
    dispatch({
        type: GET_TRANSACTIONS_LOADING,
    });
    
    axiosInstance.get('transactions/user')
        .then((resp) => {
            dispatch({
                type: GET_TRANSACTIONS_SUCCESS,
                payload: resp.data,
            });
        })
        .catch((err) => {
            dispatch({
                type: GET_TRANSACTIONS_FAIL,
                payload: err.response ? err.response.data : {error: 'Something went wrong.'},
            });
        });
}