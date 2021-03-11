import React, { useContext, useState } from 'react';
import { Text, View } from 'react-native';
import BookingComponent from '../components/bookingComponent';
import getBooking from '../context/actions/getBooking';
import { GlobalContext } from '../context/provider';

const Booking = () => {

    const { bookingDispatch, bookingState: { getBooking: {data, loading} } } = useContext(GlobalContext)

    const [startDateTime, setStartDateTime] = useState(new Date());
    const [endDateTime, setEndDateTime] = useState(new Date());
    const [showStartDatePicker, setShowStartDatePicker] = useState(false);
    const [showStartTimePicker, setShowStartTimePicker] = useState(false);
    const [showEndDatePicker, setShowEndDatePicker] = useState(false);
    const [showEndTimePicker, setShowEndTimePicker] = useState(false);
    const [errors, setErrors] = useState(null);

    const onChangeStart = (event, selectedDate) => {
        setShowStartDatePicker(false);
        setShowStartTimePicker(false);
        const currentDate = selectedDate || startDateTime;
        setStartDateTime(currentDate);
    };

    const onChangeEnd = (event, selectedDate) => {
        const currentDate = selectedDate || endDateTime;
        setEndDateTime(currentDate);
        setShowEndDatePicker(false);
        setShowEndTimePicker(false);
    };

    const openShowStartDatePicker = () => {
        setShowStartDatePicker(true);
    };
    const openShowStartTimePicker = () => {
        setShowStartTimePicker(true);
    };
    const openShowEndDatePicker = () => {
        setShowEndDatePicker(true);
    };
    const openShowEndTimePicker = () => {
        setShowEndTimePicker(true);
    };

    const onSubmit = () => {
        if (startDateTime > endDateTime){
            setErrors('End must be after Start')
        }
        else{
            setErrors(null)
        }
        if (errors === null){
            getBooking(startDateTime, endDateTime)(bookingDispatch)
        }
    }

    return (
        <BookingComponent
            startDateTime={startDateTime}
            endDateTime={endDateTime}
            showStartDatePicker={showStartDatePicker}
            showStartTimePicker={showStartTimePicker}
            showEndDatePicker={showEndDatePicker}
            showEndTimePicker={showEndTimePicker}
            onChangeStart={onChangeStart}
            onChangeEnd={onChangeEnd}
            openShowStartDatePicker={openShowStartDatePicker}
            openShowStartTimePicker={openShowStartTimePicker}
            openShowEndDatePicker={openShowEndDatePicker}
            openShowEndTimePicker={openShowEndTimePicker}
            errors={errors}
            onSubmit={onSubmit}
            data={data}
            loading={loading}
        />
    )
}

export default Booking;