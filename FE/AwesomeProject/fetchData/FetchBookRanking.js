import { urlHeader } from "../SetUp";
import api from '../SetUp/SetUpAxios';
export default async function fetchBooksRanking(amountCover, skipIds) {
    try {
        let url = `/Book/GetCoverByDesReaders?amountCovers=${amountCover}`;
        if (skipIds && skipIds.length > 0) {
            const skipIdsString = skipIds.map((id) => `skipIds=${id}`).join('&');
            url += `&${skipIdsString}`;
          }
        const response = await api.get(url);
        const data = await response.data;
        return data;
    } catch (error) {
        console.error('Error fetching data:', error);
        alert('Không tải được dữ liệu từ server');
        throw error;
    }
}