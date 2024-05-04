import { Text, SafeAreaView, StyleSheet, View, FlatList, Image, TouchableOpacity, ActivityIndicator, Dimensions } from 'react-native';
import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { setReachEndTrueRanking } from '../../app/slices/rankingSlice';
import { setReachEndTrueNew } from '../../app/slices/newSlice';
import ScreenTop from './ScreenTop';
import BackButton from '../BackButton';
import BottomBar from '../BottomBar';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');
export default function ListBooks({ navigation, name, data, loadingState, isEndReached }) {
    const dispatch = useDispatch()
    const [isLoading, setIsLoading] = useState(loadingState);
    const [notFound, setNotFound] = useState(false);

    const handleEndReached = () => {
        if (!isEndReached) {
            if (name == 'Ranking') {
                dispatch(setReachEndTrueRanking())
            }
            if (name == 'New') {
                dispatch(setReachEndTrueNew())
            }
        }
    };

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
                    showsVerticalScrollIndicator={false}
                    renderItem={({ item }) => {
                        return (
                            <TouchableOpacity onPress={() => navigation.navigate("BookScreen", { item })}>
                                <View style={styles.itemContainer}>
                                    <Image source={{ uri: item.coverComicImagePngStrings[0] }} style={styles.image} resizeMode='contain' />
                                    <View style={styles.infor}>
                                        <Text style={styles.textName}>{item.title}</Text>
                                        <Text style={styles.textAuthor}>{item.author}</Text>
                                    </View>
                                </View>
                            </TouchableOpacity>
                        )
                    }}
                    onEndReached={handleEndReached}
                    onEndReachedThreshold={0.1}
                    //ListFooterComponent={isEndReached ? renderLoadingIndicator : null} 
                />
            )}
        </View>
    );
}

const styles = StyleSheet.create({
    itemContainer: {
        flex: 1,
        flexDirection: 'row',
        alignItems: 'center',
        marginLeft: 10,
        marginTop: 20,
        justifyContent: 'space-between'
    },
    image: {
        flex: 0.35,
        height: 125,
        borderRadius: 10,
        borderColor: '#ccc',
        borderWidth: 1,
    },
    infor: {
        flex: 0.60,
        height: 130,
        justifyContent: 'center',
        borderBottomWidth: 1,
        borderColor: "#EFEAEA",
    },
    textName: {
        fontSize: 21,
        marginLeft: 10,
        fontWeight: '600',
    },
    textAuthor: {
        fontSize: 15,
        marginLeft: 10,
    }
});
