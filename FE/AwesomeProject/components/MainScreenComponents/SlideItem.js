import {
    Text,
    View,
    StyleSheet,
    TextInput,
    TouchableOpacity,
    ImageBackground,SafeAreaView, Image, Dimensions, Animated,Easing
  } from 'react-native';
  

  const {width: Screen_width, height: Screen_height} = Dimensions.get('window');

  export default function SliderItem ({item, navigation})  {  
    const translateYImage = new Animated.Value(10);

    Animated.timing(translateYImage, {
      toValue: 0,
      duration: 1000,
      useNativeDriver: true,
      easing: Easing.bounce,
    }).start();
    return (
      <TouchableOpacity style = {{flext: 1}}onPress={() => {
        navigation.navigate('BookScreen', {item})
    }}>
        <View style={styles.container}>
            <Image source={item.img}  resizeMode="cover" 
             style={[
                    styles.image,
                    {
                      transform: [
                        { 
                          scale: 1,
                        },
                      ],
                    },
                  ]}  />
            {/* <View style={styles.content}>
                <Text  style={styles.title}>{item.title}</Text>
                <Text  style={styles.input}>Tác giả: {item.author}</Text>
                <Text  style={styles.input}>Thể loại: {item.genre}</Text>
                <Text  style={styles.input}>Giá: {item.price}</Text>
                <TouchableOpacity style = {styles.button} >
                <Text style={styles.input}>Đọc Ngay</Text>
                </TouchableOpacity>
            </View> */}
        </View>
        </TouchableOpacity>
    );
  }
  

  
  const styles = StyleSheet.create({
    container: {
        width:Screen_width,
        height:Screen_width,
        alignItems: 'center',
        //flexDirection: 'row',
      },
      image: {
        flex: 1,
        width: '100%',
        borderColor: "#2F2C2C",
        borderBottomWidth: 1,
        borderTopWidth: 1,
      },
      content:{
        padding:10,
        flex: 0.4,
      },
      title:{
        textAlign: 'center',
        fontSize: 24,
        fontWeight: 'bold',
        color: '#333',
      },
      input:{
        marginLeft: 10,
        fontSize: 18,
        color: '#333',
      },
      button: {
        padding:10,
        margin: 10,
        color: '#FFFFFF',
        backgroundColor: '#0FA8CE',
        borderColor: '#gray',
        borderWidth: 1,
        borderRadius: 5,
        justifyContent: 'center',
        alignItems: 'center',
      },
  })