import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground, FlatList, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';
import BottomBar from '../components/BottomBar';
import ListBooks from '../components/SharedMainScreenComponents/ListBooks';
import ScreenTop from '../components/SharedMainScreenComponents/ScreenTop';
import { useSelector, useDispatch } from 'react-redux';
import { FetchBooksIdsRanking, FetchBooksRanking } from '../app/slices/rankingSlice';
import { setReachEndFalse } from '../app/slices/rankingSlice';
import {amountRankingBooks } from '../SetUp';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');


export default function RankngScreen({ navigation }) {
    const dispatch = useDispatch();
    const ListBooksRankingIds = useSelector(state => state.rankingList.ListRankingIds);
    const ListRankingBooks = useSelector(state => state.rankingList.ListRankingBooks);
    const skipIds = useSelector(state => state.rankingList.skipIds);
    const skipIdslenght = useSelector(state => state.rankingList.skipIdslenght);
    const isLoading = useSelector(state => state.rankingList.isLoading);
    const isEndReached = useSelector(state => state.rankingList.isReachEnd);

    useEffect(() => {
        if (skipIds.length >= 0 && skipIds.length < skipIdslenght && isEndReached) {
            dispatch(FetchBooksIdsRanking({ amount: amountRankingBooks, skipIds: skipIds }))
        }
    }, [skipIds, skipIdslenght, isEndReached]);

    useEffect(() => {
        if (ListBooksRankingIds && ListBooksRankingIds.length > 0) {
            console.log(ListBooksRankingIds);
            console.log(skipIds)
            console.log(skipIdslenght)
            dispatch(FetchBooksRanking());
        }
    }, [ListBooksRankingIds]);

    useEffect(() => {
        console.log(ListRankingBooks)
    }, [ListRankingBooks]);

    useEffect(() => {
        console.log(isEndReached)
    }, [ListRankingBooks]);


    return (
        <SafeAreaView style={styles.container}>
            <ScreenTop navigation={navigation} Name={'Ranking'} />
            <ScrollView>
                <ListBooks navigation={navigation} data={ListRankingBooks} loadingState={isLoading} />
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
});
