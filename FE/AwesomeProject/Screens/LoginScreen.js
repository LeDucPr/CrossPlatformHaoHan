import React, { useState } from 'react';
import {
    Text,
    View,
    StyleSheet,
    TextInput,
    TouchableOpacity,
    ImageBackground,
    SafeAreaView
} from 'react-native';

import WebLogo from '../components/WebLogo';
import ImageBackground1 from '../components/ImageBackground1';
import RegisterScreen from './RegisterScreen';

export default function LoginScreen({ navigation }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const handleLogin = async () => {
      try {
          const response = await fetch('https://localhost:7188/Client?userName=' + username + '&password=' + password, {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              // body: JSON.stringify({ username, password }), // Nếu cần gửi dữ liệu dưới dạng JSON
          });
  
          const data = await response.json();
  
          if (response.ok) {
              // Kiểm tra kết quả từ API ở đây
              if (data.success) {
                  setIsLoggedIn(true);
                  alert('Đăng nhập thành công');
                  // Thực hiện điều gì đó sau khi đăng nhập thành công, có thể là điều hướng đến màn hình khác
                  navigation.navigate('MainScreen');
              } else {
                  alert('Tên người dùng hoặc mật khẩu không chính xác');
              }
          } else {
              throw new Error('Đã xảy ra lỗi khi thực hiện yêu cầu');
          }
      } catch (error) {
          console.error('Lỗi:', error);
          alert('Đã xảy ra lỗi khi thực hiện yêu cầu');
      }
  };

    return (
        <SafeAreaView style={{ flex: 1 }}>
            <ImageBackground source={require('../assets/Background1.png')} style={styles.rootContainer} resizeMode='cover'>
                <View style={styles.container}>
                    <WebLogo />
                    <Text style={{ fontSize: 40, color: "white" }}>
                        Login
                  </Text>
                    <View style={styles.formContainer}>
                        <TextInput
                            style={styles.input}
                            placeholder="Username"
                            placeholderTextColor="#FFF9F9"
                            value={username}
                            onChangeText={text => setUsername(text)}
                        />
                        <TextInput
                            style={styles.input}
                            placeholder="Password"
                            placeholderTextColor="#FFF9F9"
                            secureTextEntry={true}
                            value={password}
                            onChangeText={text => setPassword(text)}
                        />
                        <TouchableOpacity style={styles.buttonLogin} onPress={handleLogin} onPressOut={() => navigation.navigate('MainScreen')}>
                            <Text style={styles.loginTxt}>Login</Text>
                        </TouchableOpacity>
                        <Text style={[styles.loginTxt, { marginTop: 20, textAlign: "center" }]}>Don't have an account?</Text>
                        <TouchableOpacity style={styles.buttonRes} onPress={() => navigation.navigate("Register")} >
                            <Text style={styles.ResTxt}>Create one</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </ImageBackground>
        </SafeAreaView>
    );
}


const styles = StyleSheet.create({
    rootContainer: {
        flex: 1,
    },
    container: {
        flex: 1,
        alignItems: 'center',
        backgroundColor: 'transparent',
    },
    formContainer: {
        marginTop: 20,
        width: '80%',
        height: "50%",
        backgroundColor: 'rgba(0, 0, 0, 0.9)', // White form background color
        padding: 20,
        borderRadius: 7,
        elevation: 5,
        shadowColor: '#EBE2E2', // Màu của bóng
        shadowOffset: { width: 0, height: 0 },
        shadowOpacity: 1,
        shadowRadius: 10,
    },
    input: {
        color: 'white',
        borderColor: 'gray',
        borderBottomWidth: 3,
        paddingHorizontal: 10,
        borderRadius: 10,
        marginTop: '5%',
        marginBottom: '5%',
        height: '20%',
        backgroundColor: 'rgba(255,255,255,0.2)',
    },
    buttonLogin: {
        marginTop: 10,
        height: '20%',
        color: '#FFFFFF',
        backgroundColor: '#E494BE',
        borderColor: '#gray',
        borderWidth: 1,
        borderRadius: 5,
        padding: 10,
        justifyContent: 'center',
        alignItems: 'center',
    },
    loginTxt: {
        color: "white",
        fontSize: 18,
    },
    buttonRes: {
        marginTop: 10,
        height: '0',
        color: '#FFFFFF',
        justifyContent: 'center',
        alignItems: 'center',
    },
    ResTxt: {
        color: '#7D6BF2',
        fontSize: 15,
        textDecorationLine: 'underline'
    }
})