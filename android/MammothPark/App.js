import { StatusBar } from 'expo-status-bar';
import React, { useState } from 'react';
import { Text, View, Button, Modal, Image } from 'react-native';
import RegisterForm from './screens/registerForm';
import { globalStyles } from './styles/global';
import Navigator from './routes/welcomeStack';


export default function App() {
  return (
    <Navigator />
  );
}