import 'react-native-gesture-handler';
import React from 'react';
import AppNavContainer from './navigations';
import GlobalProvider from './context/provider';

export default function App() {
  return (
    <GlobalProvider>
      <AppNavContainer />
    </GlobalProvider>
  );
}