import React from 'react';
import { Button, TextInput, View, Text } from 'react-native';
import { Formik } from 'formik';
import * as yup from 'yup';
import { globalStyles } from '../styles/global';

const reviewSchema = yup.object({
    Email: yup.string().email().required(),
    Password: yup.string().required().min(8)
})

export default function LoginForm({loginUser}) {
    return(
        <View>
            <Formik
                initialValues={ {Email: '', Password: ''} }
                validationSchema={ reviewSchema }
                onSubmit={ (values) => {
                    console.log(values);
                    loginUser(values);
                } }
            >
                { (props) => (
                    <View>
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