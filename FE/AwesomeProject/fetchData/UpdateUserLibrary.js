import { urlHeader } from "../SetUp";
import api from '../SetUp/SetUpAxios';
export default async function UpdateUserLibrary(userId, bookId) {
    try {
        const url = `/Client/UpdateBookIdsByClientId/` + userId + '/' + bookId +"?";
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