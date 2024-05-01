import  Resource  from "@/resource/MsResource";
 /**
 * Thay đổi number hiển thị
 * 
 */
export function  replaceNumber(number) {
    if(!number ) {
        return 0;
    }
    number = number.toFixed(0)
    return number.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, Resource.DigitGroupSeparator)
}
/***
 * fomat ngày tháng
 * 
 */
export function datetimeToDate(date) {
    if (date != null) {
        const current = new Date(date)
        var day
        if(current.getMonth()+1< 10) {
            if(current.getDate() < 10) {
                day = `${current.getFullYear()}-0${current.getMonth()+1}-0${current.getDate()}`
            } else {
                day = `${current.getFullYear()}-0${current.getMonth()+1}-${current.getDate()}`
            }
        } else {
            if(current.getDate() < 10) {
                day = `${current.getFullYear()}-${current.getMonth()+1}-0${current.getDate()}`
            } else {
                day = `${current.getFullYear()}-${current.getMonth()+1}-${current.getDate()}`
            }
        }
        return day
    }
    return null;
}

export function checkStatusOrder(status) {
    switch (status) {
        case 1:
            return "Đơn hàng chờ";
        case 2:
            return "Đơn hàng đợi thanh toán";
        case 3:
            return "Đã đặt hàng";
        case 4:
            return "Đã gửi hàng";
        case 5:
            return "Đã hoàn thành";
        default:
            return "";
    }
}

export function getCategoryTypeName(id) {
    switch (id) {
        case 1:
            return "Kích cỡ";
        case 2:
            return "Màu sắc";
        case 3:
            return "Sự kiện";
        case 4:
            return "Loại sản phẩm";
        case 5:
            return "Thương hiệu";
        default:
            return "";
    }
}

export function getOrderTypeName(orderType) {
    switch (orderType) {
        case 1:
            return "Đơn hàng thuê";
        case 2:
            return "Nhập kho";
        case 3:
            return "Thanh lý";
        default:
            return "";
    }
}
