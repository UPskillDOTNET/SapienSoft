import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import Home from '../screens/home';
import Booking from '../screens/booking';
import searchReservations from '../screens/searchReservations';
import reservationDetail from '../screens/reservationDetail';
import myReservations from '../screens/myReservations';
import myTransactions from '../screens/myTransactions';
import Profile from '../screens/profile';
import Logout from '../screens/logout';
import Privacy from '../screens/privacy';
import bookingDetail from '../screens/bookingDetail';

const HomeNavigator = () => {
    const HomeStack = createStackNavigator();
    return(
        <HomeStack.Navigator initialRouteName='Home'>
            <HomeStack.Screen name='Home' component={Home}></HomeStack.Screen>
            <HomeStack.Screen name='Booking' component={Booking}></HomeStack.Screen>
            <HomeStack.Screen name='SearchReservations' component={searchReservations}></HomeStack.Screen>
            <HomeStack.Screen name='ReservationDetail' component={reservationDetail}></HomeStack.Screen>
            <HomeStack.Screen name='BookingDetail' component={bookingDetail}></HomeStack.Screen>
            <HomeStack.Screen name='My Reservations' component={myReservations}></HomeStack.Screen>
            <HomeStack.Screen name='My Transactions' component={myTransactions}></HomeStack.Screen>
            <HomeStack.Screen name='Profile' component={Profile}></HomeStack.Screen>
            <HomeStack.Screen name='Logout' component={Logout}></HomeStack.Screen>
            <HomeStack.Screen name='Privacy' component={Privacy}></HomeStack.Screen>
        </HomeStack.Navigator>
    )
}

export default HomeNavigator;