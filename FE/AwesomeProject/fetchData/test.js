import { urlHeader } from "../SetUp";

async function fetchDataById(id, amount) {
    const url = `${urlHeader}/Book/GetIntroById?bookId=${id}&amountPage=${amount}`;
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();
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
        const introData = await Promise.all(fetchPromises);

        return introData;
    } catch (error) {
        console.error('Error fetching intro data:', error);
        throw error;
    }
}


