import {
    Text,
    View,
    StyleSheet,
    TextInput,
    TouchableOpacity,
    ImageBackground,SafeAreaView, Dimensions, Image, ScrollView,
  } from 'react-native';
  import { useEffect, useState } from 'react';
  import Books from '../data';
  import SearchFilter from '../components/SearchScreen/SearchFilter';
  import BackButton from '../components/BackButton';

  const {width: Screen_width, height: Screen_height} = Dimensions.get('window');

  export default function SearchScreen ({navigation}){
    const [isLoading, setIsLoading] = useState(false);
    const [data, setData] = useState([]);
    const [error, setError] = useState(null);
    const [fullData, setFullData] = useState([]);
    const [searchQuery, setSearchQuery] = useState("");

    // useEffect(()=>{
    //     setIsLoading(true);
    //     fetchData({Data});
    // }, []);

    // const fetchData = async (url) => {
    //     try{
    //         const respone = await fetch(url);
    //         const json = await respone.json();
    //         setData(json);
    //         console.log(json);
    //     }catch(error){
    //         setError(error)
    //     }
    // }

    const handleSearch = (query) =>{
        setSearchQuery(query);
    }
    return (
        <SafeAreaView style = {{flex:1, marginHorizontal: 20}}>
          <BackButton navigation={navigation}/>
          <View style={styles.parent}>
            <TextInput placeholder='Search' style={styles.searchBox} autoCapitalize='none' autoCorrect={false} value={searchQuery}
                       onChangeText={(query) => handleSearch(query)}
            />
            <TouchableOpacity style={styles.closeButtonParent} onPress={() => handleSearch("")}>
              <Image style={styles.closeButton}source={require("../assets/closeIcon.png")}/>
            </TouchableOpacity>
          </View>
          <SearchFilter navigation={navigation} data = {Books} input = {searchQuery} setInput = {setSearchQuery}/>
        </SafeAreaView> 
    );
  } 
  
 
  
  const styles = StyleSheet.create({
    container:{
      flex:1,
      width: Screen_width,
      height: Screen_height,
      backgroundColor: 'white'
    },
    parent: {
      marginTop: '5%',
      paddingHorizontal:20, 
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
    backButton:{
      backgroundColor: "white",
      marginTop: '5%',
      height: 30,
      width: 30,
    }
  })
  