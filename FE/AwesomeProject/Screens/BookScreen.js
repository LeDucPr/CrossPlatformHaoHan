import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground } from 'react-native';
import BackButton from '../components/BackButton';
import { useEffect, useState } from 'react';
import UpdateRating from '../fetchData/UpdateRating';
const { width: Screen_width, height: Screen_height } = Dimensions.get('window');


export default function BookScreen({ route, navigation }) {
  const { item } = route.params;
  
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <View style={styles.container}>
        <View style={styles.image}>
          <ImageBackground source={{uri: item.coverComicImagePngStrings[0]}} resizeMode="cover" style={{ flex: 1, width: "100%" }}>
            <View style={styles.backConainer}>
              <BackButton navigation={navigation} />
            </View>
            <View style={styles.titleContainer}>
              <Text numberOfLines={3} style={styles.titletext}>{item.title}</Text>
            </View>
          </ImageBackground>
        </View>
        <View style={styles.overlay}>
          <ScrollView >
            <View style={styles.inforContainer}>
              {/* <Text>{item.title}</Text> */}
              <Text style={styles.textDescrip}>{item.description}</Text>
              <Text style={styles.textAuthor}>Tác giả: {item.author}</Text>
            </View>
          </ScrollView>
        </View>
        <View style={{ justifyContent: 'flex-end' }}>
          <View style={styles.Botcontainer}>
            <TouchableOpacity style={{ justifyContent: 'center', alignItems: 'center' }}>
              <Image style={styles.likeButton} source={require("../assets/like-106.png")} resizeMode='contain' />
              <Text>{item.reader.toString()}</Text>
            </TouchableOpacity>
            <View style={{ justifyContent: 'center', }}>
              <TouchableOpacity style={styles.ReadBtn} onPress={() => navigation.navigate("ReadScreen", { item })}>
                <Text>Read now</Text>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    //alignItems: 'center',
    //flexDirection: 'row',
    backgroundColor: '#FFF2F2'
  },
  image: {
    flex: 0.5,
    width: '100%',
    borderColor: "#2F2C2C",
    backgroundColor: 'red'
  },
  titleContainer: {
    position: 'absolute',
    justifyContent: 'flex-end',
    bottom: 20,
    padding: 20
  },
  backConainer: {
    padding: 20,
  },
  titletext: {
    width: '80%',
    color: '#FFFDFD',
    fontSize: 25,
    fontWeight: 'bold',
  },
  overlay: {
    flex: 0.5,
    width: '100%',
    bottom: 20,
    borderTopLeftRadius: 25,
    borderTopRightRadius: 25,
    backgroundColor: '#FFF2F2'
  },
  inforContainer: {
    padding: 20
  },
  textDescrip: {
    paddingBottom: 5,
    marginBottom: 5,
    fontSize: 22,
    fontWeight: '400',
    color: '#4B4B4B',
    borderBottomWidth: 1,
    borderRadius: 20,
    borderColor: '#E2E2E2'
  },
  textAuthor: {
    marginBottom: 10,
    fontSize: 18,

  },
  Botcontainer: {
    height: Screen_height * 0.075,
    flexDirection: 'row',
    justifyContent: 'space-around',
    alignContent: 'center',
    backgroundColor: '##FFF2F2',
    borderTopWidth: 1,
    borderTopColor: '#FAF9F9',
  },
  ReadBtn: {
    width: Screen_width * 0.7,
    height: "80%",
    backgroundColor: '#DEF643',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 25,
  },
  likeButton: {
    width: Screen_width * 0.2,
    height: "65%",
  }
});
