import { Text, View, StyleSheet, TextInput, TouchableOpacity, Image, Button, ImageBackground, Dimensions, Pressable } from 'react-native';
import * as React from 'react';
import { NavigationContainer, DefaultTheme, } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs'
import LoginScreen from './Screens/LoginScreen';
import RegisterScreen from './Screens/RegisterScreen';
import DashBoardScreen from './Screens/DashBoardScreen';
import MainScreen from './Screens/MainScreen';
import SearchScreen from './Screens/SearchScreen';
import BookScreen from './Screens/BookScreen';
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
  var baseUrl = "https://localhost:7188/Book/GetIntroById";
  var bookId = "0001";
  var url = baseUrl + "?bookId=" + bookId;
  fetch(url)
    .then((res) => res.json())
    .then((data) => {
      console.log(data);
    })
  return (
    <NavigationContainer theme={MyTheme}>
      <Stack.Navigator screenOptions={{
        headerShown: false,
      }} initialRouteName='LoginScreen'>
        <Stack.Screen name="MainScreen" component={MainScreen} />
        <Stack.Screen name="DashBoard" component={DashBoardScreen}  />   
        <Stack.Screen name="LoginScreen" component={LoginScreen}  />
          <Stack.Screen name="Register" component={RegisterScreen} />
        <Stack.Screen name="Search" component={SearchScreen} />
        <Stack.Screen name="BookScreen" component={BookScreen} />
      </Stack.Navigator>
    </NavigationContainer>


  );
}














