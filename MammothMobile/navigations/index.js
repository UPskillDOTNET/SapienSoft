import React, { useContext, useEffect, useState } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import AuthNavigator from './AuthNavigator';
import DrawerNavigator from './DrawerNavigator';
import { GlobalContext } from '../context/provider';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { ActivityIndicator } from 'react-native';

const AppNavContainer = () => {

    const { authState: {isLoggedIn} } = useContext(GlobalContext);
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [authLoaded, setAuthLoaded] = useState(false);

    const getUser = async () => {
        try {
            const token = await AsyncStorage.getItem('token');
            if(token) {
                setAuthLoaded(true)
                setIsAuthenticated(true);
            }
            else {
                setAuthLoaded(true)
                setIsAuthenticated(false);
            }
        }
        catch (error) {}
    };

    console.log('isLoggedIn :>> ', isAuthenticated)

    useEffect(() => {
        getUser();
    }, []);

    console.log('isLoggedIn: ', isLoggedIn)
    return(
        <>
        {authLoaded?
        <NavigationContainer>
            {isLoggedIn || isAuthenticated ? <DrawerNavigator/> : <AuthNavigator />}
        </NavigationContainer>
        : <ActivityIndicator/>}
        </>
    )
}

export default AppNavContainer;