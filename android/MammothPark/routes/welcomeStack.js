import { createStackNavigator } from 'react-navigation-stack';
import { createAppContainer, Image } from 'react-navigation';
import Welcome from '../screens/welcome';
import Home from '../screens/home';
import Reservation from '../screens/reservation';

const screens = {
    Welcome: {
        screen: Welcome,
    },
    Home: {
        screen: Home, navigationOptions:  {headerLeft: null, gestureEnabled: false}
    }, 
    Reservation: {
        screen: Reservation
    }
}

const WelcomeStack = createStackNavigator(screens);

export default createAppContainer(WelcomeStack);