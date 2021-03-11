import AsyncStorage from "@react-native-async-storage/async-storage";
import { LOGOUT_USER } from "../../constants/actionTypes";

removeAsyncStorage = async () => {
    try {
        await AsyncStorage.removeItem('token');
        await AsyncStorage.removeItem('username');
    } catch(e) {
        console.log('asyncstorage_error_token: ', e)
    }
    
    console.log('Done.')
  }

export default () => (dispatch) => {
    removeAsyncStorage();
    dispatch({
        type: LOGOUT_USER,
    });
};