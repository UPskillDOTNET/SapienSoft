import React from 'react';
import { Button, TextInput, View, Text } from 'react-native';
import { Formik } from 'formik';
import * as yup from 'yup';
import { globalStyles } from '../styles/global';

const reviewSchema = yup.object({
    FirstName: yup.string().required().min(4),
    LastName: yup.string().required().min(4),
    UserName: yup.string().required().min(4),
    Email: yup.string().email().required(),
    Password: yup.string().required().min(8)
})

export default function RegisterForm({registerUser}) {
    return(
        <View>
            <Formik
                initialValues={ {FirstName: '', LastName: '', UserName: '', Email: '', Password: ''} }
                validationSchema={ reviewSchema }
                onSubmit={ (values) => {
                    console.log(values);
                    registerUser(values);
                } }
            >
                { (props) => (
                    <View>
                        <TextInput
                            style={ globalStyles.input }
                            placeholder='First Name'
                            onChangeText={ props.handleChange('FirstName') }
                            value={ props.values.FirstName }
                            onBlur={ props.handleBlur('FirstName') }
                        />
                        <Text style={ globalStyles.error }>{ props.touched.FirstName && props.errors.FirstName }</Text>
                        <TextInput
                            style={ globalStyles.input }
                            placeholder='Last Name'
                            onChangeText={ props.handleChange('LastName') }
                            value={ props.values.LastName }
                            onBlur={ props.handleBlur('LastName') }
                        />
                        <Text style={ globalStyles.error }>{ props.touched.LastName && props.errors.LastName }</Text>
                        <TextInput 
                            style={ globalStyles.input }
                            placeholder='Username'
                            onChangeText={ props.handleChange('UserName') }
                            value={ props.values.UserName }
                            onBlur={ props.handleBlur('UserName') }
                        />
                        <Text style={ globalStyles.error }>{ props.touched.UserName && props.errors.UserName }</Text>
                        <TextInput
                            style={ globalStyles.input }
                            placeholder='Email'
                            onChangeText={ props.handleChange('Email') }
                            value={ props.values.Email }
                            onBlur={props.handleBlur('Email')}
                        />
                        <Text style={ globalStyles.error }>{ props.touched.Email && props.errors.Email }</Text>
                        <TextInput
                            style={ globalStyles.input }
                            placeholder='Password'
                            onChangeText={ props.handleChange('Password') }
                            value={ props.values.Password }
                            onBlur={ props.handleBlur('Password') }
                            secureTextEntry={ true }
                        />
                        <Text style={ globalStyles.error }>{ props.touched.Password && props.errors.Password }</Text>
                        <View style={ globalStyles.button }>
                            <Button title='Submit' onPress={ props.handleSubmit }/>
                        </View>
                    </View>
                ) }
            </Formik>
        </View>
    )
}