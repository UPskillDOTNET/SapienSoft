import { GET_USER_FAIL, GET_USER_LOADING, GET_USER_SUCCESS } from "../../constants/actionTypes";

const userReducer = (state, {type, payload}) => {
    switch (type) {
        case GET_USER_LOADING:
            return {
                ...state,
                getUser: {
                    ...state.getUser,
                    loading: true,
                    error: null,
                },
            };
        case GET_USER_SUCCESS:
            return {
                ...state,
                getUser: {
                    ...state.getUser,
                    loading: false,
                    data: payload,
                    error: null,
                },
            };
        case GET_USER_FAIL:
            return {
                ...state,
                getUser: {
                    ...state.getUser,
                    loading: false,
                    error: payload,
                },
            };
        default:
            return state;
    }
}

export default userReducer;