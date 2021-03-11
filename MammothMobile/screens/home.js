import { useNavigation } from '@react-navigation/core';
import React, { useEffect } from 'react';
import { Text, View } from 'react-native';
import { TouchableOpacity } from 'react-native-gesture-handler';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';
import HomeComponent from '../components/homeComponent';

const Home = () => {
    const { setOptions, toggleDrawer } = useNavigation();
    useEffect(() => {
        setOptions({headerLeft: () => (
            <TouchableOpacity onPress = {() => {toggleDrawer()}}>
                <MaterialIcon name='menu' size={28} style={{padding:20}}></MaterialIcon>
            </TouchableOpacity>
        )})
    }, [])
    return (
        <HomeComponent />
    )
}

export default Home;