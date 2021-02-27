import React, { useState } from 'react';
import { Text, View, Button, FlatList, Modal, TouchableWithoutFeedback, TouchableOpacity, Keyboard, Alert } from 'react-native';
import { globalStyles } from '../styles/global';
import DatesForm from './datesForm';


export default function Reservation() {

    const [modalOpen, setModalOpen] = useState(false);
    const [reservations, setReservations] = useState([]);

    const pressHandler = (id) => {
        console.log(id)
    }

    async function getAvailable(dates) {
        console.log('chegou a funcao)');
        var query = '?start=' + dates.StartDate + 'T'
        console.log(query);
        try {
            let response = await fetch('https://webhook.site/553c9d26-38c1-416c-ad30-bd57c3ea455f', {
                method: 'POST',
                headers: { 'Accept': 'application/json', 'Content-type': 'application/json'},
                body: dates
            });
            if (response.status == '200'){
                console.log(response.headers);
                var reservations = [
                    {
                        "dateCreated": "2021-02-25T17:43:36.5272484+00:00",
                        "value": 48,
                        "parkName": "Public Park",
                        "slotId": 1, "locator": "A01",
                        "latitude": 41.17863016,
                        "longitude": -8.60897769
                    },
                    {
                        "dateCreated": "2021-02-25T17:43:36.5274085+00:00",
                        "value": 48,
                        "parkName": "Public Park",
                        "slotId": 2, "locator": "A02",
                        "latitude": 41.17649696,
                        "longitude": -8.60555505
                    },
                    {
                        "dateCreated": "2021-02-25T17:43:36.5274101+00:00",
                        "value": 48,
                        "parkName": "Public Park",
                        "slotId": 3,
                        "locator": "A03",
                        "latitude": 41.17851442,
                        "longitude": -8.59592681
                    },
                    {
                        "dateCreated": "2021-02-25T17:43:36.5274104+00:00",
                        "value": 48,
                        "parkName": "Public Park",
                        "slotId": 4,
                        "locator": "A04",
                        "latitude": 41.18163863,
                        "longitude": -8.60108298,
                    },
                    {
                        "dateCreated": "2021-02-25T17:43:36.5274107+00:00",
                        "value": 48,
                        "parkName": "Public Park",
                        "slotId": 5,
                        "locator": "A05",
                        "latitude": 41.17538811,
                        "longitude": -8.59880131,
                    }
                ]
                setReservations(reservations);
                setModalOpen(false);
            }
        } catch (error) {
          Alert.alert('Error', error)
          console.log(error);
        }
    };


    FlatListItemSeparator = () => {
        return (
            <View
                style={{
                height: 1,
                width: "100%",
                backgroundColor: "#000",
                }}
            />
        );
    }

    return (
        <View style={globalStyles.container}>
            <Text>Let's search for a slot!</Text>
            
            {/* Modal */}
            <Modal visible={modalOpen} animationType='slide'>
                <TouchableWithoutFeedback onPress={Keyboard.dismiss} >
                <View>
                    <DatesForm getAvailable={getAvailable}/>
                    <View style={globalStyles.button}>
                    <Button title={'Cancel'} onPress={() => setModalOpen(false)} />
                    </View>
                </View>
                </TouchableWithoutFeedback>
            </Modal>


            <View style={globalStyles.button}>
                <Button title={'Get Available'} onPress={() => setModalOpen(true)} />
            </View>

            <View >
                <FlatList
                    keyExtractor={(item) => item.locator}
                    data={reservations}
                    ItemSeparatorComponent = { FlatListItemSeparator }
                    renderItem={({item}) => (
                        <TouchableOpacity onPress={() => pressHandler(item.locator)} >
                            <Text>Park Name: {item.parkName}</Text>
                            <Text>Slot Id: {item.slotId}</Text>
                            <Text>Slot Locator: {item.locator}</Text>
                            <Text>Latitude: {item.latitude}</Text>
                            <Text>Longitude: {item.longitude}</Text>
                            <Text>Value: {item.value} €</Text>
                        </TouchableOpacity>
                    )}
                />
            </View>
        </View>
    )
}