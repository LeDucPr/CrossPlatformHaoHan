import { urlHeader } from "../SetUp";
import { getUserToken } from '../SetUp/GetUserToken';
import { getUserId } from '../SetUp/GetUserId';
import api from '../SetUp/SetUpAxios';
export default async function fetchCoversDataFromFieldsContrains(amountWord, amountCovers, skipIds, fields) {
  try {
    const url = `/Book/GetCoversByFields(Contrains)?LetterTrueWordFalse=false&amountWords=${amountWord}&`;
    const headers = {
      'accept': 'text/plain',
      'Content-Type': 'application/json'
    };
    const input = {
      "amountCovers": amountCovers,
      "skipIds": skipIds,
      "fields": fields
    };
    const response = await api.put(url, input, { headers })
    const data = response.data;
    return data;
  } catch (error) {
    console.error('Error fetching data:', error);
    alert('Không tải được dữ liệu từ server');
    throw error;
  }
}