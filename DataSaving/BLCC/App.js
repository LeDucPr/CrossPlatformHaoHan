import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import  LoginTemplate from './Others/HomeScreen'
import LoginPage from './Others/Login';
import RegisterPage from './Others/Register';

export default function App() {
return (
	// <LoginTemplate />
	// <LoginPage />
	<RegisterPage/>
  	);
}


// export default function App() {
//   return (
//     <View style={styles.container}>
//       <Text>Open up App.js to start working on your app!</Text>
//       <StatusBar style="auto" />
//     </View>
//   );
// }

// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//     backgroundColor: '#fff',
//     alignItems: 'center',
//     justifyContent: 'center',
//   },
// });
