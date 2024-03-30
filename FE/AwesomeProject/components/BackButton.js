import { StyleSheet, Image, TouchableOpacity } from 'react-native';

export default function BackButton({navigation}) {
  return (
    <TouchableOpacity  onPress={() => navigation.goBack()}>
        <Image style={styles.backButton}source={require("../assets/BackIcon.png")}/>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
    backButton:{
        marginTop: '5%',
        height: 30,
        width: 30,
      }
});