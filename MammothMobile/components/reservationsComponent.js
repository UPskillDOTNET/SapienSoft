import React from 'react';
import { ActivityIndicator, Text, View, FlatList, Image } from 'react-native';
import { TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';
import { useNavigation } from '@react-navigation/core';

const ReservationsComponent = ({ data, loading }) => {
    const { navigate } = useNavigation();

    const ListEmptyComponent = () => {
        return (
            <View>
                <Text style={{alignSelf:'center', color: 'darkorange'}}>You don't have any reservations.</Text>
            </View>
        )
    }

    const renderItem = ({item}) => {
        return (
            <TouchableOpacity onPress={()=>{
                navigate('ReservationDetail', {item})
            }}>
                <View style={{paddingVertical:15, flexDirection: 'row', alignItems: 'center'}}>
                    <Image source={{uri: item.qrCode}} style={{width:75, height: 75}}/>
                    <View style={{padding:5}}>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'dodgerblue'}}>Park Name: </Text>
                            <Text>{item.park.name}</Text>
                        </View>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'dodgerblue'}}>Locator: </Text>
                            <Text>{item.locator}</Text>
                        </View>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'dodgerblue'}}>Start: </Text>
                            <Text>{new Date(item.start).toUTCString()}</Text>
                        </View>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={{color: 'dodgerblue'}}>End: </Text>
                            <Text>{new Date(item.end).toUTCString()}</Text>
                        </View>
                    </View>
                </View>
            </TouchableOpacity>
        )
    }
    return (
        
        <View style={GlobalStyles.stackScreen}>
                <View style={{alignItems: 'center', padding: 10}}>
                    <TouchableOpacity onPress={() => {navigate('Booking');}}>
                        <MaterialIcon name='add-circle' size={50} color={'darkorange'}></MaterialIcon>
                    </TouchableOpacity>
                </View>
                
                {loading && <View style={GlobalStyles.loading}><ActivityIndicator size="large" color='dodgerblue' /></View>}
                {!loading && <FlatList
                    data={data.sort(((a, b) => (a.start > b.start) ? 1 : -1))}
                    keyExtractor={(item)=> String(item.dateCreated)}
                    renderItem={renderItem}
                    ItemSeparatorComponent={() => (<View style={{height: 1, width: "100%", backgroundColor: "darkorange"}}></View>)}
                    ListEmptyComponent={ListEmptyComponent}
                />}
        </View>

    )
}

export default ReservationsComponent;