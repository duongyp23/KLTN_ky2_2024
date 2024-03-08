
/**
         * Kiểm tra xem có chọn tất cả các dữ liệu trong bảng ko
         * NTD 5/10/2022
         */
export function checkSelectAllRow() {
    var tmp = 0
    this.selectList.forEach(element1 => {
        this.datalist.forEach(element2 => {
            if(element1 == element2) {
                tmp++
            }
        });
    });
    if(tmp == this.datalist.length && tmp > 0) {
        this.isSelectAllRow = true
    }
}
/**
 * sự kiện khi bấm click + shift
 * NTD 5/10/2022
 */
 export function onShiftClick(index) {
    if(index <= this.focusIndex) {
        for (let i = this.focusIndex; i >= index; i--) {
            if(this.checkItemInList(this.datalist[i]) == false){
                this.selectList.push(this.datalist[i])
            }
        }
    } else {
        for (let i = this.focusIndex; i <= index; i++) {
            if(this.checkItemInList(this.datalist[i]) == false){
                this.selectList.push(this.datalist[i])
            }
        }
    }
    this.emitter.emit("checkRow")
    this.focusIndex = index
    if(this.selectList.length >= this.datalist.length) {
        this.checkSelectAllRow()
    }
}
/**
 * Kiểm tra item có trong danh sách chọn không
 * NTD 11/9/2022
 */
export function checkItemInList(item) {
    var check = false
    this.selectList.forEach(element => {
        if(element == item) {
            check = true
        }
    });
    return check
}
/**
 * Chọn Item  bằng enter
 * NTD 12/9/2022
 */
 export function selectItem() {
    if(this.datalist[this.focusIndex]) {
        if(this.checkItemInList(this.datalist[this.focusIndex])){
            this.removeItemSelectList(this.datalist[this.focusIndex], this.focusIndex)
        } else {
            this.addItemSelectList(this.datalist[this.focusIndex], this.focusIndex)
        }
    }
}
 /**
 * Thiết chọn lên xuống bằng bàn phím
 * NTD 8/9/2022
 */
  export function  setFocusDown() {
    if(this.focusIndex < this.datalist.length - 1 ) {
        this.focusIndex++
    }
    if(this.focusIndex*43 > this.$refs.tbody.clientHeight) {
        this.$refs.tbody.scrollTo(0, (this.focusIndex+1)*43 - this.$refs.tbody.clientHeight)
    }

}
/**
 * Thiết chọn lên xuống bằng bàn phím
 * NTD 8/9/2022
 */
 export function setFocusUp() {
    if(this.focusIndex > 0){
        this.focusIndex--
    }
    if((this.focusIndex+1)*43 > this.$refs.tbody.clientHeight) {
        this.$refs.tbody.scrollTo(0, (this.focusIndex+1)*43 - this.$refs.tbody.clientHeight)
    } else {
        this.$refs.tbody.scrollTo(0, 0)
    }
}
/**
 * Thay đổi số lượng bản ghi trong 1 trang
 * NTD 31/8/2022
 */
 export function changeNumberItemInTable(number) {
    this.numberItemInTable = number
    this.pageNumber = 1
    this.showSelect = false
}
/***
 * đóng select
 * NTD 31/8/2022
 */
 export function closeSelect() {
    this.showSelect = false
}
/**
 * Tính tổng số trang
 * NTD 22/8/2022
 */
 export function totalPage() {
    return Math.ceil(this.totalItem/this.numberItemInTable)
}
/**
 * Chọn tất cả các dòng trong table
 * NTD 10/8/2022
 */
 export function selectAllRow() {
    if(this.isSelectAllRow == false ) {
        this.datalist.forEach(property => {
            if(!this.checkItemInList(property)) {
                this.selectList.push(property)
            }
        })
        this.emitter.emit('checkRow')
    } else {
        this.selectList = []
        this.emitter.emit('checkRow')
    }
    this.isSelectAllRow = !this.isSelectAllRow
}
/**
 * Mở form fix item 
 * NTD 8/8/2022
 */
 export function fixItem(item) {
    this.emitter.emit('openFormFix', item)
}
/**
 * Chọn nguyên 1 dòng
 * NTD 5/10/2022
 */
 export function selectOnlyRow(item, index) {
    this.isSelectAllRow = false
    this.selectList = []
    this.selectList.push(item)
    this.emitter.emit("checkRow")
    this.focusIndex = index
}
/**
 * Thêm item vào select list
 * NTD 13/8/2022
 */
 export function addItemSelectList(item, index) {
    if(this.selectList.length == 0) {
        this.selectList = [item]
    } else {
        this.selectList.push(item)
    }
    this.emitter.emit("checkRow")
    this.focusIndex = index
    if(this.selectList.length >= this.datalist.length) {
        this.checkSelectAllRow()
    }
}
/**
 * Xóa item khỏi select list
 * NTD 13/8/2022
 */
 export function removeItemSelectList(item, index) {
    var tmpIndex = 0
    this.selectList.forEach(element => {
        if(element == item) {
            this.selectList.splice(tmpIndex, 1);
        }
        tmpIndex++
    });
    if(this.selectList.length == 0) {
        this.selectList = []
    }
    this.emitter.emit('checkRow')
    this.focusIndex = index
}
/**
 * tạo bản sao
 * NTD 14/8/2022
 */
 export function createCopy(item) {
    this.emitter.emit('openFormCopy', item)
}
/***
 * Tính tổng nguyên giá
 * NTD 15/8/2022
 */
 export function totalMarkedPrice() {
    var total = 0
    this.datalist.forEach(property => {
        total += property.markedPrice
    });
    return total
}
/**
 * Tính tổng giá trị hao mòn
 * 15/8/2022
 */
 export function totalAttritionValue () {
    var total = 0
    this.datalist.forEach(property => {
        total += property.attritionValue
    });
    return total
}
/**
 * Tính tổng số lượng
 * NTD 15/8/2022
 */
 export function totalQuantity () {
    var total = 0
    this.datalist.forEach(property => {
        total += property.quantity
    });
    return total
}