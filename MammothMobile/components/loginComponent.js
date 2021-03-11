import { useNavigation } from '@react-navigation/core';
import React, { useState } from 'react';
import { ActivityIndicator, Button, Image, Text, View } from 'react-native';
import { ScrollView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';

const LoginComponent = ({ form, errors, onChange, onSubmit, error, loading, justSignedUp }) => {
    const {navigate} = useNavigation();
    const [isSecureEntry, setIsSecureEntry] = useState(true);
    return (
        
            <View style={GlobalStyles.container}>
                <ScrollView>
                    <View style={GlobalStyles.logo}>
                        <Image source={require('../assets/mammoth-logo.png')}/>
                    </View>

                    <Text style={{alignSelf: 'center'}}>A parking App by SapienSoft</Text>

                    {justSignedUp && <Text style={{alignSelf:'center', color: 'darkorange'}}>Account created successfully!</Text>}
                    {error?.message && <Text  style={[GlobalStyles.textError, {alignSelf: 'center'}]}>{error?.message}</Text>}

                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Email</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter Email'
                            value={form.Email || null}
                            editable={true}
                            onChangeText={(value) => {onChange({name:'Email', value})}}
                        />
                        <Text style={GlobalStyles.textError}>{errors.Email}</Text>
                    </View>
                    
                    <View style={GlobalStyles.input}>
                        <View style={GlobalStyles.rowContent}>
                            <Text style={GlobalStyles.label}> Password</Text>
                        </View>
                        <View style={{flexDirection: 'row'}}>
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
                        <Text>Need a new account?  </Text>
                        <TouchableOpacity onPress={() => navigate('Register')}>
                            <Text style={GlobalStyles.textLink}>Register</Text>
                        </TouchableOpacity>
                    </View>  
                </ScrollView>
            </View>
        
    )
}

export default LoginComponent;