import {LogBox} from 'react-native';
import React, { useRef, useState, useEffect, } from 'react';
import { NavigationContainer, DefaultTheme, } from '@react-navigation/native';
import AppNavigator from './AppNavigator';

const MyTheme = {
  ...DefaultTheme,
  colors: {
    ...DefaultTheme.colors,
    primary: 'rgb(255, 45, 85)',
    background: 'transparent',
  },
};
export default function App() {
  useEffect(() => {
    LogBox.ignoreLogs(['VirtualizedLists should never be nested inside plain ScrollViews', "Cannot read property 'scrollToIndex' of undefined","AxiosError: Request failed with status code 400"]);
  }, []);
  return (
    <NavigationContainer theme={MyTheme}>
      <AppNavigator />
    </NavigationContainer>
  );
}














