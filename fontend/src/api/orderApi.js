
import axios from "axios";
import Cookies from "js-cookie";
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/Orders',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + Cookies.get('token')
            }
        }
    )
}


export const apiUpdateOrder = (order) => {
    return request().post('Update', order)
}

export const apiUpdateOrderData = (order, orderDetails) => {
    return request().post('UpdateOrderData', { order, orderDetails })
}

export const apiGetOrder = (orderId) => {
    return request().get(orderId)
}

export const apiGetOrderOfUser = (userId) => {
    return request().get('GetOrderOfUser/'+userId)
}

export const apiGetAllOrder = () => {
    return request().get('GetAll');
}

export const apiGetPagingOrder = (filter, pageSize, pageNumber) => {
    return request().post('Paging?pageSize=' + pageSize + '&pageNumber=' + pageNumber, filter)
}