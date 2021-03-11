import { useFocusEffect, useNavigation } from '@react-navigation/core';
import React, { useCallback, useContext, useEffect, useState } from 'react';
import RegisterComponent from '../components/registerComponent';
import register, { clearAuthState } from '../context/actions/register';
import { GlobalContext } from '../context/provider';

const Register = () => {

    const [form, setForm] = useState({});
    const [errors, setErrors] = useState({});
    const { authDispatch, authState: {error, loading, data} } = useContext(GlobalContext)
    const { navigate } = useNavigation();



    useFocusEffect(
        useCallback(() => {
            return () => {
                if (data || error) {
                    clearAuthState()(authDispatch);
                }
            };
        }, [data, error]),
    );

    const onChange = ({name, value}) => {
        setForm({...form, [name]: value});

        if(value !== '') {
            if (name === 'Password') {
                if (value.length < 8) {
                    setErrors(prev => {
                        return {...prev, [name]: 'Password must be 8 characters'}
                    })
                }
                else if (!/[A-Z]/.test(form.Password)){
                    setErrors(prev => {
                        return {...prev, [name]: 'Password must have at least one capital letter'}
                    })
                }
                else if (!/[a-z]/.test(form.Password)){
                    setErrors(prev => {
                        return {...prev, [name]: 'Password must have at least one lowercase letter'}
                    })
                }
                else if (!/[0-9]/.test(form.Password)){
                    setErrors(prev => {
                        return {...prev, [name]: 'Password must have at least one number'}
                    })
                }
                else {
                    setErrors(prev => {
                        return {...prev, [name]: null}
                    })
                }
            }
            else if (name === 'Email'){
                if (!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(form.Email)) {
                    setErrors(prev => {
                        return {...prev, [name]: 'Must be a valid Email'}
                    })
                }
                else {
                    setErrors(prev => {
                        return {...prev, [name]: null}
                    })
                }
            }
        }
        else {
            setErrors(prev => {
                return {...prev, [name]: 'This filed is required'}
            })
        }
    }
    
    const onSubmit = () => {
        if(!form.FirstName){
            setErrors(prev => {
                return {...prev, FirstName: 'Please add a First Name'}
            })
        }
        if(!form.LastName){
            setErrors(prev => {
                return {...prev, LastName: 'Please add a Last Name'}
            })
        }
        if(!form.UserName){
            setErrors(prev => {
                return {...prev, UserName: 'Please add a username'}
            })
        }
        if(!form.Email){
            setErrors(prev => {
                return {...prev, Email: 'Please add a Email'}
            })
        }
        if(!form.Password){
            setErrors(prev => {
                return {...prev, Password: 'Please add a Password'}
            })
        }

        if (
            Object.values(form).length === 5 &&
            Object.values(form).every((item) => item.trim().length > 0) &&
            Object.values(errors).every((item) => !item)
        ) {
            register(form)(authDispatch)((response) =>{
                navigate('Login', {data: form.Email});
            });
        }

    };
    return (
        <RegisterComponent
            form={form}
            errors={errors}
            onChange={onChange}
            onSubmit={onSubmit}
            error={error}
            loading={loading}
        />
    )
}

export default Register;