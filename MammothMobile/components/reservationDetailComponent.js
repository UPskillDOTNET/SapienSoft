import { useNavigation } from '@react-navigation/core';
import React from 'react';
import { Text, View, Image, Linking } from 'react-native';
import { ScrollView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';

const ReservationDetailComponent = ({ reservation, openMaps, createEvent }) => {

    const {availableToRent, dateCreated, end, externalId, id, latitude, locator, longitude, qrCode, rentValue, start, value, park} = reservation;

    return (
        <ScrollView style={GlobalStyles.container}>
        <View>
            <View style={{flexDirection: 'row', justifyContent: 'space-evenly', paddingBottom: 20}}>
                <Image source={{uri: qrCode}} style={{width:150, height: 150, alignSelf: 'center'}}/>
                <Text style={{fontSize: 50, color: 'dodgerblue', textAlignVertical: 'center'}}>{locator}</Text>
            </View>

            <View style={GlobalStyles.rowContent}>
                <TouchableOpacity onPress={() => {Linking.openURL('https://www.aeroportoparque.com')}}>
                    <MaterialIcon name='http' size={30} style={{padding:20}} color={'orange'}></MaterialIcon>
                </TouchableOpacity>
                <View style={{width: '80%', alignItems: 'center'}}>
                    <Text style={{alignSelf: 'center', fontSize: 20}}>{park.name}</Text>
                    <Text style={{alignSelf: 'center'}}>{park.uri}</Text>
                </View>
            </View>
            
            <View style={GlobalStyles.rowContent}>
                <TouchableOpacity onPress={createEvent}>
                    <MaterialIcon name='date-range' size={30} style={{padding:20}} color={'orange'}></MaterialIcon>
                </TouchableOpacity>
                <View style={{width: '80%'}}>
                    <Text>Start: {start}</Text>
                    <Text>End: {end}</Text>
                    <Text>Created: {dateCreated}</Text>
                </View>
            </View>

            

            <View style={GlobalStyles.rowContent}>
                <TouchableOpacity onPress={openMaps}>
                    <MaterialIcon name='drive-eta' size={30} style={{padding:20}} color={'orange'}></MaterialIcon>
                </TouchableOpacity>
                <View>
                    <Text>Latitude: {latitude.toFixed(8)}</Text>
                    <Text>Longitude: {longitude.toFixed(8)}</Text>
                </View>
            </View>

            <View>
                <Text style={{alignSelf: 'center', fontSize: 20}}>{value.toFixed(2)} €</Text>
            </View>

        </View>
        </ScrollView>

        
    )
}

export default ReservationDetailComponent;