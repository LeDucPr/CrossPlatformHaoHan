import {
    Text,
    View,
    StyleSheet,
    TextInput,
    TouchableOpacity,
    ImageBackground,SafeAreaView, FlatList, Dimensions, Animated,
  } from 'react-native';
import React, {useRef, useState, useEffect,}   from 'react';
import SliderItem from './SlideItem';
import Slides from '../../data';
import WebLogo from '../WebLogo';
import Pagination from './Pagination';


  export default function Slider()  {
    const [index, setIndex] = useState(0);
    const scrollX = useRef(new Animated.Value(0)).current;
    
    const flatlistRef = useRef();
    const screenWidth = Dimensions.get("window").width;
    const [activeIndex, setActiveIndex] = useState(0);

    // Auto Scroll

    useEffect(() => {
      let interval = setInterval(() => {
        if (index === Slides.length - 1) {
          flatlistRef.current.scrollToIndex({
            index: 0,
            animation: true,
          });
        } else {
          flatlistRef.current.scrollToIndex({
            index: index + 1,
            animation: true,
          });
        }
      }, 2000);

      return () => clearInterval(interval);
      });

	const getItemLayout = (data, index) => ({
		length: screenWidth,
		offset: screenWidth * index, 
		index: index,
	});

    const handleOnScroll = event => {
      Animated.event(
        [
          {
            nativeEvent: {
              contentOffset: {
                x: scrollX,
              },
            },
          },
        ],
        {
          useNativeDriver: false,
        },
      )(event);
    };

    const handleOnViewableItemsChanged = useRef(({viewableItems}) => {
      //console.log('viewableItems', viewableItems[0].index);
      setIndex(viewableItems[0].index);
    }).current;

    const viewabilityConfig = useRef({
      viewAreaCoveragePercentThreshold: 50,
    }).current;
    
  
    return (
        <View>
            <FlatList
            data={Slides}
            ref={flatlistRef}
				    getItemLayout={getItemLayout}
            renderItem={({item}) => <SliderItem item = {item}/>}
            horizontal
            pagingEnabled
            snapToAlignment='center'
            showsHorizontalScrollIndicator ={false}
            onScroll={handleOnScroll}
            onViewableItemsChanged={handleOnViewableItemsChanged}
            viewabilityConfig={viewabilityConfig}
            />
            <Pagination data ={Slides} scrollX = {scrollX} index = {index}/>
        </View>
        
    );
  }
  
 
  
  const styles = StyleSheet.create({
  })