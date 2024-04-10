import {
  Text,
  View,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  ImageBackground, SafeAreaView, Dimensions, Image, ScrollView,
} from 'react-native';
import React, {useRef, useState, useEffect,}   from 'react';
import Slider from '../components/MainScreenComponents/Slider';
import Datas from '../data';
import MainScreenTop from '../components/MainScreenComponents/MaiScreenTop';
import BooksList from '../components/BookScreenComponent/BooksList';
import BottomBar from '../components/BottomBar';
import fetchIntroData from '../fetchData/FetchCoverById';

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function MainScreen({ navigation }) {
  const [introDatas, setIntroData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await fetchIntroData(8);
        setIntroData(data);
        setIsLoading(false);
      } catch (error) {
        console.error('Error fetching intro data:', error);
      }
    }; 
    fetchData();
  }, []);
  return (
    <SafeAreaView style={styles.container}>
      <MainScreenTop navigation={navigation} Datas={introDatas} />
      <ScrollView>
        <Slider navigation={navigation} datas={introDatas} loadingState={isLoading} />
        <BooksList navigation={navigation} datas={introDatas} name={'Daily Picks'} loadingState={isLoading} />
        <BooksList navigation={navigation} datas={introDatas} name={'What You Might Like'} loadingState={isLoading} />
      </ScrollView>
      <BottomBar navigation={navigation} />
    </SafeAreaView>

  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    width: Screen_width,
    height: Screen_height,
    backgroundColor: '#FFFDFD'
  }
})
