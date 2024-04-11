import { urlHeader } from "../SetUp";

export default async function getImagesForBook(bookId, skipImages, takeImages) {
    const url = `${urlHeader}/Book/GetNextImagesForContent?bookId=${bookId}&skipImages=${skipImages}&takeImages=${takeImages}`;
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