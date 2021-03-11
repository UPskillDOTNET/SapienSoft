import React, { createContext, useReducer } from 'react';
import authInitialState from './initialStates/authInitialState';
import reservationsInitialState from './initialStates/reservationsInitialState';
import authReducer from './reducers/authReducer';
import reservationsReducer from './reducers/reservationsReducer';
import bookingReducer from './reducers/bookingReducer';
import bookingInitialState from './initialStates/bookingInitialState';
import userReducer from './reducers/userReducer';
import userInitialState from './initialStates/userInitialState';
import transactionsInitialState from './initialStates/transactionsInitialState';
import transactionsReducer from './reducers/transactionsReducer';

export const GlobalContext = createContext({});

const GlobalProvider=({children}) => {
    const [authState, authDispatch] = useReducer(authReducer, authInitialState);
    const [reservationsState, reservationsDispatch] = useReducer(reservationsReducer, reservationsInitialState);
    const [bookingState, bookingDispatch] = useReducer(bookingReducer, bookingInitialState);
    const [userState, userDispatch] = useReducer(userReducer, userInitialState);
    const [transactionsState, transactionsDispatch] = useReducer(transactionsReducer, transactionsInitialState)
    return (
        <GlobalContext.Provider value={{
            authState, authDispatch,
            reservationsState, reservationsDispatch,
            bookingState, bookingDispatch,
            userState, userDispatch,
            transactionsState, transactionsDispatch
        }}>
            {children}
        </GlobalContext.Provider>
    )
}

export default GlobalProvider;