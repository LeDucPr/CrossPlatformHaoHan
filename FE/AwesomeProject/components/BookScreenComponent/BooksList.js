import React from 'react'
import { Image, StyleSheet, Text, View, FlatList, TouchableOpacity, Dimensions, ActivityIndicator } from 'react-native'
import { useRef, useState, useEffect, } from 'react';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function BooksList({ navigation, datas, name, loadingState }) {
    const [isLoading, setIsLoading] = useState(loadingState);
    useEffect(() => {
        setIsLoading(loadingState);
      }, [loadingState]);
    return (
        <View style={{ marginBottom: Screen_height * 0.02 }}>
            <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', margin: Screen_width * 0.025, }}>
                <Text style={styles.text}>{name}</Text>
                <TouchableOpacity>
                    <Text>More {'>'}</Text>
                </TouchableOpacity>
            </View>
            {isLoading ? (
                <ActivityIndicator size="small" color="gray" />
            ) : (
                <FlatList nestedScrollEnabled data={datas} numColumns={2} renderItem={({ item }) => {
                    return (
                        <TouchableOpacity onPress={() => {
                            navigation.navigate('BookScreen', { item })
                        }}>
                            <View style={styles.container}>
                                <Image style={styles.image} source={{uri: item.coverComicImagePngStrings[0]}} resizeMode="cover" />
                                <Text numberOfLines={1} style={styles.title}>{item.title}</Text>
                            </View>
                        </TouchableOpacity>
                    )
                }} />
            )}
        </View>
    )
}
const styles = StyleSheet.create({
    container: {
        width: Screen_width * 0.45,
        height: Screen_width * 0.4,
        margin: Screen_width * 0.025,
    },
    image: {
        width: '100%',
        height: '80%',
        borderRadius: 10,
    },
    text: {
        fontSize: 23,
        fontWeight: '500'
    }
})