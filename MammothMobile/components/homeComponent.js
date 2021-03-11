import { useNavigation } from '@react-navigation/core';
import React from 'react';
import { Button, Text } from 'react-native';
import { Card } from 'react-native-elements'
import { ScrollView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';

const HomeComponent = ({ form, errors, onChange, onSubmit, error, loading, justSignedUp }) => {

    const {navigate} = useNavigation();

    return (
        <ScrollView style={{backgroundColor: 'white'}}>
            <Card>
                <Card.Title>WHAT'S NEW</Card.Title>
                <Card.Image source={require('../assets/img1.png')} resizeMode='cover'></Card.Image>
                <Text style={{marginBottom: 10}}>
                    Now with Mammoth, we'll take you to your final destination. We're using Google
                    Maps (on Android) and Maps (on IOS) to to drive you to your favourite parking space.
                    Start making reservations today and enjoy a 10% discount on your first booking.
                </Text>
                <Button
                    buttonStyle={{borderRadius: 0, marginLeft: 0, marginRight: 0, marginBottom: 0}}
                    title='VIEW NOW'
                    onPress={()=>{navigate('My Reservations')}}
                />           
            </Card>
            <Card>
                <Card.Title>ONLY FOR OUR BEST CLIENTS</Card.Title>
                <Card.Image source={require('../assets/img2.png')} resizeMode='cover'></Card.Image>
                <Text style={{marginBottom: 10}}>
                    What if you could always find a parking slot at your destination? Become a Gold Member
                    and find out today our many reawrds for our best Clients. We garantee you a parking space
                    at your favourite park, with the best price in town.
                </Text>
                <Button
                    buttonStyle={{borderRadius: 0, marginLeft: 0, marginRight: 0, marginBottom: 0}}
                    title='I WANT TO BE SPECIAL'
                />           
            </Card>
            <Card>
                <Card.Title>SUSTAINABILITY</Card.Title>
                <Card.Image source={require('../assets/img3.png')} resizeMode='cover'></Card.Image>
                <Text style={{marginBottom: 10}}>
                At Mammoth we focus on meeting the needs of the present without compromising the ability of future
                generations to meet their needs. We have three main objectives: economic, environmental, and social.
                We also have a challenge for you...
                </Text>
                <Button
                    buttonStyle={{borderRadius: 0, marginLeft: 0, marginRight: 0, marginBottom: 0}}
                    title='Want to help'
                />           
            </Card>
            <Card>
                <Card.Title>BOOKING</Card.Title>
                <Card.Image source={require('../assets/img4.png')} resizeMode='cover'></Card.Image>
                <Text style={{marginBottom: 10}}>
                    Booking a parking slot is now easier than ever. It takes you less than a minute. Try now?
                </Text>
                <Button
                    buttonStyle={{borderRadius: 0, marginLeft: 0, marginRight: 0, marginBottom: 0}}
                    title='Booking'
                />           
            </Card>
                                

        </ScrollView>

        
    )
}

export default HomeComponent;