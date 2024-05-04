import React, { useRef, useState, useEffect, } from 'react';
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
import RankngScreen from './Screens/RankingScreen';
import NewScreen from './Screens/NewScreen';


const Stack = createNativeStackNavigator();

export default function AppNavigator() {
  return (
    <Stack.Navigator screenOptions={{
        headerShown: false,
      }} initialRouteName='MainScreen'>
        <Stack.Screen name="MainScreen" component={MainScreen} />
        <Stack.Screen name="DashBoard" component={DashBoardScreen}  />   
        <Stack.Screen name="LoginScreen" component={LoginScreen}  />
        <Stack.Screen name="Register" component={RegisterScreen} />
        <Stack.Screen name="Search" component={SearchScreen} />
        <Stack.Screen name="BookScreen" component={BookScreen} />
        <Stack.Screen name="ReadScreen" component={ReadScreen} />
        <Stack.Screen name="UserSceen" component={UserSceen}/>
        <Stack.Screen name="LibraryScreen" component={LibraryScreen}/>
        <Stack.Screen name="RankingScreen" component={RankngScreen}/>
        <Stack.Screen name="NewScreen" component={NewScreen}/>
      </Stack.Navigator>
  );
}