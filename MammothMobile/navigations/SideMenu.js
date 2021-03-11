import React from 'react';
import { View, Text, Image, TouchableOpacity, Alert } from 'react-native';
import GlobalStyles from '../styles/GlobalStyles';
import logoutUser from '../context/actions/logoutUser';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';

const SideMenu = ({navigation, authDispatch}) => {

    const handleLogout = () => {
        navigation.toggleDrawer();
        Alert.alert('Logout', 'Are you sure?', [
            {
                text:'Cancel',
                onPress: () => {},
            },
            {
                text: 'Yes',
                onPress: () => {
                    logoutUser()(authDispatch);
                },
            },
        ]);
    };

    const menuItems = [
        {
            icon: <MaterialIcon name='library-books' size={25} style={{padding:20}} color={'grey'}></MaterialIcon>,
            name: 'My Reservations',
            onPress: () => {
                navigation.navigate('My Reservations');
            }
        },
        {
            icon: <MaterialIcon name='money' size={25} style={{padding:20}} color={'grey'}></MaterialIcon>,
            name: 'My Transactions',
            onPress: () => {
                navigation.navigate('My Transactions');
            }
        },
        {
            icon: <MaterialIcon name='add-circle' size={25} style={{padding:20}} color={'grey'}></MaterialIcon>,
            name: 'Booking',
            onPress: () => {
                navigation.navigate('Booking');
            }
        },
        {
            icon: <MaterialIcon name='person' size={25} style={{padding:20}} color={'grey'}></MaterialIcon>,
            name: 'Profile',
            onPress: () => {
                navigation.navigate('Profile');
            }
        },
        {
            icon: <MaterialIcon name='privacy-tip' size={25} style={{padding:20}} color={'grey'}></MaterialIcon>,
            name: 'Terms & Policy',
            onPress: () => {
                navigation.navigate('Privacy');
            }
        },
        {
            icon: <MaterialIcon name='logout' size={25} style={{padding:20}} color={'grey'}></MaterialIcon>,
            name: 'Logout',
            onPress: handleLogout,
        },
    ]


    return (
        <View style={GlobalStyles.container}>
            <View style={GlobalStyles.logo}>
                <Image source={require('../assets/mammoth-logo2.png')}/>
            </View>

            <View>
                {menuItems.map(({icon, name, onPress}) => (
                    <TouchableOpacity onPress={onPress} key={name} style={{flexDirection: 'row', alignItems: 'center'}}>
                        {icon}
                        <Text style={GlobalStyles.menuItem}>{name}</Text>
                    </TouchableOpacity>
                ))}
            </View>
        </View>
    )
}

export default SideMenu;