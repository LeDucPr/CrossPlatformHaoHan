import { urlHeader } from "../SetUp";

export default async function fetchDataFromFields(amountIntros, skipIds, fields) {
    const url = `${urlHeader}/Book/GetSomeByFields`;
    const options = {
      method: 'PUT',
      headers: {
        'Accept': 'text/plain',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        amountIntros: amountIntros,
        skipIds: skipIds,
        fields: fields
      })
    };
  
    try {
      const response = await fetch(url, options);
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