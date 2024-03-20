import React from 'react';
import { View, Text, StyleSheet, Button, Image } from 'react-native';
import { TextInput } from 'react-native-web';

const RegisterPage = () => {
	return (
	<View style = {styles.container}>
        <View style={styles.viewImageAndHeader}>
            <Image source={require("../assets/icon.png")} style = {styles.image}></Image>
            <Text style={styles.textHeader}>Welcome Back</Text>
        </View>
        <View style = {styles.viewTextInputs}>
            <View style = {styles.viewTextInput}> 
                <TextInput style = {styles.textInput} placeholder="Email" placeholderTextColor="rgba(150, 151, 153, 0.5)" />
            </View>
            <View style={{height:20}}/>
            <View style = {styles.viewTextInput}>
                <TextInput style = {styles.textInput} placeholder="Password" placeholderTextColor="rgba(150, 151, 153, 0.5)" secureTextEntry={true} />
            </View>
        </View>
        <View style = {styles.viewForgotPassword}>
            <Text style={styles.textForgotPassword} onPress={() => {}}>Forgot your password?</Text>
        </View>
        <View style={styles.viewButtons}>
            <View style = {styles.viewButtonLogin}>
                <Button title='Login' color="#481c8a" onPress={() => {}}/>
            </View>
        </View>
        <View style = {styles.viewTextAcc}>
            <Text style={styles.textAcc}>Don't have an account? <Text style={styles.signupLink}>Sign up</Text></Text>
        </View>
    </View>
	);
};

const styles = StyleSheet.create({
    container: {
        flex:1,
        justifyContent: "center",
        textAlign: "center",
        backgroundColor: '#FFFFFF'
    },
    viewImageAndHeader: {
        alignItems: "center",
    },
    image: {
        width: 100, 
        height: 100, 
        marginBottom: 20,
    },
    textHeader: {
        fontSize: 20,
        fontWeight: "bold",
        marginBottom: 20, 
        color: "#6200EE"
    },
    textInput: {
        flex: 1,
        fontSize: 16, 
        textDecorationColor: "#78797a",
        opacity: 1
    },
    viewTextInputs: {
        width: "80%", 
        marginHorizontal: "10%"
    },
    viewTextInput:{
        height: 40, 
        textAlign: "left",
        justifyContent:"center",
        borderRadius: 6,
        overflow: "hidden",
        borderColor: "#5f7dad",
        borderWidth:2,
    },
    viewForgotPassword: {
        margin: 5,
        marginHorizontal: "10%",
        alignItems: "flex-end"
    },
    textForgotPassword: {
        flex: 1,
        fontSize: 10,
    },
    viewButtons:{
        marginHorizontal: "10%",
        width: "80%"
    },
    viewButtonLogin: {
        flex:1,
        marginTop: 20,
        borderRadius: 6,
        overflow: "hidden",
    },
    viewTextAcc: {
        marginTop: 5,
        marginHorizontal: "10%",
        width: "*80%",
        alignItems: "flex-end"
    },
    textAcc: {
        fontSize: 10
    },
    signupLink: {
        color: '#6200EE',
        fontWeight: "bold"
    },
});

export default RegisterPage;