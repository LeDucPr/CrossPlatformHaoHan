import { urlHeader } from "../SetUp";

export default async function UpdateRating(id) {
    const url = `${urlHeader}/Book/UpdateBookRating?bookId=${id}`;
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                Accept: '*/*',
            },
        });
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