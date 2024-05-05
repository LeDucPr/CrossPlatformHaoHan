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
import { FetchBooksGenres } from '../app/slices/genresBkSlice';



const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function GenresScreen({ route, navigation }) {
    const [isLoading, setIsLoading] = useState(false);
    const [tempSearchQuery, setTempSearchQuery] = useState('');
    const [searchQuery, setSearchQuery] = useState('');
    const [isEnterPressed, setIsEnterPressed] = useState(false);
    const [Books, setBooks] = useState([]);


    const handleSearch = (query) => {
        setTempSearchQuery(query);
    }

    const handleSubmit = () => {
        setSearchQuery(tempSearchQuery);
        setIsEnterPressed(true);
    }
    const handleKeyPress = (event) => {
        if (event.key === 'Enter') {
            setSearchQuery(tempSearchQuery);
            setIsEnterPressed(true);
        }
    };

    useEffect(() => {
        if (isEnterPressed) {
            setIsLoading(true)
            const fetchData = async () => {
                try {
                    const sanitizedQuery = searchQuery.replace(/[+=\-()&*%#@!:]/g, '');
                    const words = sanitizedQuery.split(' ');
                    const minWordLength = Math.min(...words.map(word => word.length));
                    const amountWord = minWordLength - 2
                    const amountCovers = 10;
                    const skipIds = [];
                    const fields = {
                        title: searchQuery
                    };
                    const data = await fetchCoversDataFromFieldsContrains(amountWord, amountCovers, skipIds, fields);
                    setBooks(data)
                    setIsLoading(false)
                } catch (error) {
                    console.error('Error fetching intro data:', error);
                }
            };
            fetchData();
        }
    }, [searchQuery]);


    const dispatch = useDispatch();
    const Genre = useSelector(state => state.genreList.Genre)

    useEffect(() => {
        if (Genre && Genre.length > 0) {
            dispatch(FetchBooksGenres())
        }
    }, [Genre]);

    return (
        <SafeAreaView style={{ flex: 1 }}>
            <ScreenTop navigation={navigation} Name={'Genres'} />
            <View style={styles.parent}>
                <TextInput placeholder='Search' style={styles.searchBox} autoCapitalize='none' autoCorrect={false} value={tempSearchQuery}
                    onChangeText={(query) => handleSearch(query)} onKeyPress={handleKeyPress} onSubmitEditing={handleSubmit} returnKeyType='search' />
                <TouchableOpacity style={styles.closeButtonParent} onPress={() => { handleSearch(""); setIsEnterPressed(false); }}>
                    <Image style={styles.closeButton} source={require("../assets/closeIcon.png")} />
                </TouchableOpacity>
            </View>
            <GenresType navigation={navigation} />
            {!isEnterPressed && <View style={{ flex: 1 }} />}
            {isEnterPressed && <SearchFilter navigation={navigation} data={Books} input={searchQuery.replace(/[+=\-()&*%#@!:]/g, '')} setInput={setSearchQuery} loadingState={isLoading} />}
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
    parent: {
        marginTop: '5%',
        paddingHorizontal: 20,
        paddingVertical: 10,
        borderColor: "gray",
        borderRadius: 5,
        borderWidth: 1,
        flexDirection: "row",
        justifyContent: "space-between",
    },
    searchBox: {
        height: 25,
        width: "90%",
    },
    closeButton: {
        height: 20,
        width: 20,
    },
    closeButtonParent: {
        justifyContent: "center",
        alignItems: "center",
        marginRight: 5,
    },
    backButton: {
        backgroundColor: "white",
        marginTop: '5%',
        height: 30,
        width: 30,
    }
})
