import { useRoute } from '@react-navigation/core';
import React, { useContext } from 'react';
import { Linking, Alert } from 'react-native';
import BookingDetailComponent from '../components/bookingDetailComponent';
import createBooking from '../context/actions/createBooking';
import { GlobalContext } from '../context/provider';
import { navigate } from '../navigations/RootNavigator';
import { StackActions } from '@react-navigation/native';

const bookingDetail = ({navigation}) => {

    const { bookingDispatch } = useContext(GlobalContext)
    const { params: { item = {} } = {} } = useRoute();

    console.log(item)

    const openMaps = () => {
        const scheme = Platform.select({ ios: 'maps:0,0?q=', android: 'geo:0,0?q=' });
        const latLng = `${item.latitude},${item.longitude}`;
        const label = item.name;
        const url = Platform.select({
            ios: `${scheme}${label}@${latLng}`,
            android: `${scheme}${latLng}(${label})`
        });
        Linking.openURL(url);
    }

    const createEvent = () => {

        if(Platform.OS === 'ios') {
               Linking.openURL('calshow:');
        } else if(Platform.OS === 'android') {
            Linking.openURL('content://com.android.calendar/time/');
        }
    }

    const onSubmit = () => {
        Alert.alert('Booking', 'Are you sure?', [
            {
                text:'Cancel', onPress: () => {},
            },
            {
                text: 'Yes',
                onPress: () => {
                    createBooking(item)(bookingDispatch);
                    navigation.dispatch(StackActions.popToTop());
                },
            },
        ]);
    }

    return (
        <BookingDetailComponent
            booking={item}
            createEvent={createEvent}
            openMaps={openMaps}
            booking={item}
            onSubmit={onSubmit}
        />
    )
}

export default bookingDetail;