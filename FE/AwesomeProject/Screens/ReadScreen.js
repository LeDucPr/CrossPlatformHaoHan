import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground, FlatList, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';
import BackButton from '../components/BackButton';
import getImagesForBook from '../fetchData/FetchImagesById';
import UpdateRating from '../fetchData/UpdateRating';
import UpdateUserLibrary from '../fetchData/UpdateUserLibrary';
import AsyncStorage from '@react-native-async-storage/async-storage';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');


export default function BookScreen({ route, navigation }) {
    const { item } = route.params;
    const [images, setImages] = useState([]);
    const [skipImages, setSkipImages] = useState(0);
    const [isLoadingFirstLoad, setIsLoadingFirstLoad] = useState(true);
    const [isLoading, setIsLoading] = useState(false);
    const [isEndReached, setIsEndReached] = useState(false);
    const [userData, setUserData] = useState();

    const fetchImages = async () => {
        const takeImages = 3;
        const data = await getImagesForBook(item.id, skipImages, takeImages);
        if (data) {
            setImages(prevImages => [...prevImages, ...data]);
            setIsLoadingFirstLoad(false);
        }
        setIsLoading(false);
        if (data && data.length > 0) {
            setIsEndReached(false);
        }
        if (data && data.length == 0) {
            await UpdateRating(item.id);
        }
    };
    
    useEffect(() => {
        setIsLoading(true);
        fetchImages();
    }, [skipImages]);

    const handleEndReached = () => {
        if (!isEndReached) {
            setIsEndReached(true);
            const takeImages = 3;
            setSkipImages(prevSkipImages => prevSkipImages + takeImages);
        }
    };

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

    useEffect(() => {
        if (userData) {
          UpdateUserLibrary(userData.id, item.id);
        }
      }, [userData]);


    const renderItem = ({ item }) => (
        <Image style={{ width: Screen_width, height: Screen_height * 0.7 }} source={{ uri: item }} resizeMode='contain' />
    );

    const renderLoadingIndicator = () => (
        <View style={styles.loadingIndicator}>
            <ActivityIndicator size="large" color="#0000ff" />
        </View>
    );

    return (
        <SafeAreaView style={{ flex: 1 }}>
            <View style={styles.container}>
                <View style={styles.backContainer}>
                    <BackButton navigation={navigation} />
                </View>
                <View style={styles.flatListContainer}>
                    {isLoadingFirstLoad ? (
                        renderLoadingIndicator()
                    ) : (
                        <FlatList
                            data={images}
                            renderItem={renderItem}
                            keyExtractor={(item, index) => index.toString()}
                            onEndReached={handleEndReached}
                            onEndReachedThreshold={0.1}
                            ListFooterComponent={isLoading && renderLoadingIndicator}
                        />
                    )}
                </View>
            </View>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    backContainer: {
        position: 'absolute',
        top: 20,
        left: 20,
        zIndex: 1,
        backgroundColor: 'rgba(0, 0, 0, 0)',
    },
    flatListContainer: {
        flex: 1,
        backgroundColor: 'rgba(0, 0, 0, 0.9)',
    },
});
