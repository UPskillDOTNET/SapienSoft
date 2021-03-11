import { GET_RESERVATIONS_FAIL, GET_RESERVATIONS_LOADING, GET_RESERVATIONS_SUCCESS } from "../../constants/actionTypes";

const reservationsReducer = (state, {type, payload}) => {
    switch (type) {
        case GET_RESERVATIONS_LOADING:
            return {
                ...state,
                getReservations: {
                    ...state.getReservations,
                    loading: true,
                    error: null,
                },
            };
        case GET_RESERVATIONS_SUCCESS:
            return {
                ...state,
                getReservations: {
                    ...state.getReservations,
                    loading: false,
                    data: payload,
                    error: null,
                },
            };
        case GET_RESERVATIONS_FAIL:
            return {
                ...state,
                getReservations: {
                    ...state.getReservations,
                    loading: false,
                    error: payload,
                },
            };
        default:
            return state;
    }
}

export default reservationsReducer;