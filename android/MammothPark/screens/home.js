import React, {useState } from 'react';
import { Text, TextInput, View, Button } from 'react-native';
import { globalStyles } from '../styles/global';
import DateTimePicker from '@react-native-community/datetimepicker';
import { MaterialIcons } from '@expo/vector-icons';
import { TouchableOpacity } from 'react-native-gesture-handler';

export default function Home({navigation}) {


    
    
    return (
        <View style={globalStyles.container}>

            <Text>Welcome {navigation.getParam('userName')}</Text>
            <View style={ globalStyles.button }>
                <Button title='Reservation' onPress={() => (navigation.navigate('Reservation'))}/>
            </View>
            <View style={ globalStyles.button } onPress={() => (navigation.navigate('Profile'))}>
                <Button title='Profile'/>
            </View>
            

        </View>
    )
}