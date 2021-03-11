import React, { useContext, useEffect } from 'react';
import TransactionsComponent from '../components/transactionsComponent';
import getTransactions from '../context/actions/getTransactions';
import { GlobalContext } from '../context/provider';

const Transactions = () => {

    const {transactionsDispatch, transactionsState: {getTransactions: {data, loading} } } = useContext(GlobalContext)

    useEffect(() => {
        getTransactions()(transactionsDispatch)
    }, [])

    return (
        <TransactionsComponent
            data={data}
            loading={loading}
        />
    )
}

export default Transactions;