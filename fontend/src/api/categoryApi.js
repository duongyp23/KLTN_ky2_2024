
import axios from "axios";
import Cookies from "js-cookie";
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/Categorys',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + Cookies.get('token')
            }
        }
    )
}

export const apiGetPagingCategory = (filter, pageSize, pageNumber) => {
    return request().post('Paging?pageSize=' + pageSize + '&pageNumber=' + pageNumber, filter)
}
