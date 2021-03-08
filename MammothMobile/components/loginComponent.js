import { useNavigation } from '@react-navigation/core';
import React from 'react';
import { ActivityIndicator, Button, Image, Text, View } from 'react-native';
import { ScrollView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';

const LoginComponent = ({ errors, onChange, onSubmit, error, loading }) => {
    const {navigate} = useNavigation();
    return (
        
            <View style={GlobalStyles.container}>
                <ScrollView>
                    <View style={GlobalStyles.logo}>
                        <Image source={require('../assets/logo.png')}/>
                    </View>

                    <Text style={{alignSelf: 'center'}}>A parking App by SapienSoft</Text>

                    {error?.message && <Text  style={[GlobalStyles.textError, {alignSelf: 'center'}]}>{error?.message}</Text>}

                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Email</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter Email'
                            editable={true}
                            onChangeText={(value) => {onChange({name:'Email', value})}}
                        />
                        <Text style={GlobalStyles.textError}>{errors.Email}</Text>
                    </View>
                    
                    <View style={GlobalStyles.input}>
                        <Text style={GlobalStyles.label}>Password</Text>
                        <TextInput
                            style={GlobalStyles.textInput}
                            placeholder='Enter Password'
                            secureTextEntry={true}
                            onChangeText={(value) => {onChange({name:'Password', value})}}
                        />
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