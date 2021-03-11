import React, { useEffect } from 'react';
import { useState } from 'react';
import { useContext } from 'react';
import ProfileComponent from '../components/profileComponent';
import getUser from '../context/actions/getUser';
import { GlobalContext } from '../context/provider';

const Profile = () => {

    const {userDispatch, userState: { getUser: {data, loading} } } = useContext(GlobalContext)
    const [editing, setEditing] = useState(false);

    const [form, setForm] = useState({});

    console.log(form.FirstName)

    useEffect(()=> {
        getUser()(userDispatch);
    }, []);

    const handleEditing = () => {
        setEditing(true)
    }

    const handleSaveChanges = () => {
        setEditing(false)
        setFirstName()
    }

    const onChange = ({name, value}) => {
        setForm({...form, [name]: value});
    }

    return (
        <ProfileComponent 
            data={data}
            loading={loading}
            editing={editing}
            handleEditing={handleEditing}
            handleSaveChanges={handleSaveChanges}
            form={form}
            onChange={onChange}
        />
    )
}

export default Profile;