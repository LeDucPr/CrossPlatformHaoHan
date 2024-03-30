import React from 'react';
import { View, Image, StyleSheet, Text, Animated, Dimensions } from 'react-native';

const {width} = Dimensions.get('screen')

export default function Pagination({data, scrollX, index}) {
  return (
    <View style = {styles.container}>
        {
            data.map((_, idx) => {
                const inputRange = [(idx - 1)*width, idx*width, (idx + 1)*width]
                const dotWidth = scrollX.interpolate({
                    inputRange,
                    outputRange: [12,30,12],
                    extrapolate: 'clamp',
                })
                const opacity = scrollX.interpolate({
                    inputRange,
                    outputRange: [0.2, 1, 0.1],
                    extrapolate: 'clamp',
                  });
                  const backgroundColor = scrollX.interpolate({
                    inputRange,
                    outputRange: ['#ccc', '#DE3535', '#ccc'],
                    extrapolate: 'clamp',
                  });
                return <Animated.View key ={idx.toString()} style={[styles.dot, {width: dotWidth, backgroundColor},  //idx === index && styles.dotActive,
            ]}/>;  
            })}
    </View>
  );
}

const styles = StyleSheet.create({
    container: {
        position: 'absolute',
        bottom: "5%", 
        flexDirection: 'row',
        width : '100%',
        alignItems: 'center',
        justifyContent: 'center',
    },
    dot:{
        width: 12,
        height: 12,
        marginHorizontal:3,
        borderRadius: 6,
        backgroundColor: '#CCC',
    },
    dotActive: {
        backgroundColor: '#000',
      },
});