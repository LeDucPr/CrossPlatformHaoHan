import { Text, SafeAreaView, StyleSheet, View, Image, TouchableOpacity, StatusBar, FlatList, Dimensions} from 'react-native';

import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { setGenre } from '../../app/slices/genresBkSlice';

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function GenresType({navigation}) {
    const GenresType = ['Action', 'Adventure', 'Adult', 'Drama', 'Ecchi', 'Fantasy', 'Horror', 'Harem', 'Isekai', 'Romance', 'Historical', 'Sports']
    const dispatch = useDispatch();
    const handleOnPress = (genre) => {
        dispatch(setGenre(genre));
      }; 
      
  return (
    <View style = {styles.container}>
        <FlatList nestedScrollEnabled data={GenresType}  numColumns={4} renderItem={({ item }) => {
                    return (
                        <TouchableOpacity style={styles.genresBtn}  onPress={() => handleOnPress(item)}>
                            <View style={styles.container}>
                                <Text numberOfLines={1} style={styles.title}>{item}</Text>
                            </View>
                        </TouchableOpacity>
                    )
                }} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    padding: 5,
  },
  title: {
    fontSize: 13,
    fontWeight: '500'
  },
  genresBtn:{
    paddingLeft: 6,
    paddingRight: 6,
    paddingTop: 2,
    paddingBottom: 2,
    backgroundColor: '#F7F7F7',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 25,
    marginTop: 3,
    marginBottom: 3,
    marginLeft: 5,
    marginRight: 5,
  }
});
