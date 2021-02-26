import React from 'react';
import { Button, TextInput, View, Text } from 'react-native';
import { Formik } from 'formik';
import * as yup from 'yup';
import { globalStyles } from '../styles/global';

const reviewSchema = yup.object({
    StartDate: yup.date().required().min(new Date()),
    EndDate: yup.date().required().min(new Date()),
})

export default function DatesForm({getAvailable}) {
    return(
        <View>
            <Formik
                initialValues={ {StartDate: '', StartTime: ''} }
                validationSchema={ reviewSchema }
                onSubmit={ (values) => {
                    console.log(values);
                    getAvailable(values);
                } }
            >
                { (props) => (
                    <View>
                        <TextInput
                            style={ globalStyles.input }
                            placeholder='Start date (ex. 2021-03-09T05:00:00)'
                            onChangeText={ props.handleChange('StartDate') }
                            value={ props.values.StartDate }
                            onBlur={ props.handleBlur('StartDate') }
                        />
                        <Text style={ globalStyles.error }>{ props.touched.StartDate && props.errors.StartDate }</Text>
                        <TextInput
                            style={ globalStyles.input }
                            placeholder='End date (ex. 2021-03-09T05:00:00)'
                            onChangeText={ props.handleChange('EndDate') }
                            value={ props.values.EndDate }
                            onBlur={ props.handleBlur('EndDate') }
                        />
                        <Text style={ globalStyles.error }>{ props.touched.EndDate && props.errors.EndDate }</Text>

                        <View style={ globalStyles.button }>
                            <Button title='Submit' onPress={ props.handleSubmit }/>
                        </View>
                    </View>
                ) }
            </Formik>
        </View>
    )
}