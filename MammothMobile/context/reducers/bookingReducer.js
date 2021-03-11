import { BOOKING_FAIL, BOOKING_LOADING, BOOKING_SUCCESS } from "../../constants/actionTypes";

const bookingReducer = (state, {type, payload}) => {
    switch (type) {
        case BOOKING_LOADING:
            return {
                ...state,
                getBooking: {
                    ...state.getBooking,
                    loading: true,
                    error: null,
                },
            };
        case BOOKING_SUCCESS:
            return {
                ...state,
                getBooking: {
                    ...state.getBooking,
                    loading: false,
                    data: payload,
                    error: null,
                },
            };
        case BOOKING_FAIL:
            return {
                ...state,
                getBooking: {
                    ...state.getBooking,
                    loading: false,
                    error: payload,
                },
            };
        default:
            return state;
    }
}

export default bookingReducer;