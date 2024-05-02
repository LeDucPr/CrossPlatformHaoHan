import AsyncStorage from '@react-native-async-storage/async-storage';

export const getUserId = async () => {
  try {
    const data = await AsyncStorage.getItem('userData');
    if (data) {
      const userdata =  JSON.parse(data);
      return userdata.id;
    } else {
      return null;
    }
  } catch (error) {
    console.error('Error retrieving user data:', error);
    throw error;
  }
};