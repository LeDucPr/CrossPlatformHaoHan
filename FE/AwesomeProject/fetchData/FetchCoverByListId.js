import { urlHeader } from "../SetUp";

async function fetchDataById(id) {
    const url = `${urlHeader}/Book/GetCoverById?bookId=${id}`;
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

export default async function fetchCoverDatas(ListBookId) {
    const fetchPromises = [];
    for (let i = 0; i < ListBookId.length; i++) {
        const id = ListBookId[i];
        fetchPromises.push(fetchDataById(id));
    }
    try {
        const coverData = await Promise.all(fetchPromises);
        return coverData;    
    } catch (error) {
        console.error('Error fetching intro data:', error);
        throw error;
    }
}


