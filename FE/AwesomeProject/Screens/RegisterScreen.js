import {
  Text,
  View,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  ImageBackground,
  SafeAreaView
} from 'react-native';
import { useEffect, useState } from 'react';
import CreateNewAccount from '../fetchData/CreatNewAccount';
import WebLogo from '../components/WebLogo';

const RegistersScreen = (navigation) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  const handleRegister = async () => {
    if (password === confirmPassword) {
      // Mật khẩu và xác nhận mật khẩu giống nhau
      await CreateNewAccount(username, password, email);
      navigation.navigate("LoginScreen");
    } else {
      // Mật khẩu và xác nhận mật khẩu không giống nhau
      console.log("Password and Confirm Password do not match");
    }
  };

  return (
    <SafeAreaView style={{ flex: 1 }}>
      <ImageBackground source={require('../assets/Background1.png')} style={styles.rootContainer} resizeMode='cover'>
        <View style={styles.container}>
          <WebLogo />
          <View>
            <Text style={{ fontSize: 40, color: "white" }}>
              Register
            </Text>
          </View>
          <View style={styles.formContainer}>
            <TextInput
              style={styles.input}
              placeholder="name@gmail.com"
              placeholderTextColor="#FFF9F9"
              onChangeText={text => setEmail(text)}
            />
            <TextInput
              style={styles.input}
              placeholder="Username"
              placeholderTextColor="#FFF9F9"
              onChangeText={text => setUsername(text)}
            />
            <TextInput
              style={styles.input}
              placeholder="Password"
              placeholderTextColor="#FFF9F9"
              secureTextEntry={true}
              onChangeText={text => setPassword(text)}
            />
            <TextInput
              style={styles.input}
              placeholder=" Comfirm Password"
              placeholderTextColor="#FFF9F9"
              secureTextEntry={true}
              onChangeText={text => setConfirmPassword(text)}
            />
            <TouchableOpacity onPress={handleRegister} style={styles.button}>
              <Text style={styles.loginTxt}>Submit</Text>
            </TouchableOpacity>
          </View>
        </View>
      </ImageBackground>
    </SafeAreaView>
  );
}

export default RegistersScreen;

const styles = StyleSheet.create({
  rootContainer: {
    flex: 1,
  },
  container: {
    flex: 1,
    alignItems: 'center',

  },
  formContainer: {
    marginTop: 20,
    width: '80%',
    height: "55%",
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
    backgroundColor: 'rgba(255,255,255,0.16)',
    height: "18%",
    color: 'white',
    borderColor: 'gray',
    borderWidth: 1,
    marginBottom: 10,
    paddingHorizontal: 10,
    borderRadius: 10,
  },
  button: {
    marginTop: 10,
    height: 50,
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
    fontSize: 15,
  }
})
