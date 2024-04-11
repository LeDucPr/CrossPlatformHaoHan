import { urlHeader } from "../SetUp";

export default async function fetchCoversDataFromFieldsContrains(amountWord, amountCovers, skipIds, fields) {
    const url = `${urlHeader}/Book/GetCoversByFields(Contrains)?LetterTrueWordFalse=false&amountWords=${amountWord}`;
    const options = {
      method: 'PUT',
      headers: {
        'Accept': 'text/plain',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        amountCovers: amountCovers,
        skipIds: skipIds,
        fields: fields,
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