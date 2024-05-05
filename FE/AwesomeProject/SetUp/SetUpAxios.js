import axios from 'axios';
import { getUserToken } from '../SetUp/GetUserToken';
import { getUserId } from '../SetUp/GetUserId';
//Tạo axioi với baseURl như dưới
const api = axios.create({
  baseURL: 'http://192.168.0.100:5142',
});
//Xử lý trước khi gửi request, cộng thêm userID với token vào đuổi url
api.interceptors.request.use(async (config) => {
  const userId = await getUserId();
  const token = await getUserToken();

  if (userId && token) {
    // config.url += `userId=${userId}&token=${token}`;
    config.headers['userId'] = userId;
    config.headers['token'] = token;

  }

  return config;
});

export default api;