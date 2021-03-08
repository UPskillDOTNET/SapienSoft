import { useNavigation } from '@react-navigation/core';
import React, { useEffect } from 'react';
import { Text, View } from 'react-native';
import { TouchableOpacity } from 'react-native-gesture-handler';

const Home = () => {
    const { setOptions, toggleDrawer } = useNavigation();
    useEffect(() => {
        setOptions({headerLeft: () => (
            <TouchableOpacity onPress = {() => {toggleDrawer()}}>
                <Text>NAV</Text>
            </TouchableOpacity>
        )})
    }, [])
    return (
        <View>
            <Text>Hello Home</Text>
        </View>
    )
}

export default Home;