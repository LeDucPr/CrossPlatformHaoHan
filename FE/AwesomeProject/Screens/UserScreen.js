import { Text, SafeAreaView, StyleSheet, Image, Dimensions, View, ScrollView, TouchableOpacity, Button, ImageBackground, FlatList, ActivityIndicator } from 'react-native';
import { useEffect, useState } from 'react';
import BackButton from '../components/BackButton';
import getImagesForBook from '../fetchData/FetchImagesById';
import UserScreenTop from '../components/UserScreenComponents/UserScreenTop';
import BottomBar from '../components/BottomBar';
import AsyncStorage from '@react-native-async-storage/async-storage';

const { width: Screen_width, height: Screen_height } = Dimensions.get('window');


export default function UserSceen({ navigation }) {
    const [userData, setUserData] = useState([]);

   useEffect(() => {
        const fetchUserData = async () => {
            try {
                const data = await AsyncStorage.getItem('userData');
                if (data) {
                    setUserData(JSON.parse(data));
                }
            } catch (error) {
                console.error('Lỗi khi truy xuất dữ liệu từ AsyncStorage:', error);
            }
        };

        fetchUserData();
    }, []);

    useEffect(() => {
    }, [userData]);

    return (
        <SafeAreaView style={styles.container}>
            <UserScreenTop navigation={navigation} userData={userData} />
            <BottomBar navigation={navigation} />
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
});
