import { Text, View, StyleSheet, TextInput, TouchableOpacity, Image, Button, ImageBackground, Dimensions, Pressable } from 'react-native';
import React, { useRef, useState, useEffect, } from 'react';
import { NavigationContainer, DefaultTheme, } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs'
import LoginScreen from './Screens/LoginScreen';
import RegisterScreen from './Screens/RegisterScreen';
import DashBoardScreen from './Screens/DashBoardScreen';
import MainScreen from './Screens/MainScreen';
import SearchScreen from './Screens/SearchScreen';
import BookScreen from './Screens/BookScreen';
import ReadScreen from './Screens/ReadScreen';
import UserSceen from './Screens/UserScreen';
import LibraryScreen from './Screens/LibraryScreen';
import fetchIntroData from './fetchData/FetchCoverById';
import Datas from './data';
import fetchCoversDataFromFieldsContrains from './fetchData/FetchFromFields';
import getImagesForBook from './fetchData/FetchImagesById';
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
  return (
    <NavigationContainer theme={MyTheme}>
      <AppNavigator />
    </NavigationContainer>


  );
}














