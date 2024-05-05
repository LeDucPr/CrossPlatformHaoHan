import { urlHeader } from "../SetUp";
import axios from 'axios';
import api from '../SetUp/SetUpAxios';
import {getUserToken} from '../SetUp/GetUserToken';
import {getUserId} from '../SetUp/GetUserId';

async function fetchDataById(id) {
    try {
        const url = `/Book/GetCoverById?bookId=${id}`;
        const response = await api.get(url);
        const data = await response.data;
        return data;
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
    }
}

export default async function fetchIntroData(quantity) {
    // const ids = ["0001", "0002", "0003", "0004", "0005", "0006", "0007", "0008"];
    const ids = Array.from({ length: quantity }, (_, index) => {
        const idNumber = index + 1;
        const idString = idNumber.toString().padStart(4, '0');
        return idString;
    });
    const fetchPromises = ids.map((id) => fetchDataById(id, 0));
    try {
        const introData = await axios.all(fetchPromises);

        return introData;
    } catch (error) {
        console.error('Error fetching intro data:', error);
        alert('Không tải được dữ liệu từ server');
        throw error;
    }
}


