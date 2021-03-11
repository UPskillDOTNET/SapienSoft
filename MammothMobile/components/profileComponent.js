import React from 'react';
import { ActivityIndicator, Text, View, FlatList, Image } from 'react-native';
import { TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';
import { useNavigation } from '@react-navigation/core';
import { Button } from 'react-native';

const ProfileComponent = ({ data, onChange, form, loading, editing, handleEditing, handleSaveChanges }) => {
    return (
            <View style={GlobalStyles.stackContainer}>
                { !loading && (
                    <View>
                        <MaterialIcon name='person' size={130} style={{alignSelf: 'center'}} color={'dodgerblue'}></MaterialIcon>
                        <Text style={[GlobalStyles.label, {color:'darkorange'}]}>First Name</Text>
                        <TextInput
                            style={{height: 40, borderTopColor: 'gray', borderTopWidth: 1, paddingHorizontal: 10, color:'dimgrey'}}
                            value={data.firstName}
                            editable={editing}
                            onChangeText={(value) => {onChange({name:'FirstName', value})}}
                        />

                        <Text style={[GlobalStyles.label, {color:'darkorange'}]}>Last Name</Text>
                        <TextInput
                            style={{ height: 40, borderTopColor: 'gray', borderTopWidth: 1, paddingHorizontal: 10, color:'dimgrey'}}
                            value={data.lastName}
                            editable={editing}
                            onChangeText={(value) => {onChange({name:'LastName', value})}}
                        />

                        <Text style={[GlobalStyles.label, {color:'darkorange'}]}>Username</Text>
                        <TextInput
                            style={{ height: 40, borderTopColor: 'gray', borderTopWidth: 1, paddingHorizontal: 10, color:'dimgrey' }}
                            value={data.userName}
                            editable={false}
                        />

                        <Text style={[GlobalStyles.label, {color:'darkorange'}]}>Email</Text>
                        <TextInput
                            style={{ height: 40, borderTopColor: 'gray', borderTopWidth: 1, paddingHorizontal: 10, color:'dimgrey' }}
                            value={data.email}
                            editable={false}
                        />

                        <Text style={[GlobalStyles.label, {color:'darkorange'}]}>Phone Number</Text>
                        <TextInput
                            style={{ height: 40, borderTopColor: 'gray', borderTopWidth: 1, paddingHorizontal: 10, color:'dimgrey' }}
                            value={data.phoneNumber}
                            editable={editing}
                        />

                        <Text></Text>
                        <TextInput
                            style={{ height: 40, borderColor: 'gray', borderWidth: 1, paddingHorizontal: 10, borderRadius: 5, alignSelf: 'center', color:'dimgrey'}}
                            value={"ID " + data.id}
                            editable={false}
                        />

                    </View>
                )}
                { loading && (<ActivityIndicator size="large" color='dodgerblue' />) }
            </View>
    )
}

export default ProfileComponent;