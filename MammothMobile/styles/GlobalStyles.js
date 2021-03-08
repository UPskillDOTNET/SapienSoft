import { StyleSheet } from "react-native"
import { color } from "react-native-reanimated"

export default StyleSheet.create({
    container: {
        paddingTop: 50,
        paddingHorizontal: 20,
    },
    input: {
        paddingVertical: 5
    },
    textInput: {
        height: 40,
        borderColor: 'gray',
        borderWidth: 1,
        paddingHorizontal: 10,
        borderRadius: 5,
        flex:1
    },
    rowContent:{
        flexDirection: 'row',
        alignItems: 'center'
    },
    logo: {
        paddingVertical: 40,
        alignSelf: 'center'

    },
    buttonWrapper: {
        marginVertical: 20
    },
    textError: {
        paddingLeft: 5,
        paddingTop: 5,
        fontSize: 12,
        color: 'crimson'
    },
    textLink: {
        color: 'dodgerblue'
    },
    label: {
        padding: 5
    },
    menuItem: {
        fontSize:20,
        color:'dodgerblue',
        paddingVertical: 10,
    },
    shortButtonWrapper: {
        marginVertical: 20,
        marginHorizontal: 15,
        alignSelf:'center',
        width:'40%',
    },
    textInputInfo: {
        height: 40,
        borderColor: 'dimgray',
        borderWidth: 1,
        borderRadius: 5,
        paddingHorizontal: 10,
        width: '65%',
        color: 'dimgray'
    }
})