import { Text, SafeAreaView, StyleSheet, View, Image, TouchableOpacity, StatusBar } from 'react-native';

import React, { useState } from 'react';



export default function LibraryScreenTop({navigation}) {
  return (
    <View style = {styles.container}>
        <View style = {styles.TopSection}>
          <Image source={require('../../assets/weblogo.png')} style={styles.image} resizeMode="contain" />
        </View>
        <View style = {styles.BotSection}>
          <View style ={{justifyContent: 'center'}}>
            <Text style = {{fontSize:26, fontWeight:500, }}>Library</Text>
          </View>
        </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    width:  '100%',
    height: '15%',
    padding: 5,
    backgroundColor: '#FFFCFC',
    borderBottomWidth:1,
    borderBottomColor: '#FAF9F9',
  },
  TopSection:{
    flex: 0.7,
    justifyContent: 'center',
    alignItems: 'center',
  },
  BotSection:{
    flex:0.4,
    alignContent: 'center',
    flexDirection: 'row',
    justifyContent:'space-between',
  },
  image: {
    width: '50%',
    height: '100%', 
  },
  icon: {
    width: 30,
    height: 30,
    margin: 5,
    resizeMode: 'contain',
  },
});
