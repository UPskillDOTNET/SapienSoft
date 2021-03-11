import { useNavigation } from '@react-navigation/core';
import React from 'react';
import { Text, View, Image, Linking, StyleSheet, Dimensions } from 'react-native';
import { ScrollView } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import { Button } from 'react-native';
import MapView from 'react-native-maps';

const BookingDetailComponent = ({ booking, onSubmit }) => {

    const { dateCreated, end, externalId, latitude, locator, longitude, parkName, slotId, start, userId, value } = booking;

    return (
        <ScrollView style={{padding: 20, backgroundColor: 'white'}}>
            <View>

                <Text>Park Name:  {parkName}</Text>
                <Text>Start:  {new Date(start).toDateString()}</Text>
                <Text>End:  {new Date(end).toDateString()}</Text>
                <Text>Locator:  {locator}</Text>
                <Text>SlotId:  {slotId}</Text>
                <Text>Value:  {value.toFixed(2)} €</Text>
                <View style={GlobalStyles.buttonWrapper}>
                    <Button title='Book now!' onPress={onSubmit}/>
                </View>

            </View >
            <View style={styles.mapContainer}>
                <MapView
                    style={styles.map}
                    initialRegion={{
                        latitude: latitude,
                        longitude: longitude,
                        latitudeDelta: 0.05,
                        longitudeDelta: 0.05,
                    }}
                ><MapView.Marker
                    coordinate={{latitude: latitude, longitude: longitude}}
                    title={parkName}
                    description={locator}
                />
                </MapView>
                <View style={{flexDirection: 'row', justifyContent:'space-between'}}>
                    <Text style={{paddingHorizontal:20, color: 'dimgrey'}}>Lat:{latitude.toFixed(8)}</Text>
                    <Text style={{paddingHorizontal:20, color: 'dimgrey'}}>Long:{longitude.toFixed(8)}</Text>
                </View>
                
            </View>
        </ScrollView>
    )
}

const styles = StyleSheet.create({
    mapContainer: {
      flex: 1,
      backgroundColor: '#fff',
      alignItems: 'center',
      justifyContent: 'center',
    },
    map: {
      width: Dimensions.get('window').width,
      height: Dimensions.get('window').height/1.8,
    },
  });

export default BookingDetailComponent;