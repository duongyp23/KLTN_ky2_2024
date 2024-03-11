
import axios from "axios";
import Cookies from "js-cookie";
/**
 * Tạo request() 
 * NTD 3/10/2022
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/v1/',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + Cookies.get('token')
            }
        }
    )
}
/**
 * Api đăng nhập
 * NTD 2/10/2022
 */
export const apiLogin = (user) => {
    return request().post("Users", user)
}

/**
 * Api lấy danh sách phòng ban
 * NTD 2/10/2022
 * @returns 
 */
export const apiGetDepartment = () => {
    return request().get("Departments")
}

/**
 * Api lấy danh sách loại tài sản
 * NTD 2/10/2022
 * @returns 
 */
export const apiGetPropertyType = () => {
    return request().get("PropertyTypes")
}



/**
 * Api lấy trang tài sản
 * NTD 2/10/2022
 * @param {*} keyword 
 * @param {*} departmentID 
 * @param {*} propertyTypeID 
 * @param {*} numberItemInTable 
 * @param {*} pageNumber 
 * @returns 
 */
export const apiGetProperty = (keyword, departmentID, propertyTypeID, numberItemInTable, pageNumber) => {
    var query = ''
    if (keyword) {
        query = query + `keyword=` + keyword + `&`
    }
    if (departmentID) {
        query = query + `departmentID=` + departmentID + `&`
    }
    if (propertyTypeID) {
        query = query + `propertyTypeID=` + propertyTypeID + `&`
    }
    return request().get('Properties?' + query + 'pageSize=' + numberItemInTable + '&pageNumber=' + pageNumber);

}

/**
 * Lấy danh sách tài sản chưa ghi tăng
 * NTD 11/10/2022
 * @param {*} keyword 
 */
export const apiGetPropertyNotActive = (keyword, numberItemInTable, pageNumber, listPropertyID) => {
    var query = ''
    if (keyword) {
        query = query + `keyword=` + keyword + `&`
    }
    return request().post('Properties/not-active?' + query + 'pageSize=' + numberItemInTable + '&pageNumber=' + pageNumber, listPropertyID);
}
/**
 * Api lấy trang ghi tăng
 * NTD 2/10/2022
 * @param {*} keyword 
 * @param {*} numberItemInTable 
 * @param {*} pageNumber 
 * @returns 
 */
export const apiGetPagingVoucher = (keyword, numberItemInTable, pageNumber) => {
    var query = ''
    if (keyword) {
        query = query + `keyword=` + keyword + `&`
    }
    return request().get('Vouchers?' + query + 'pageSize=' + numberItemInTable + '&pageNumber=' + pageNumber);
}
/**
 * lấy danh sách property trong voucher
 * NTD 10/10/2022
 */
export const apiGetPropertyInVoucher = (id) => {
    return request().get(`Vouchers/listProperty/${id}`)
}

/**
 * Thêm mpowis 1 tài sản
 * NTD 15/10/2022
 * @param {*} voucher 
 * @param {*} listProperty 
 * @returns 
 */
export const apiInsertVoucher = (voucher, listProperty) => {

    return request().post('Vouchers', {
        VoucherData: voucher,
        ListProperty: listProperty
    })
}

export const apiUpdateVoucher = (voucher, listProperty) => {
    return request().put(`Vouchers/${voucher.VoucherID}`, {
        VoucherData: voucher,
        ListProperty: listProperty
    })
}

/**
 * Api xóa tài sản theo ID
 * NTD 2/10/2022
 * @param {*} id 
 * @returns 
 */
export const apiDeleteByID = (id) => {
    return request().delete(`Properties/${id}`)
}


/**
 * Api xóa tài sản theo ID
 * NTD 2/10/2022
 * @param {*} id 
 * @returns 
 */
export const apiDeleteVoucherByID = (id) => {
    return request().delete(`Vouchers/${id}`)
}
/**
 * Api xóa nhiều tài sản
 * NTD 2/10/2022
 * @param {*} listID 
 * @returns 
 */
export const apiDeleteMultiple = (listID) => {
    return request().post(`Properties/delete-multiple`, listID)
}

/**
 * Api xóa nhiều voucher
 * NTD 2/10/2022
 * @param {*} listID 
 * @returns 
 */
export const apiDeleteMultipleVoucher = (listID) => {
    return request().post(`Vouchers/delete-multiple`, listID)
}

/**
 * Api lấy mã tài sản mới
 * NTD 2/10/2022
 * @returns 
 */
export const apiGetNewCode = () => {
    return request().get(`Properties/new-code`)
}

/**
 * Api lấy mã voucher mới
 * NTD 2/10/2022
 * @returns 
 */
export const apiGetNewVoucherCode = () => {
    return request().get(`Vouchers/new-code`)
}
/**
 * Api lấy thông tin tài sản theo id
 * NTD 2/10/2022
 * @param {*} id 
 * @returns 
 */
export const apiGetPropertyByID = (id) => {
    return request().get(`Properties/${id}`)
}

/**
 * Api lấy thông tin ghi tăng theo id
 * NTD 2/10/2022
 * @param {*} id 
 * @returns 
 */
export const apiGetVoucherByID = (id) => {
    return request().get(`Vouchers/${id}`)
}
/**
 * api thêm mới nhãn dán
 */
export const apiAddNewCategory = (category) => {
    return request().post(`Categorys`, category)
}
/**
 * Api sửa tài sản
 * NTD 2/10/2022
 */
export const apiUpdateProperty = (id, property) => {
    return request().put(`Properties/${id}`, property)
}
/**
 * Api thêm mới nhiều tài sản
 * NTD 2/10/2022
 */
export const apiAddMultiple = (listProperty) => {
    return request().post(`Properties/import-multiple`, listProperty)
}

export const apiGetBudget = () => {
    return request().get('Budgets')
}