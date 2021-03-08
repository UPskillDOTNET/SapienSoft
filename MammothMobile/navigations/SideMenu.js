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
                text:'Cancel', onPress: () => {},
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
            icon: <MaterialIcon name='library-books' size={25} style={{padding:20}}></MaterialIcon>,
            name: 'My Reservations',
            onPress: () => {
                navigation.navigate('MyReservations');
            }
        },
        {
            icon: <MaterialIcon name='perm-identity' size={25} style={{padding:20}}></MaterialIcon>,
            name: 'Profile',
            onPress: () => {
                navigation.navigate('Profile');
            }
        },
        {
            icon: <MaterialIcon name='logout' size={25} style={{padding:20}}></MaterialIcon>,
            name: 'Logout',
            onPress: handleLogout
        },
    ]


    return (
        <View style={GlobalStyles.container}>
            <View style={GlobalStyles.logo}>
                <Image source={require('../assets/logo2.png')}/>
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