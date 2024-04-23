
import axios from "axios";
import Cookies from "js-cookie";
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/Users',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + Cookies.get('token')
            }
        }
    )
}

export const apiLogin = (user) => {
    return request().post('Login', user)
}

export const apiUpdateUserInfo = (user) => {
    return request().post('Update', user)
}

export const apiRegister = (user) => {
    return request().post('Register', user)
}

export const apiGetInfoUser = (user_id) => {
    return request().get(user_id);
}

export const apiGetBankAccountAdmin = () => {
    return request().get("GetBankAccountAdmin");
}

export const apiGetWaitOrder = (userId) => {
    return request().get('GetWaitOrder?userId=' + userId);
}

export const apiGetAllUser = (filter) => {
    return request().post('GetAll', filter);
} 
