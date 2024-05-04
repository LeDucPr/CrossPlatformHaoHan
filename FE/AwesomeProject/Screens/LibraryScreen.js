import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground, FlatList, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';
import BottomBar from '../components/BottomBar';
import LibraryScreenTop from '../components/LibraryScreenComponents/LibraryScreenTop';
import getUserLibrary from '../fetchData/FetchUserLibrary';
import AsyncStorage from '@react-native-async-storage/async-storage';
import LibraryList from '../components/LibraryScreenComponents/LibraryList';
import fetchCoverDatas from '../fetchData/FetchCoverByListId';
import { useSelector, useDispatch } from 'react-redux';
import { FetchBooksIdsLibrary, FetchBooksLibrary } from '../app/slices/librarySlice';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');



export default function LibraryScreen({ navigation }) {
    const [userData, setUserData] = useState()
    // const [BookIds, setBookIds] = useState([])
    // const [BookDatas, setBookDatas] = useState([])

    const dispatch = useDispatch();
    const ListBooksLibraryIds = useSelector(state => state.libraryList.ListLibraryIds);
    const ListLibraryBooks = useSelector(state => state.libraryList.ListLibraryBooks);
    const isLoading = useSelector(state => state.libraryList.isLoading);
    const isFetchAll = useSelector(state => state.libraryList.isFetchAll);


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
    }, []); // dependency array rỗng

    useEffect(() => {
        if (userData && !isFetchAll) {
            dispatch(FetchBooksIdsLibrary({ clientId: userData.id }))
        }
    }, [userData]);

    useEffect(() => {
        if (ListBooksLibraryIds && ListBooksLibraryIds.length > 0 && !isFetchAll) {
            dispatch(FetchBooksLibrary());
        }
    }, [ListBooksLibraryIds]);

    // useEffect(() => {
    //     const fetchData = async () => {
    //         if (userData) {
    //             try {
    //                 const ListBookId = await getUserLibrary(userData.id);
    //                 const uniqueData = ListBookId.filter((value, index, self) => {
    //                     return self.indexOf(value) === index;
    //                   })
    //                 setBookIds(uniqueData);
    //             } catch (error) {
    //                 console.error('Lỗi khi lấy danh sách sách:', error);
    //             }
    //         }
    //     };

    //     fetchData();
    // }, [userData]);


    // useEffect(() => {
    //     const fetchData = async (ListBookId) => {
    //         try {
    //             const reversedListBookId = [...ListBookId].reverse();
    //             const data = await fetchCoverDatas(reversedListBookId);
    //             setBookDatas(data);
    //             setIsLoading(false);
    //         } catch (error) {
    //             console.error('Error fetching intro data:', error);
    //         }
    //     };
    //     if (BookIds && BookIds.length > 0) {
    //         fetchData(BookIds)
    //     }
    // }, [BookIds]);




    return (
        <SafeAreaView style={styles.container}>
            <LibraryScreenTop navigation={navigation} />
            <ScrollView>
                <LibraryList navigation={navigation} data={ListLibraryBooks} loadingState={isLoading} />
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
