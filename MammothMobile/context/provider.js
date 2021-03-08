import React, { createContext, useReducer } from 'react';
import authInitialState from './initialStates/authInitialState';
import reservationsInitialState from './initialStates/reservationsInitialState';
import authReducer from './reducers/authReducer';
import reservationsReducer from './reducers/reservationsReducer';


export const GlobalContext = createContext({});

const GlobalProvider=({children}) => {
    const [authState, authDispatch] = useReducer(authReducer, authInitialState)
    const [reservationsState, reservationsDispatch] = useReducer(reservationsReducer, reservationsInitialState)
    return (
        <GlobalContext.Provider value={{authState, authDispatch, reservationsState, reservationsDispatch}}>
            {children}
        </GlobalContext.Provider>
    )
}

export default GlobalProvider;