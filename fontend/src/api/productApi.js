
import axios from "axios";
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/Products',
            timeout: 5000,
            headers: {
                Authorization: "Bearer "
            }
        }
    )
}

export const apiGetPagingProduct = (filter, pageSize, pageNumber) => {
    return request().post('Paging?pageSize=' + pageSize + '&pageNumber=' + pageNumber, filter)
}

export const apiInsertProduct = (product, selectCategory) => {
    return request().post('AddProduct', { product, selectCategory } )
}

export const apiUpdateProduct = (product, selectCategory) => {
    return request().post('UpdateProduct', { product, selectCategory })
}

export const apiGetAllProduct = (filter) => {
    return request().post('GetAll', filter);
} 

export const apiAddProductToCart = (productId, userId, optionCode, quantity) => {
    return request().post('AddProductToCart?productId=' + productId + '&userId=' + userId+'&optionCode='+optionCode+'&quantity='+quantity);
}

export const apiDeleteProduct = (id) => {
    return request().delete('Delete/' + id);
}

export const apiGetProduct = (id) => {
    return request().get(id);
}
