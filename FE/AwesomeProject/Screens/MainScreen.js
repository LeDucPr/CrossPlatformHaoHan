import {
  Text,
  View,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  ImageBackground, SafeAreaView, Dimensions, Image, ScrollView,
} from 'react-native';
import React, { useRef, useState, useEffect, } from 'react';
import Slider from '../components/MainScreenComponents/Slider';
import Datas from '../data';
import MainScreenTop from '../components/MainScreenComponents/MaiScreenTop';
import ListTouch from '../components/MainScreenComponents/ListTouch';
import BooksList from '../components/BookScreenComponent/BooksList';
import BottomBar from '../components/BottomBar';
import fetchIntroData from '../fetchData/FetchCoverById';
import AsyncStorage from '@react-native-async-storage/async-storage';
import BooksSuggestionList from '../components/BookScreenComponent/BooksSuggestionList';
import getUserSuggestion from '../fetchData/FetchClientSuggestionBook';
import fetchCoverDatas from '../fetchData/FetchCoverByListId';
import { booksDefault } from '../SetUp';
import { useSelector, useDispatch } from 'react-redux';
import { FetchBooksIdsSuggestion, FetchBooksSuggestion } from '../app/slices/suggestionSlice';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function MainScreen({ navigation }) {
  const [introDatas, setIntroData] = useState([]);
  const [isLoadingIntro, setIsLoadingIntro] = useState(true);
  const [userData, setUserData] = useState();


  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await fetchIntroData(booksDefault);
        setIntroData(data);
        setIsLoadingIntro(false);
      } catch (error) {
        console.error('Error fetching intro data:', error);
      }
    };
    fetchData();
  }, []);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const data = await AsyncStorage.getItem('userData');
        if (data) {
          setUserData(JSON.parse(data));
        }
      } catch (error) {
        console.error('Lỗi khi truy xuất dữ liệu từ AsyncStorage:', error);
      }
    };

    fetchUserData();
  }, []);

  const dispatch = useDispatch();
  const SuggesitonBookIds = useSelector(state => state.suggestionList.ListSuggestionIds);
  const ListSuggesitonBook = useSelector(state => state.suggestionList.ListSuggestionBooks);
  const isLoadingSuggest = useSelector(state => state.suggestionList.isLoading);
  const isFetchAll = useSelector(state => state.suggestionList.isFetchAll);


  useEffect(() => {
    if (userData && !isFetchAll) {
      dispatch(FetchBooksIdsSuggestion({ clientId: userData.id }))
    }
    console.log(userData)
  }, [userData]);

  useEffect(() => {
    if (SuggesitonBookIds && SuggesitonBookIds.length > 0 && isFetchAll) {
      dispatch(FetchBooksSuggestion());
    }
  }, [SuggesitonBookIds]);


  //   useEffect(() => {
  //     const fetchData = async () => {
  //       if (userData) {
  //         try {
  //           const ListSuggestionBookId = await getUserSuggestion(userData.id);
  //           const uniqueData = ListSuggestionBookId.filter((value, index, self) => {
  //             return self.indexOf(value) === index;
  //           });
  //           setSuggestionBookIds(uniqueData)
  //         } catch (error) {
  //           console.error('Error fetching intro data:', error);
  //         }
  //       }
  //     };
  //     fetchData();
  //   }, [userData]);

  //   useEffect(() => {
  //     const fetchData = async (ListBookId) => {
  //         try {
  //             const data = await fetchCoverDatas(ListBookId);
  //             setSuggestionDatas(data);
  //             setIsLoadingSuggest(false);
  //         } catch (error) {
  //             console.error('Error fetching intro data:', error);
  //         }
  //     };
  //     if (SuggesitonBookIds && SuggesitonBookIds.length > 0) {
  //         fetchData(SuggesitonBookIds)
  //     }
  // }, [SuggesitonBookIds]);


  return (
    <SafeAreaView style={styles.container}>
      <MainScreenTop navigation={navigation} />
      <ScrollView style={{ paddingBottom: Screen_height * 0.05 }}>
        <Slider navigation={navigation} datas={introDatas} loadingState={isLoadingIntro} />
        <ListTouch navigation={navigation} />
        <BooksSuggestionList navigation={navigation} datas={ListSuggesitonBook} name={'Suggestion'} loadingState={isLoadingSuggest} />
        <BooksList navigation={navigation} datas={introDatas} name={'Daily Picks'} loadingState={isLoadingIntro} />
        <BooksList navigation={navigation} datas={introDatas} name={'What You Might Like'} loadingState={isLoadingIntro} />
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
