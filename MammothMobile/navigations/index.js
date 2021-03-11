import React, { useContext, useEffect, useState } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import AuthNavigator from './AuthNavigator';
import DrawerNavigator from './DrawerNavigator';
import { GlobalContext } from '../context/provider';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { ActivityIndicator } from 'react-native';
import { navigationRef } from './RootNavigator';

const AppNavContainer = () => {

    const { authState: {isLoggedIn} } = useContext(GlobalContext);
    const [isAuthenticated, setIsAuthenticated] = useState(isLoggedIn);
    const [authLoaded, setAuthLoaded] = useState(false);

    const getUser = async () => {
        try {
            const token = await AsyncStorage.getItem('token');
            console.log('token: ', token)
            if(token) {
                setAuthLoaded(true);    
                setIsAuthenticated(true);
            }
            else {
                setAuthLoaded(true);
                setIsAuthenticated(false);
            }
        }
        catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        getUser();
    }, [isLoggedIn]);

    return(
        <>
            {authLoaded ? (
                <NavigationContainer ref={navigationRef}>
                    { isAuthenticated ? ( <DrawerNavigator/> ) : ( <AuthNavigator /> ) }
                </NavigationContainer>
            ) : (
            <ActivityIndicator/>
            )}
        </>
    )
}

export default AppNavContainer;