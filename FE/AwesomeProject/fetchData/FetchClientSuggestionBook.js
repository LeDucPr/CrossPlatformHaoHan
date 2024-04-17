import { urlHeader } from "../SetUp";

export default async function getUserSuggestion(userId) {
    const url = `${urlHeader}/Book/GetCoversByClientId?clientId=${userId}`;
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