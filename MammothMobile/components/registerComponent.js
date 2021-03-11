import React, { useState } from 'react';
import { Button, Text, View, ActivityIndicator } from "react-native";
import { ScrollView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import { useNavigation } from '@react-navigation/core';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';

const RegisterComponent = ({ errors, onChange, onSubmit, error, loading }) => {
    const {navigate} = useNavigation();
    const [isSecureEntry, setIsSecureEntry] = useState(true);
    return (
        
            <View style={GlobalStyles.container}>
                <ScrollView>
                    <Text style={{alignSelf: 'center'}}>Register now!</Text>

                    <Text style={[GlobalStyles.textError, {alignSelf: 'center'}]}>{error}</Text>

                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>First Name</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter First Name'
                            onChangeText={(value) => {onChange({name:'FirstName', value})}}
                        />
                        <Text style={GlobalStyles.textError}>{errors.FirstName}</Text>
                    </View>
                    
                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Last Name</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter Last Name'
                            onChangeText={(value) => {onChange({name:'LastName', value})}}
                        />
                        <Text style={GlobalStyles.textError}>{errors.LastName}</Text>
                    </View>

                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Username</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter Username'
                            onChangeText={(value) => {onChange({name:'UserName', value})}}
                        />
                        <Text style={GlobalStyles.textError}>{errors.UserName}</Text>
                    </View>
                    
                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Email</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter Email'
                            onChangeText={(value) => {onChange({name:'Email', value})}}
                        />
                        <Text style={GlobalStyles.textError}>{errors.Email}</Text>
                    </View>
                    
                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Password </Text>
                        <View style={GlobalStyles.rowContent}> 
                            <TextInput
                                style={GlobalStyles.textInput}
                                placeholder='Enter Password'
                                secureTextEntry={isSecureEntry}
                                onChangeText={(value) => {onChange({name:'Password', value})}}
                            />
                            <TouchableOpacity style={{padding: 8}}onPress={(prev) => {setIsSecureEntry((prev) => !prev)}}>
                                {isSecureEntry?
                                <MaterialIcon name='visibility' size={24} style={{color: 'grey'}}></MaterialIcon>:
                                <MaterialIcon name='visibility-off' size={24} style={{color: 'grey'}}></MaterialIcon>}
                            </TouchableOpacity>
                        </View>

                        <Text style={GlobalStyles.textError}>{errors.Password}</Text>
                    </View>

                    {loading && <ActivityIndicator size="small" color='dodgerblue' />}
                    <View style={GlobalStyles.buttonWrapper}>
                        <Button title='Submit' onPress={onSubmit} disabled={loading}/>
                    </View>

                        

                    <View style={GlobalStyles.rowContent}>
                        <Text>Already have an account?  </Text>
                        <TouchableOpacity onPress={() => {navigate('Login')}}>
                            <Text style={GlobalStyles.textLink}>Login</Text>
                        </TouchableOpacity>
                    </View>
                </ScrollView>
            </View>
    )
}

export default RegisterComponent;