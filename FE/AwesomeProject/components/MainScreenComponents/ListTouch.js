import { Text, SafeAreaView, StyleSheet, View, Image, TouchableOpacity, StatusBar } from 'react-native';

import React, { useState } from 'react';



export default function ListTouch({navigation}) {
  return (
    <View style = {styles.container}>
       <View style = {{ justifyContent:'space-around', flexDirection: 'row'}}>
            <TouchableOpacity style = {{ justifyContent:'center',}} onPress={() => navigation.navigate("NewScreen")}>
              <Image source={require('../../assets/newicon.png')} style={styles.icon} reszieMode = 'contain' />
            </TouchableOpacity>
            <TouchableOpacity style = {{ justifyContent:'center'}} onPress={() => navigation.navigate("RankingScreen")}>
              <Image source={require('../../assets/rankingicon.png')} style = {styles.icon} reszieMode = 'contain'/>
            </TouchableOpacity>
          </View>
    </View>
  );
}


const styles = StyleSheet.create({
    container: {
      width:  '100%',
      height: '2%',
      padding: 5,
      backgroundColor: '#FFFCFC',
      borderBottomWidth:1,
      borderBottomColor: '#FAF9F9',
      //justifyContent: 'space-around'
    },
    icon: {
      width: 40,
      height: 40,
      margin: 5,
      resizeMode: 'contain',
    },
  });

