import { GET_TRANSACTIONS_FAIL, GET_TRANSACTIONS_LOADING, GET_TRANSACTIONS_SUCCESS } from "../../constants/actionTypes";

const transactionsReducer = (state, {type, payload}) => {
    switch (type) {
        case GET_TRANSACTIONS_LOADING:
            return {
                ...state,
                getTransactions: {
                    ...state.getTransactions,
                    loading: true,
                    error: null,
                },
            };
        case GET_TRANSACTIONS_SUCCESS:
            return {
                ...state,
                getTransactions: {
                    ...state.getTransactions,
                    loading: false,
                    data: payload,
                    error: null,
                },
            };
        case GET_TRANSACTIONS_FAIL:
            return {
                ...state,
                getTransactions: {
                    ...state.getTransactions,
                    loading: false,
                    error: payload,
                },
            };
        default:
            return state;
    }
}

export default transactionsReducer;