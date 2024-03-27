import React from 'react'
import { Image, StyleSheet, Text, View } from 'react-native'

export default function WebLogo() {
  return (
    <Image source={require('../assets/weblogo.png')} style={styles.image} resizeMode="contain" />
  )
}
const styles = StyleSheet.create({
    image: {
        marginTop: '10%',
        width: '90%',
        height: '20%',
      },
})