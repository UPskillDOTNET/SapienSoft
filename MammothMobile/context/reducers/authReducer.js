import { CLEAR_AUTH_STATE, LOGIN_FAIL, LOGIN_LOADING, LOGIN_SUCCESS, REGISTER_FAIL, REGISTER_LOADING, REGISTER_SUCCESS } from "../../constants/actionTypes";

const authReducer = (state, {type, payload}) => {
    switch (type) {
        case LOGIN_LOADING:
        case REGISTER_LOADING:
            return {
                ...state,
                loading: true,
            }
        case REGISTER_SUCCESS:
            return {
                ...state,
                loading: false,
                data: payload,
            };
        case LOGIN_SUCCESS:
            return {
                ...state,
                loading: false,
                data: payload,
                isLoggedIn: true,
            };
        case LOGIN_FAIL:
        case REGISTER_FAIL:
            return {
                ...state,
                loading: false,
                error: payload,
            };
        case CLEAR_AUTH_STATE:
            return {
                ...state,
                loading: false,
                data: null,
                error: null,
            };
            
        default:
            return state;
    }
}

export default authReducer;