import React from 'react'
import { Image, StyleSheet, Text, View, Dimensions, TouchableOpacity } from 'react-native'

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function BottomBar({ navigation }) {
    return (
        <View style={{ justifyContent: 'flex-end',height: Screen_height * 0.07, }}>
            <View style={styles.container}>
                <TouchableOpacity style={{ justifyContent: 'center' }} onPress={() => navigation.navigate("MainScreen")}>
                    <Text>Main</Text>
                </TouchableOpacity>
                <TouchableOpacity style={{ justifyContent: 'center' }} onPress={() => navigation.navigate("GenresScreen")}>
                    <Text>Genres</Text>
                </TouchableOpacity>
                <TouchableOpacity style={{ justifyContent: 'center' }} onPress={() => navigation.navigate("LibraryScreen")}>
                    <Text>Library</Text> 
                </TouchableOpacity>
                <TouchableOpacity style={{ justifyContent: 'center' }} onPress={() => navigation.navigate("UserSceen")}>
                    <Text>More</Text>
                </TouchableOpacity>
            </View>
        </View>
    )
}
const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        alignContent: 'center',
        paddingVertical: 10,
        backgroundColor: '#FFFCFC',
        borderTopWidth: 1,
        borderTopColor: '#FAF9F9',
    }
})