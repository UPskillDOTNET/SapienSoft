import React, { useContext, useState } from 'react';
import LoginComponent from '../components/loginComponent';
import { GlobalContext } from '../context/provider';
import { useNavigation } from '@react-navigation/core';
import login from '../context/actions/login';

const Login = () => {

    const [form, setForm] = useState({});
    const [errors, setErrors] = useState({});
    const { authDispatch, authState: {error, loading, data} } = useContext(GlobalContext)
    const { navigate } = useNavigation();

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
            Object.values(form).length === 2 &&
            Object.values(form).every((item) => item.trim().length > 0) &&
            Object.values(errors).every((item) => !item)
        ) {
            console.log('form: ', form)
            login(form)(authDispatch);
        }

    };

    return (
        <LoginComponent
            form={form}
            errors={errors}
            onChange={onChange}
            onSubmit={onSubmit}
            error={error}
            loading={loading}
        />
    )
}

export default Login;