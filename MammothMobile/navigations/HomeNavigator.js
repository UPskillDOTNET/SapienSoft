import React from 'react';
import { Text, View } from 'react-native';
import { createStackNavigator } from '@react-navigation/stack';
import Home from '../screens/home';
import Reservations from '../screens/reservations';
import searchReservations from '../screens/searchReservations';
import reservationDetail from '../screens/reservationDetail';
import myReservations from '../screens/myReservations';
import Profile from '../screens/profile';

const HomeNavigator = () => {
    const HomeStack = createStackNavigator();
    return(
        <HomeStack.Navigator initialRouteName='Home'>
            <HomeStack.Screen name='Home' component={Home}></HomeStack.Screen>
            <HomeStack.Screen name='Reservations' component={Reservations}></HomeStack.Screen>
            <HomeStack.Screen name='SearchReservations' component={searchReservations}></HomeStack.Screen>
            <HomeStack.Screen name='ReservationDetail' component={reservationDetail}></HomeStack.Screen>
            <HomeStack.Screen name='MyReservations' component={myReservations}></HomeStack.Screen>
            <HomeStack.Screen name='Profile' component={Profile}></HomeStack.Screen>
        </HomeStack.Navigator>
    )
}

export default HomeNavigator;