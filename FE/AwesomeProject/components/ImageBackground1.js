import { StyleSheet, Image, ImageBackground } from 'react-native';

export default function ImageBackground1({asdasd}) {
  return (
    <ImageBackground source={require('../assets/Background1.png') } resizeMode="cover" style={styles.image}>
    </ImageBackground>
  );
}

const styles = StyleSheet.create({
  image: {  
    flex: 1,

  },
});