import {
  Text,
  View,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  ImageBackground, SafeAreaView, Dimensions, Image, ScrollView,
} from 'react-native';

import Slider from '../components/MainScreenComponents/Slider';
import Datas from '../data';
import MainScreenTop from '../components/MainScreenComponents/MaiScreenTop';
import BooksList from '../components/BookScreenComponent/BooksList';
import BottomBar from '../components/BottomBar';

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');

export default function MainScreen({ navigation }) {
  return (
    <SafeAreaView style={styles.container}>
      <MainScreenTop navigation={navigation} />
      <ScrollView>
        <Slider navigation={navigation}/>
        <BooksList navigation={navigation} Datas={Datas} Name ={'Daily Picks'}/>
        <BooksList navigation={navigation} Datas={Datas} Name ={'What You Might Like'}/>
      </ScrollView>
      <BottomBar navigation={navigation}/>
    </SafeAreaView>

  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    width: Screen_width,
    height: Screen_height,
    backgroundColor: '#FFFDFD'
  }
})
