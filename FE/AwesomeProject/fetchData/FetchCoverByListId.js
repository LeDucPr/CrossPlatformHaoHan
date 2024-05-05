import api from '../SetUp/SetUpAxios';
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
        alert('Không tải được dữ liệu từ server');
        throw error;
    }
}


