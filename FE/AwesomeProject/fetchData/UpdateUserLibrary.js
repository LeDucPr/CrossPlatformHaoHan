import { urlHeader } from "../SetUp";

export default async function UpdateUserLibrary(userId, bookId) {
    const url = `${urlHeader}/Client/UpdateBookIdsByClientId/` + userId + '/' + bookId;
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
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
    }
}