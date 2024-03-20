import React from 'react';
import { View, Text, StyleSheet, Button, Image } from 'react-native';

const LoginTemplate = () => {
	return (
	<View style={styles.container}>
		<Image source={require('../assets/icon.png')} style={styles.image}></Image>
		<Text style={styles.headerText}>Login Template</Text>
		<View style={styles.subtextView}>
			<Text style={styles.subtext}>The easiest way to start with your amazing application.</Text>
		</View>
		<View style={styles.buttonViews}>
			<View style = {styles.buttonView}>
				<Button title="LOGIN" color="#6200EE" onPress={() => {}} />
			</View>
			<View style = {{height:10}}></View>
			<View style = {styles.buttonView}>
				<Button title="SIGN UP" color="#000000" onPress={() => {}} />
			</View>
		</View>
	</View>
	);
};

const styles = StyleSheet.create({
	container: {
		flex: 1,
		justifyContent: 'center',
		alignItems: 'center',
		backgroundColor: '#FFFFFF',
	},
	image: {
		width: 100,
		height: 100,
		marginBottom: 20,
	},
	headerText: {
		fontSize: 24,
		fontWeight: 'bold',
	},
	subtextView: {
		width: '80%',
		marginHorizontal: '10%',
	},
	subtext: {
		fontSize: 16,
		color: '#888888',
		textAlign: 'center',
	},
	buttonViews: {
		marginTop: 20,
		width: '80%',
		marginHorizontal: '10%',
	},
	buttonView: {
		borderRadius:6,
		overflow: 'hidden',
	}
  });

export default LoginTemplate;
