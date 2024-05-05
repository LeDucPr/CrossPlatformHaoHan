import { Text, SafeAreaView, StyleSheet, View, FlatList, Image, TouchableOpacity, ActivityIndicator, Dimensions } from 'react-native';
import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { setReachEndTrueGenre } from '../../app/slices/genresBkSlice';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');
export default function GenresList({ navigation, name, data, loadingState, isEndReached }) {
    const dispatch = useDispatch()
    const [isLoading, setIsLoading] = useState(loadingState);
    const [notFound, setNotFound] = useState(false);

    const isReachEnd = useSelector(state => state.genreList.isReachEnd); 

    const handleEndReached = () => {
        if (!isEndReached) {
            dispatch(setReachEndTrueGenre())
            }
        }

    useEffect(() => {
        setIsLoading(loadingState);
        setNotFound(false);
        let timeoutId = null;

        if (loadingState) {
            timeoutId = setTimeout(() => {
                setNotFound(true);
            }, 10000); // 10 giây
        }
        return () => {
            clearTimeout(timeoutId);
        };
    }, [loadingState]);

    const renderLoadingIndicator = () => (
        <View style={styles.loadingIndicator}>
            <ActivityIndicator size="large" color="#0000ff" />
        </View>
    );

    return (
        <View style={{ flex: 1, marginTop: 10 }}>
            {/* <BackButton/> */}
            {isLoading && !notFound ? (
                <ActivityIndicator size="large" color="gray" />
            ) : notFound ? (
                <Text>Không tìm thấy</Text>
            ) : (
                <FlatList data={data}
                    // style={{ flex: 1 }}
                    contentContainerStyle={{
                        paddingBottom: Screen_height * 0.05,
                        paddingTop: 10,
                    }}
                    numColumns={2}
                    showsVerticalScrollIndicator={false}
                    renderItem={({ item }) => {
                        return (
                            <TouchableOpacity onPress={() => navigation.navigate("BookScreen", { item })}>
                                <View style={styles.itemContainer}>
                                    <Image style={styles.image} source={{ uri: item.coverComicImagePngStrings[0] }} resizeMode="cover" />
                                    <Text numberOfLines={1} style={styles.title}>{item.title}</Text>
                                </View>
                            </TouchableOpacity>
                        )
                    }}
                onEndReached={handleEndReached}
                onEndReachedThreshold={0.1}
                ListFooterComponent={isEndReached ? renderLoadingIndicator : null} 
                />
            )}
        </View>
    );
}

const styles = StyleSheet.create({
    itemContainer: {
        width: Screen_width * 0.45,
        height: Screen_width * 0.4,
        margin: Screen_width * 0.02,
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
});
