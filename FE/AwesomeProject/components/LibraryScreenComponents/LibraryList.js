import { Text, SafeAreaView, StyleSheet, View, FlatList, Image, TouchableOpacity, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';

export default function LibraryList({ navigation, data, loadingState }) {
  const [isLoading, setIsLoading] = useState(loadingState);
  const [notFound, setNotFound] = useState(false);
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

  return (
    <View style={{ marginTop: 10 }}>
      {isLoading && !notFound ? (
        <ActivityIndicator size="large" color="gray" />
      ) : notFound ? (
        <Text>Không tìm thấy</Text>
      ) : (
        <FlatList data={data}  showsVerticalScrollIndicator={false} renderItem={({ item }) => {
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
