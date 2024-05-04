import { urlHeader } from "../SetUp";
import api from '../SetUp/SetUpAxios';
export default async function getUserSuggestion(userId) {
    try {
        const url = `/Book/GetCoversByClientId?clientId=${userId}&`;
        const response = await api.get(url);
        const data = await response.data;
        const uniqueData = data.filter((value, index, self) => {
            return self.indexOf(value) === index;
        })
        return uniqueData;
    } catch (error) {
        console.error('Error fetching data:', error);
        alert('Không tải được dữ liệu từ server');
        throw error;
    }
}