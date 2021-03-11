import React from 'react';
import { ActivityIndicator, Text, View, FlatList, Image } from 'react-native';
import { TouchableOpacity } from 'react-native-gesture-handler';
import GlobalStyles from '../styles/GlobalStyles';
import MaterialIcon from 'react-native-vector-icons/MaterialIcons';

const TransactionsComponent = ({ data, loading }) => {

    const ListEmptyComponent = () => {
        return (
            <View>
                <Text style={{alignSelf:'center', color: 'darkorange', paddingVertical: 50}}>You don't have any transactions.</Text>
            </View>
        )
    }



    console.log(data)
    const renderItem = ({item}) => {
        return (
            <TouchableOpacity>
                <View style={{paddingVertical:15, flexDirection: 'row', alignItems: 'center'}}>
                    <View style={{padding:5}}>
                        

                    
                        <View style={{flexDirection: 'row', flex: 1}}>
                            <View style={{width: '40%'}}>
                                <Text style={{alignSelf: 'center'}}>{new Date(item.date).toDateString()}</Text>
                            </View>
                            
                            <View style={{width: '30%'}}>
                                {item.value > 0 ?
                                <Text style={{alignSelf: 'center'}}>{item.value.toFixed(2)} €</Text> :
                                <Text style={{color: 'crimson', alignSelf: 'center'}}>{item.value.toFixed(2)} €</Text> }
                            </View>
                            <View style={{width: '30%'}}>
                                <Text style={{alignSelf: 'center'}}>{item.balance.toFixed(2)} €</Text>
                            </View>
                            
                            <View>
                                
                            </View>
                        </View>
                        <View style={{width: '40%'}}>
                            <Text style={{fontSize: 11, alignSelf: 'center'}}>{item.transactionType.name}</Text>
                        </View>
                        

                    </View>
                </View>
            </TouchableOpacity>
        )
    }
    return (
        
        <View style={GlobalStyles.stackScreen}>
            <View style={GlobalStyles.rowContent}>
                <View style={{width: '40%'}}>
                    <Text style={{fontSize: 20, color: 'dodgerblue', paddingVertical: 20, alignSelf: 'center'}}>Date</Text>
                </View>
                                
                <View style={{width: '30%'}}>
                    <Text style={{fontSize: 20, color: 'dodgerblue', paddingVertical: 20, alignSelf: 'center'}}>Value</Text>
                </View>
                <View style={{width: '30%'}}>
                    <Text style={{fontSize: 20, color: 'dodgerblue', paddingVertical: 20, alignSelf: 'center'}}>Balance</Text>
                </View>
            </View>
            {loading && <View style={GlobalStyles.loading}><ActivityIndicator size="large" color='dodgerblue' /></View>}
            {!loading && <FlatList
                data={data.sort(((a, b) => (a.date < b.date) ? 1 : -1))}
                keyExtractor={(item)=> String(item.date)}
                renderItem={renderItem}
                ItemSeparatorComponent={() => (<View style={{height: 1, width: "100%", backgroundColor: "darkorange"}}></View>)}
                ListEmptyComponent={ListEmptyComponent}
            />}

        </View>

    )
}

export default TransactionsComponent;