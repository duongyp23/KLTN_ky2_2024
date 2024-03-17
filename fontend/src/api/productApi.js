
import axios from "axios";
import Cookies from "js-cookie";
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/Products',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + Cookies.get('token')
            }
        }
    )
}

export const apiGetPagingProduct = (filter, pageSize, pageNumber) => {
    return request().post('Paging?pageSize=' + pageSize + '&pageNumber=' + pageNumber, filter)
}

export const apiInsertProduct = (product) => {
    return request().post('Add',  product )
}

export const apiUpdateProduct = (product) => {
    return request().post('Update', product)
}

export const apiGetAllProduct = (filter) => {
    return request().post('GetAll', filter);
} 
