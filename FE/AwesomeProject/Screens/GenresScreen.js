import {
    Text,
    View,
    StyleSheet,
    TextInput,
    TouchableOpacity,
    ImageBackground, SafeAreaView, Dimensions, Image, ScrollView,
} from 'react-native';
import { useEffect, useState } from 'react';
import SearchFilter from '../components/SearchScreen/SearchFilter';
import fetchCoversDataFromFieldsContrains from '../fetchData/FetchFromFields';
import BottomBar from '../components/BottomBar';
import ScreenTop from '../components/SharedMainScreenComponents/ScreenTop';
import GenresType from '../components/GenresScreenComponent/GenresType';
import { useDispatch, useSelector } from 'react-redux';
import { FetchBooksIdsGenres, FetchBooksGenres } from '../app/slices/genresBkSlice';
import { amountRankingBooks } from '../SetUp';
import GenresList from '../components/GenresScreenComponent/GenresList';

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function GenresScreen({ route, navigation }) {
    const dispatch = useDispatch();
    const amountBooks = 6
    const Genre = useSelector(state => state.genreList.Genre)
    const ListBooksGenreIds = useSelector(state => state.genreList.ListGenresIds);
    const ListGenreBooks = useSelector(state => state.genreList.ListGenresBooks);
    const skipIds = useSelector(state => state.genreList.skipIds);
    const isReachEnd = useSelector(state => state.genreList.isReachEnd);
    const isLoading = useSelector(state => state.genreList.isLoading);

    useEffect(() => {
        if (Genre && Genre.length > 0 && isReachEnd) {
            dispatch(FetchBooksIdsGenres({amount: amountBooks, genre: Genre, skipIds: skipIds}))
        }
    }, [Genre, isReachEnd]);

    useEffect(() => {
        if (ListBooksGenreIds && ListBooksGenreIds.length > 0 && ListGenreBooks.length < skipIds.length) {
            dispatch(FetchBooksGenres());
        }
    }, [ListBooksGenreIds]);

    return (
        <SafeAreaView style={{ flex: 1 }}>
            <ScreenTop navigation={navigation} Name={'Genres'} />
            <GenresType navigation={navigation} />
            <GenresList navigation={navigation} name = 'Genre' data = {ListGenreBooks} loadingState={isLoading} isEndReached={isReachEnd}/>
            <BottomBar navigation={navigation} />
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        width: Screen_width,
        height: Screen_height,
        backgroundColor: 'white'
    },
})
