import {
    Text,
    View,
    StyleSheet,
    TextInput,
    TouchableOpacity,
    ImageBackground,SafeAreaView, Dimensions, Image, ScrollView,
  } from 'react-native';
  
  import Slider from '../components/MainScreenComponents/Slider';

  import MainScreenTop from '../components/MainScreenComponents/MaiScreenTop';

  const {width: Screen_width, height: Screen_height} = Dimensions.get('window');

  export default function MainScreen({navigation}) {
    return (
        <SafeAreaView style = {styles.container}>
            <MainScreenTop navigation={navigation}/>
            <ScrollView style={styles.bottomSection}>
            <Slider/>
            </ScrollView>
        </SafeAreaView>
      
    );
  }

  const styles = StyleSheet.create({
    container:{
      flex:1,
      width: Screen_width,
      height: Screen_height,
      backgroundColor: '#FFFDFD'
    }
  })
  