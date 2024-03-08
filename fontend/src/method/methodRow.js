

/**
         * Sự kiện khi bấm chọn shift + click
         * NTD 5/10/2022
         */
export function onShiftClick() {
    this.$emit("onShiftClick")
}
/**
 * Sự kiện khi bấm vào dong. chỉ chọn mỗi dòng ấy
 * NTD 5/10/2022
 */
 export function selectOnlyRow() {
    
        this.$emit("selectOnlyRow")
    
}
/**
 * Tính giá trị hao mòn lũy kế theo năm
 * NTD 24/8/2022
 */
export function calculateAttritionValue() {
    const today = new Date()
    const yearStartUsing = new Date(this.item.startUsingDate).getFullYear()
    const numberUsedYear = today.getFullYear() - yearStartUsing
    if(numberUsedYear > 0) {
        const value = this.item.markedPrice*numberUsedYear*this.item.attritionRate
        if(value > this.item.markedPrice) {
            return this.item.markedPrice
        } else {
            return value
        }
    }
    return 0
}
/**
 * Mở form chỉnh sửa từ button trong row
 * NTD 8/8/2022
 */
 export function openFormFix() {
    this.$emit("openFormItem");
}
/**
 * Sự kiện khi bấm vào ô chckbox hoặc là ctrl + click
 * NTD 9/8/2022
 */
 export function selectMultipleRow() {
    if (this.isSelect == true) {
        
        this.$emit("removeSelectAll");
        this.$emit("removeItemSelectList");
    }
    else {
        
        this.$emit("addItemSelectList");
    }
    
    // this.isSelect = !this.isSelect;
}
/**
 * chon row bang ctrl
 * NTD 6/9/2022
 */
 export function selectRowByCtrl() {
    if(this.isSelect == false){
        this.$emit("addItemSelectList");
    }
}
/**
 * Tạo bản sao
 * NTD 14/8/2022
 */
 export function createCopy() {
    this.$emit("createCopy");
}
/**
 * Mở contextmenu
 *  NTD 14/8/2022
 */
 export function openContextMenu(e) {
    this.$refs.menu.open(e);
}
/**
 * send lệnh xóa item
 * NTD 14/8/2022
 */
 export function deleteItem() {
    this.$emit('deleteItem')
}