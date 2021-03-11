import { useRoute } from '@react-navigation/core';
import React, { useEffect } from 'react';
import ReservationDetailComponent from '../components/reservationDetailComponent';
import { Linking } from 'react-native';

const reservationDetail = () => {

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

    return (
        <ReservationDetailComponent
            reservation={item}
            createEvent={createEvent}
            openMaps={openMaps}
        />
    )
}

export default reservationDetail;