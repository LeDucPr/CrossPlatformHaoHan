import { urlHeader } from "../SetUp";
import api from '../SetUp/SetUpAxios';
export default async function UpdateRating(id) {
    try {
        const url = `/Book/UpdateBookReader?bookId=${id}&`;
        const response = await api.post(url, null, {
            headers: {
              Accept: '*/*',
            },
          });
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
    }
}