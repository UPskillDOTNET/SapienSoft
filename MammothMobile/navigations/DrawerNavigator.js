import React, { useContext } from 'react';
import { createDrawerNavigator } from '@react-navigation/drawer';
import HomeNavigator from './HomeNavigator';
import SideMenu from './SideMenu';
import { GlobalContext } from '../context/provider';

const getDrawerContent = (navigation, authDispatch) => {
    return <SideMenu navigation={navigation} authDispatch={authDispatch}/>;

}

const DrawerNavigator = () => {
    const Drawer = createDrawerNavigator();
    const {authDispatch} = useContext(GlobalContext);
    return(
        <Drawer.Navigator drawerContent={({navigation}) => getDrawerContent(navigation, authDispatch)}>
            <Drawer.Screen name='Home' component={HomeNavigator}></Drawer.Screen>
        </Drawer.Navigator>
    )
}

export default DrawerNavigator;