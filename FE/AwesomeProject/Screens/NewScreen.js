import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground, FlatList, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';
import BottomBar from '../components/BottomBar';
import ListBooks from '../components/SharedMainScreenComponents/ListBooks';
import ScreenTop from '../components/SharedMainScreenComponents/ScreenTop';
import { useSelector, useDispatch } from 'react-redux';
import { FetchBooksNew, FetchBooksIdsNew } from '../app/slices/newSlice';
import { setReachEndFalse } from '../app/slices/rankingSlice';
import { amountRankingBooks } from '../SetUp';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');


export default function NewScreen({ navigation }) {
    const dispatch = useDispatch();
    const ListBooksNewIds = useSelector(state => state.newList.ListNewIds);
    const LisNewBooks = useSelector(state => state.newList.ListNewbooks);
    const skipIds = useSelector(state => state.newList.skipIds);
    const skipIdslenght = useSelector(state => state.newList.skipIdslenght);
    const isLoading = useSelector(state => state.newList.isLoading);
    const isEndReached = useSelector(state => state.newList.isReachEnd);

    useEffect(() => {
        if (skipIds.length >= 0 && skipIds.length < skipIdslenght && isEndReached) {
            dispatch(FetchBooksIdsNew({ amount: amountRankingBooks, skipIds: skipIds }))
        }
    }, [skipIds, skipIdslenght, isEndReached]);

    useEffect(() => {
        if (ListBooksNewIds && ListBooksNewIds.length > 0 && LisNewBooks.length < skipIds.length) {
            console.log(ListBooksNewIds);
            console.log(skipIds)
            console.log(skipIdslenght)
            dispatch(FetchBooksNew());
        }
    }, [ListBooksNewIds]);

    // useEffect(() => {
    //     console.log(LisNewBooks)
    // }, [LisNewBooks]);

    // useEffect(() => {
    //     console.log(isEndReached)
    // }, [isEndReached]);


    return (
        <SafeAreaView style={styles.container}>
            <View style={{ flex: 1 }}>
                <ScreenTop navigation={navigation} Name={'News'} />
                <ListBooks navigation={navigation} name = {'New'} data={LisNewBooks} loadingState={isLoading} isEndReached={isEndReached}/>
                <BottomBar navigation={navigation} />
            </View>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#FFFDFD'
    },
});
