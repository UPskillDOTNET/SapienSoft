import { StyleSheet } from 'react-native';

export const globalStyles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'column', // default value 'column'
        backgroundColor: '#fff',
        alignItems: 'center', //center horizontal (secondary axis)
        //justifyContent: 'center', //center vertical (main axis, column by default)
        paddingTop: 20
      },
    input: {
        borderWidth: 1,
        borderColor: 'grey',
        padding: 10,
        margin: 15,
        fontSize: 15,
        borderRadius: 6,
        width: '90%',
        alignSelf: 'center'
    },
    button: {
      width: 120,
      borderRadius: 5,
      margin: 10,
      alignSelf: 'center'
    },
    error: {
      color: 'crimson',
      fontWeight: 'bold',
      marginLeft: 15
    },
    backgroundImage: {
      flex: 1,
      resizeMode: "cover",
      justifyContent: "center"
    },
    rowContent: {
      flexDirection: 'row',
      alignItems: 'center'
    }
  });