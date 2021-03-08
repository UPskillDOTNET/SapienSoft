import axios from "axios";
import { REGISTER_FAIL, REGISTER_LOADING, REGISTER_SUCCESS, CLEAR_AUTH_STATE } from "../../constants/actionTypes";

export const clearAuthState = () => (dispatch) => {
    dispatch({
        type: CLEAR_AUTH_STATE,
    })
}

export default ({FirstName, LastName, UserName, Email, Password}) => dispatch => {
    dispatch({
        type: REGISTER_LOADING,
    });

    axios.post('http://192.168.1.80:42100/api/user/register', {FirstName, LastName, UserName, Email, Password})
        .then((resp) => {
            if (resp.data == 'Invalid model.'){
                dispatch({
                    type: REGISTER_FAIL,
                    payload: resp.data,
                });
            }
            else if (resp.data == `Email ${Email} is already registered.`){
                dispatch({
                    type: REGISTER_FAIL,
                    payload: resp.data,
                });
            }
            else if (resp.data == `User Registered with username ${UserName}`){
                dispatch({
                    type: REGISTER_SUCCESS,
                    payload: resp.data,
                });
            }
            else {
                dispatch({
                    type: REGISTER_FAIL,
                    payload: 'Something else went wrong... sorry!',
                });
            }
        })
        .catch((err) => {
            console.log('err: ', err)
        })
    console.log()
}