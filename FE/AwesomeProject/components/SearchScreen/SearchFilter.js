import { Text, SafeAreaView, StyleSheet, View, FlatList, Image, TouchableOpacity, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';

export default function SearchFilter({ navigation, data, input, setInput, loadingState }) {
  const [isLoading, setIsLoading] = useState(loadingState);
  const [notFound, setNotFound] = useState(false);
  useEffect(() => {
    setIsLoading(loadingState);
    setNotFound(false);
    let timeoutId = null;

    if (loadingState) {
      timeoutId = setTimeout(() => {
        setNotFound(true);
      }, 5000); // 10 giây
    }
    return () => {
      clearTimeout(timeoutId);
    };
  }, [loadingState]);

  return (
    <View style={{ marginTop: 10 }}>
      {isLoading && !notFound ? (
        <ActivityIndicator size="large" color="gray" />
      ) : notFound ? (
        <Text>Không tìm thấy</Text>
      ) : (
        <FlatList data={data}  showsVerticalScrollIndicator={false} renderItem={({ item }) => {
          if (input === "") {
            return (
              <View>
                {/* <Image source ={item.img} style = {styles.image} resizeMode='contain'/>
                        <View styles ={{flex:0.6, height : 130, backgroundColor:'black'}}>
                            <Text style = {styles.textName}>{item.title}</Text>
                            <Text style ={styles.textAuthor}>{item.author}</Text>
                        </View> */}
                <Text>Không tìm thấy truyện</Text>
              </View>
            )
          }
          if (item.title.toLowerCase().replace(/[+=\-()&*%#@!:]/g, '').match(/\b\w+\b/g).some(word => input.toLowerCase().includes(word))) {
            return (
              <TouchableOpacity onPress={() => navigation.navigate("BookScreen", { item })}>
                <View style={styles.itemContainer}>
                  <Image source={{ uri: item.coverComicImagePngStrings[0]}} style={styles.image} resizeMode='contain' />
                  <View style={styles.infor}>
                    <Text style={styles.textName}>{item.title}</Text>
                    <Text style={styles.textAuthor}>{item.author}</Text>
                  </View>
                </View>
              </TouchableOpacity>
            )
          }
        }} />
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
