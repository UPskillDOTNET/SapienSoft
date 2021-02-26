import { StatusBar } from 'expo-status-bar';
import React, { useState } from 'react';
import { Text, View, Button, Modal, Image, Alert, TouchableWithoutFeedback, Keyboard, ImageBackground } from 'react-native';
import RegisterForm from './registerForm';
import LoginForm from './loginForm';
import { globalStyles } from '../styles/global';


export default function Welcome({navigation}) {

  const [registerModalOpen, setRegisterModalOpen] = useState(false);
  const [loginModalOpen, setLoginModalOpen] = useState(false);

  async function registerUser(userData) {
    try {
      let response = await fetch('https://192.168.1.80:44398/api/User/register', {
        // https://localhost:44398/api/User/register
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-type': 'application/json'},
        body: JSON.stringify(userData)
      });
      if (response.status == '200'){
        Alert.alert('Information', 'Login succsseful!');
        console.log(response.headers);
      }
      else {
        Alert.alert('Information', 'Login failed!');
      }
       setRegisterModalOpen(false);
    } catch (error) {
      //Alert.alert('Error', error)
      console.log(JSON.stringify(error));
    }
  };

  async function loginUser(userData) {
    try {
      let response = await fetch('https://webhook.site/553c9d26-38c1-416c-ad30-bd57c3ea455f', {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-type': 'application/json'},
        body: userData
      });
      if (response.status == '200'){
        Alert.alert('Information', 'Login successful!');
        console.log(response.headers);
        var token = {
          "userName": "aalves",
          "email": "aalves@gmail.com",
          "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhYWx2ZXMiLCJqdGkiOiJkMmI0Y2ViOC00NjgzLTRlNmEtYWEyYy1kNDYwYWM3ZDE4MGUiLCJlbWFpbCI6ImFhbHZlc0BnbWFpbC5jb20iLCJ1aWQiOiI3N2UzZTNhMC0zOTEzLTQwMmEtOGE2Zi1mMzIxYWU3ODc5NjEiLCJyb2xlcyI6IlVzZXIiLCJleHAiOjE2MTQyNTc4NjUsImlzcyI6IlNlY3VyZUFwaSIsImF1ZCI6IlNlY3VyZUFwaVVzZXIifQ.lNn0rukwXeakxm0WwpRDhdv9wU5m_IRZIfyMG0gw07k"
        }
        setLoginModalOpen(false);
        navigation.navigate('Home', token)
      }
    } catch (error) {
      Alert.alert('Error', error)
      console.log(error);
    }
  };

  return (
    <View style={globalStyles.container}>
      <StatusBar style="auto" />

      {/* Register Modal */}
      <Modal visible={registerModalOpen} animationType='slide'>
        <TouchableWithoutFeedback onPress={Keyboard.dismiss} >
          <View>
            <RegisterForm registerUser={registerUser}/>
            <View style={globalStyles.button}>
              <Button title={'Cancel'} onPress={() => setRegisterModalOpen(false)} />
            </View>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Login Modal */}
      <Modal visible={loginModalOpen} animationType='slide'>
        <TouchableWithoutFeedback onPress={Keyboard.dismiss} >
          <View>
            <LoginForm loginUser={loginUser}/>
            <View style={globalStyles.button}>
              <Button title={'Cancel'} onPress={() => setLoginModalOpen(false)} />
            </View>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      

      {/* Welcome Screen */}
      <Image source={require('../assets/logo.png')} />
      <Text>Parking App by SapienSoft</Text>
      <View style={globalStyles.button}>
        <Button title={'Register'} onPress={() => setRegisterModalOpen(true)} />
      </View>
      <View style={globalStyles.button}>
        <Button title={'Login'} onPress={() => setLoginModalOpen(true)}/>
      </View>
      </View> 
  );
}