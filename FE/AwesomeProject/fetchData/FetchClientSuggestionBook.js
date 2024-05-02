import { urlHeader } from "../SetUp";
import api from '../SetUp/SetUpAxios';
export default async function getUserSuggestion(userId) {
    try {
        const url = `/Book/GetCoversByClientId?clientId=${userId}&`;
        const response = await api.get(url);
        const data = await response.data;
        return data;
    } catch (error) {
        console.error('Error fetching data:', error);
        alert('Không tải được dữ liệu từ server');
        throw error;
    }
}