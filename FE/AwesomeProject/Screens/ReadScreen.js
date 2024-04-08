import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground, FlatList, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';
import BackButton from '../components/BackButton';
import getImagesForBook from '../fetchData/FetchImagesById';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');


export default function BookScreen({ route, navigation }) {
    const { item } = route.params;
    const [images, setImages] = useState([]);
    const [skipImages, setSkipImages] = useState(0);
    const [isLoadingFirstLoad, setIsLoadingFirstLoad] = useState(true);
    const [isLoading, setIsLoading] = useState(false);
    const [isEndReached, setIsEndReached] = useState(false);

    const fetchImages = async () => {
        const takeImages = 3;
        const data = await getImagesForBook(item.id, skipImages, takeImages);
        console.log(data);
        if (data) {
            setImages(prevImages => [...prevImages, ...data]);
            setIsLoadingFirstLoad(false);
        }
        setIsLoading(false);
        setIsEndReached(false);
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
