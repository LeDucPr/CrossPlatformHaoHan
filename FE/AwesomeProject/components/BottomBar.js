import React from 'react'
import { Image, StyleSheet, Text, View, Dimensions } from 'react-native'

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function BottomBar({ navigation }) {
    return (
        <View style={{ flex: 1, justifyContent: 'flex-end' }}>
            <View style={styles.container}>
                <View style ={{backgroundColor:'black', justifyContent: 'center'}}>
                    <Text style ={{backgroundColor:'red'}}>Tab 1</Text>
                </View>
                <View style ={{backgroundColor:'black', justifyContent: 'center'}}>
                    <Text style ={{backgroundColor:'red'}}>Tab 2</Text>
                </View>
                <View style ={{backgroundColor:'black', justifyContent: 'center'}}>
                    <Text style ={{backgroundColor:'red'}}>Tab 3</Text>
                </View>
            </View>
        </View>
    )
}
const styles = StyleSheet.create({
    container:{
        height: Screen_height*0.07,
        flexDirection: 'row', 
        justifyContent: 'space-around', 
        alignContent: 'center',
        paddingVertical: 10, 
        backgroundColor: '#FFFCFC',
        borderTopWidth:1,
        borderTopColor: '#FAF9F9',
    }
})