import  Resource  from "@/resource/MsResource";
 /**
 * Thay đổi number hiển thị
 * 
 */
export function  replaceNumber(number) {
    if(number ==0 ) {
        return null
    }
    number = number.toFixed(0)
    return number.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, Resource.DigitGroupSeparator)
}
/***
 * fomat ngày tháng
 * 
 */
export function datetimeToDate(date) {
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