import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView } from 'react-native';

const {width: Screen_width, height: Screen_height} = Dimensions.get('window');

export default function BookScreen({route}) {
    const { item } = route.params;
    return (
        <SafeAreaView style = {{flex:1}}>
            <View style={styles.container}> 
                <View style= {styles.image}>
                    <Image source={item.img}  resizeMode="cover" style = {{flex: 1, width: "100%"}}/>
                    <View style={styles.titleContainer}>
                        <Text style={styles.titletext}>{item.title}</Text>
                    </View>
                </View>
                <View style={styles.overlay}>
                <ScrollView >
                    <View style={styles.inforContainer}>
                        {/* <Text>{item.title}</Text> */}
                        <Text style = {styles.textDescrip}>{item.description}</Text>    
                        <Text style = {styles.textAuthor}>Tác giả: {item.author}</Text>
                    </View>
                </ScrollView>
                </View>
            </View>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex:1,
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
        ...StyleSheet.absoluteFillObject,
        justifyContent: 'flex-end',
        bottom: 20,
        padding: 20
      },
      titletext: {
        width: '80%',
        color: '#FFFDFD',
        fontSize: 30,
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
      inforContainer:{
        padding: 20
      },
      textDescrip:{
        paddingBottom:5,
        marginBottom: 5,
        fontSize: 22,
        fontWeight: '400',
        color: '#4B4B4B',
        borderBottomWidth: 1,
        borderRadius:50,
        borderColor: '#E2E2E2'
      },
      textAuthor:{
        marginBottom:10,
        fontSize:18,

      }
});
