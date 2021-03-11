import React, { useContext, useEffect } from 'react';
import MyReservationsComponent from '../components/reservationsComponent';
import getReservations from '../context/actions/getReservations';
import { GlobalContext } from '../context/provider';

const Reservations = () => {

    const {reservationsDispatch, reservationsState: {getReservations: {data, loading} } } = useContext(GlobalContext)

    useEffect(() => {
        getReservations()(reservationsDispatch)
    }, [])

    return (
        <MyReservationsComponent
            data={data}
            loading={loading}
        />
    )
}

export default Reservations;