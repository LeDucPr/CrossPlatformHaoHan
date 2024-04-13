import { Text, SafeAreaView, StyleSheet, View, Image, TouchableOpacity, StatusBar, Dimensions } from 'react-native';
import React, { useState } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function UserScreenTop({navigation, userData }) {
    return (
        <View style={styles.container}>
                <View style={styles.row}>
                <Image source={require('../../assets/usericon.png')} style={styles.icon} reszieMode='contain' />
                </View>
                <View style={styles.row}>
                    <Text style={styles.nametxt}>{userData.lastNameAccount} {userData.firstNameAccount} </Text>
                </View>
                <TouchableOpacity  style={[styles.row]}>
                    <Text>{'>'}</Text>
                </TouchableOpacity>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        height: Screen_height * 0.15,
        flexDirection: 'row',
        alignContent: 'center',
        paddingVertical: 10,
        paddingLeft: 20,
        backgroundColor: '#FFFCFC',
        borderBottomWidth: 1,
        borderBottomColor: '#F9F5F5',
    },
    row : {
        marginRight:20,
        justifyContent: 'center',
    },
    icon: {
        width: Screen_height*0.10,
        height: Screen_height*0.10,
        borderRadius: 20,
    },
    nametxt:{
        fontSize: Screen_height*0.03
    }
});
