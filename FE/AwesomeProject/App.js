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
import fetchIntroData from './fetchData/FetchCoverById';
import Datas from './data';
import fetchCoversDataFromFieldsContrains from './fetchData/FetchFromFields';
import getImagesForBook from './fetchData/FetchImagesById';

const Stack = createNativeStackNavigator()
//const Stack = createBottomTabNavigator()
const navTheme = {
  ...DefaultTheme,
  colors: {
    ...DefaultTheme.colors,
    background: 'transparent',
  },
};

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
    const fetchData = async () => {
      try {
        const amountCovers = 4;
        const skipIds = [];
        const fields = {
          title: "Sword"
        };
        const amountPages = 0
        const data = await fetchCoversDataFromFieldsContrains(5, amountCovers, skipIds, fields);
        console.log(data);
      } catch (error) {
        console.error('Error fetching intro data:', error);
      }
    };
    fetchData();
  }, []);

  return (
    <NavigationContainer theme={MyTheme}>
      <Stack.Navigator screenOptions={{
        headerShown: false,
      }} initialRouteName='MainScreen'>
        <Stack.Screen name="MainScreen" component={MainScreen} />
        {/* <Stack.Screen name="DashBoard" component={DashBoardScreen}  />    */}
        {/* <Stack.Screen name="LoginScreen" component={LoginScreen}  />
          <Stack.Screen name="Register" component={RegisterScreen} /> */}
        <Stack.Screen name="Search" component={SearchScreen} />
        <Stack.Screen name="BookScreen" component={BookScreen} />
        <Stack.Screen name="ReadScreen" component={ReadScreen} />
      </Stack.Navigator>
    </NavigationContainer>


  );
}














