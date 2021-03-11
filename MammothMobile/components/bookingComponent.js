import React from 'react';
import { ActivityIndicator, Button, Text, View } from 'react-native';
import { FlatList, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';
import DateTimePicker from '@react-native-community/datetimepicker';
import { useNavigation } from '@react-navigation/core';

const BookingComponent = ({
    startDateTime,
    endDateTime, 
    showStartDatePicker,
    showStartTimePicker,
    showEndDatePicker,
    showEndTimePicker,
    onChangeStart,
    onChangeEnd,
    openShowStartDatePicker,
    openShowStartTimePicker,
    openShowEndDatePicker,
    openShowEndTimePicker,
    errors,
    onSubmit,
    data,
    loading,
}) => {

    const { navigate } = useNavigation();
    
    const ListEmptyComponent = () => {
        return (
            <View>
                <Text style={{color: 'darkorange', alignSelf: 'center'}}>Available bookings will show here!</Text>
            </View>
        );
    };

    const renderItem = ({item}) => {
        return(
            <TouchableOpacity onPress={()=>{navigate('BookingDetail', {item})}}>
                <View style={{paddingVertical:5, flexDirection: 'row', alignItems: 'center'}}>
                    <MaterialIcon name='local-parking' size={50} style={{paddingHorizontal:10}} color={'grey'}></MaterialIcon>
                    <View style={{padding:5}}>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'darkorange'}}>{item.parkName}</Text>
                        </View>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'dodgerblue'}}>Locator: </Text>
                            <Text>{item.locator}</Text>
                        </View>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'dodgerblue'}}>Value: </Text>
                            <Text>{item.value.toFixed(2)}€</Text>
                        </View>
                    </View>
                </View>
                
            </TouchableOpacity>
        );
    };

    return (
        <View style={{paddingHorizontal: 20, backgroundColor: 'white', flex: 1}}>

            {showStartDatePicker && (
                <DateTimePicker
                    value={startDateTime}
                    minimumDate={new Date()}
                    mode='date'
                    onChange={onChangeStart}
                />
            )}
            {showStartTimePicker && (
                <DateTimePicker
                    value={startDateTime}
                    mode='time'
                    is24Hour={true}
                    onChange={onChangeStart}
                />
            )}
            {showEndDatePicker && (
                <DateTimePicker
                    value={endDateTime}
                    minimumDate={new Date()}
                    mode='date'
                    onChange={onChangeEnd}
                />
            )}
            {showEndTimePicker && (
                <DateTimePicker
                    value={endDateTime}
                    mode='time'
                    is24Hour={true}
                    onChange={onChangeEnd}
                />
            )}

            <View>
                <Text style={GlobalStyles.label}>Reservation Start</Text>
                <View style={GlobalStyles.rowContent}>
                    <TextInput
                        style={GlobalStyles.textInputInfo}
                        onChangeText={text => onChangeText(text)}
                        value={startDateTime.toUTCString()}
                        editable={false}
                    />
                    <TouchableOpacity onPress={openShowStartDatePicker}>
                        <MaterialIcon name='date-range' size={25} style={{paddingLeft: 25, color: 'darkorange'}}></MaterialIcon>
                    </TouchableOpacity>
                    <TouchableOpacity onPress={openShowStartTimePicker}>
                        <MaterialIcon name='access-time' size={25} style={{paddingLeft: 25, color: 'darkorange'}}></MaterialIcon>
                    </TouchableOpacity>
                </View>
            </View>

            <View style={{paddingVertical:10}}>
                <Text style={GlobalStyles.label}>Reservation End</Text>
                <View style={GlobalStyles.rowContent}>
                    <TextInput
                        style={GlobalStyles.textInputInfo}
                        onChangeText={text => onChangeText(text)}
                        value={endDateTime.toUTCString()}
                        editable={false}
                    />
                    <TouchableOpacity onPress={openShowEndDatePicker}>
                        <MaterialIcon name='date-range' size={25} style={{paddingLeft: 25, color: 'darkorange'}}></MaterialIcon>
                    </TouchableOpacity>
                    <TouchableOpacity onPress={openShowEndTimePicker}>
                        <MaterialIcon name='access-time' size={25} style={{paddingLeft: 25, color: 'darkorange'}}></MaterialIcon>
                    </TouchableOpacity>
                </View>
                {errors && <Text style={GlobalStyles.textError}>{errors}</Text>}
            </View>

            <View style={{paddingVertical:10}}>
                <Button onPress={onSubmit} title="Submit" />
            </View>
            <View>
                {loading && (
                    <View style={GlobalStyles.loading}>
                        <ActivityIndicator size="large" color='dodgerblue' />
                    </View>
                )}
                
                {!loading && <FlatList
                    data={data.splice(0,10)}
                    numColumns={2}
                    keyExtractor={(item) => String(item.dateCreated)}
                    renderItem={renderItem}
                    ListEmptyComponent={ListEmptyComponent}
                />}
            </View>

        </View>
    )
}

export default BookingComponent;