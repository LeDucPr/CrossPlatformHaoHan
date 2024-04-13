import {
  Text,
  View,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  ImageBackground, SafeAreaView, Dimensions, Image, ScrollView,
} from 'react-native';
import { useEffect, useState } from 'react';
import Books from '../data';
import SearchFilter from '../components/SearchScreen/SearchFilter';
import BackButton from '../components/BackButton';
import fetchCoversDataFromFieldsContrains from '../fetchData/FetchFromFields';

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function SearchScreen({ route, navigation }) {
  const [isLoading, setIsLoading] = useState(false);
  const [tempSearchQuery, setTempSearchQuery] = useState('');
  const [searchQuery, setSearchQuery] = useState('');
  const [isEnterPressed, setIsEnterPressed] = useState(false);
  const [Books, setBooks] = useState([]);


  const handleSearch = (query) => {
    setTempSearchQuery(query);
  }

  const handleSubmit =() => {
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


  return (
    <SafeAreaView style={{ flex: 1, marginHorizontal: 20 }}>
      <BackButton navigation={navigation} />
      <View style={styles.parent}>
        <TextInput placeholder='Search' style={styles.searchBox} autoCapitalize='none' autoCorrect={false} value={tempSearchQuery}
          onChangeText={(query) => handleSearch(query)} onKeyPress={handleKeyPress} onSubmitEditing={handleSubmit} returnKeyType='search'/>
        <TouchableOpacity style={styles.closeButtonParent} onPress={() => { handleSearch(""); setIsEnterPressed(false); }}>
          <Image style={styles.closeButton} source={require("../assets/closeIcon.png")} />
        </TouchableOpacity>
      </View>
      {isEnterPressed && <SearchFilter navigation={navigation} data={Books} input={searchQuery.replace(/[+=\-()&*%#@!:]/g, '')} setInput={setSearchQuery} loadingState={isLoading} />}
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
