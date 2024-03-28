
import axios from "axios";
import Cookies from "js-cookie";
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/OrderDetails',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + Cookies.get('token')
            }
        }
    )
}


export const apiDeleteOrderDetail = (detailId) => {
    return request().delete('Delete/' + detailId);
}

export const apiUpdateOrderDetail = (orderDetail) => {
    return request().post('Update', orderDetail);
}