<template>
    <MsTool
    ></MsTool>
</template>
<script>
import MsTool from './MsTool.vue';
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import Resource from '@/resource/MsResource';
import { apiDeleteByID, apiDeleteMultiple, apiGetProperty } from '@/api/modules';
import {replaceNumber} from '@/method/methodFormat';
import { setFocusUp,setFocusDown,fixItem,removeItemSelectList,createCopy,addItemSelectList,selectOnlyRow,
    onShiftClick,changeNumberItemInTable,totalPage,totalQuantity,totalAttritionValue,selectAllRow,
    checkSelectAllRow, closeSelect, checkItemInList} from "@/method/methodTable";

export default {
    data() {
        return {
            isLoader: false, // hiện progress
            focusIndex: 0, // vị trí của item được focus
            keyword:null, // từ cần lọc
            pageNumber: 1, // số thứ tự page
            numberItemInTable: 20, // số bản ghi trong 1 page
            totalItem: 0, // Tổng số bản ghi
            isSelectAllRow: false, // chọn tất cả
            stt: 1, // số thứ tự
            showSelect: false, // hiển thị chọn số bản ghi
            departmentID: null, // ID phòng ban lọc
            propertyTypeID: null, // ID loại tài sản lọc
            
            datalist: [], // danh sách tài sản
            selectList:[], // danh sách tài sản được chọn
            tempItem:{}
        };
    },
    components: { MsTool},
    methods: {
        /**
         * Mở pop up thông báo xóa
         */
        openPopupDelete(item) {
            if(item) {
                this.emitter.emit('openPopupDeleteProperty', item)
                this.tempItem = item
            } else {
                this.emitter.emit('openPopupDeleteListProperty', this.selectList)
            }
        },
        checkItemInList,
        /**
         * Kiểm tra xem có chọn tất cả các dữ liệu trong bảng ko
         * NTD 5/10/2022
         */
        checkSelectAllRow,
        /**
         * sự kiện khi bấm click + shift
         * NTD 5/10/2022
         */
        onShiftClick,
         /**
         * Thiết chọn lên xuống bằng bàn phím
         * NTD 8/9/2022
         */
         setFocusDown,
        /**
         * Thiết chọn lên xuống bằng bàn phím
         * NTD 8/9/2022
         */
        setFocusUp,
        /**
         * Thay đổi số lượng bản ghi trong 1 trang
         * NTD 31/8/2022
         */
        changeNumberItemInTable,
        /***
         * đóng select
         * NTD 31/8/2022
         */
        closeSelect,
        /**
         * Tính tổng số trang
         * NTD 22/8/2022
         */
        totalPage,
        /**
         * Chọn tất cả các dòng trong table
         * NTD 10/8/2022
         */
        selectAllRow,
        /**
         * Mở form fix item 
         * NTD 8/8/2022
         */
        fixItem,
        /**
         * Chọn nguyên 1 dòng
         * NTD 5/10/2022
         */
        selectOnlyRow,
        /**
         * Thêm item vào select list
         * NTD 13/8/2022
         */
        addItemSelectList,
        /**
         * Xóa item khỏi select list
         * NTD 13/8/2022
         */
        removeItemSelectList,
        /**
         * tạo bản sao
         * NTD 14/8/2022
         */
        createCopy,
        /**
         * Xóa tài sản được chọn từ context menu
         * NTD 14/8/2022
         */
         deleteItem(item, index) {
            if(item.isActive == 0) {
                this.deleteItemById(item.propertyID)
                this.removeItemSelectList(item, index)
            }
        },
        /**
         * Thay đổi dữ liệu kiểu số
         * NTD 15/8/2022
         */
        replaceNumber,
        /***
         * Tính tổng nguyên giá
         * NTD 15/8/2022
         */
         totalMarkedPrice() {
            var total = 0
            this.datalist.forEach(property => {
                total += property.markedPrice
            });
            return total
        },
        /**
         * Tính tổng giá trị hao mòn
         * 15/8/2022
         */
        totalAttritionValue,
        /**
         * Tính tổng số lượng
         * NTD 15/8/2022
         */
        totalQuantity,
        /**
         * Lấy dữ liệu từ backend
         * NTD 25/8/2022
         */
        async getNewData() {
            
            this.isLoader=true
            this.datalist=[]
            await apiGetProperty(this.keyword, this.departmentID, this.propertyTypeID, this.numberItemInTable, this.pageNumber)
            .then(response => {
                
                this.datalist = response.data.data
                this.totalItem = response.data.totalCount
            })
            .catch( () => {
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorGetPaging)
            })
            this.isSelectAllRow = false
            this.focusIndex = 0
            this.$nextTick(() => this.$refs.table.focus())
            this.checkSelectAllRow()
            this.isLoader = false
            
        },
        /**
         * gọi gửi api xóa tài sản theo ID
         * NTD 25/8/2022
         */
        async deleteItemById(id) {
            await  apiDeleteByID(id)
            .then(() => {
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageDelete)
                this.getNewData()
            })
            .catch(() => {
                this.emitter.emit('openToastMessageError', Resource.ErrorMessage.ErrorMessageDelete)
            })
        },
        /***
         * Xóa nhiều tài sản
         * NTD 6/9/2022
         */
        async deleteMultipleItem() {
            const listID = []
            this.selectList.forEach(element => {
                listID.push(element.propertyID)
            });
            
            await  apiDeleteMultiple(listID)
            .then(response => {
                console.log(response.data)
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageDelete)
                if(this.pageNumber == 1) {
                    this.getNewData()
                } else {
                    this.pageNumber = 1
                }
                
            })
            .catch(() => {
                this.emitter.emit('openToastMessageError', Resource.ErrorMessage.ErrorMessageDelete)
            })
        }
    },
    
    mounted() {
        /**
         * Xóa tài sản khỏi list
         * NTD 13/8/2022
         */
        this.emitter.on('deleteSelectList', () => {
            if(this.selectList.length == 1){
                this.deleteItemById(this.selectList[0].propertyID)
            } else {
                this.deleteMultipleItem()
            }
            if(this.isSelectAllRow == true ) {
                this.isSelectAllRow = false
            }
            this.emitter.emit('removeSelectAllRow')
            this.selectList = []
        }) 

        /**
         * Xóa tài sản khỏi list
         * NTD 13/8/2022
         */
         this.emitter.on('deleteProperty', () => {
            
            this.deleteItemById(this.tempItem.propertyID)
            
            if(this.isSelectAllRow == true ) {
                this.isSelectAllRow = false
            }
            this.emitter.emit('removeSelectAllRow')
            this.selectList = []
        }) 
        /**
         * Lọc Item theo Type
         * NTD 14/8/2022
         */
        this.emitter.on('sortType',(id) => {
            this.propertyTypeID = id
        }),
        /**
         * Lọc item theo bộ phận
         * NTD 14/8/2022
         */
        this.emitter.on('sortDepartment',(id) => {
            this.departmentID = id
        }),
        /**
         * Lọc tài sản theo tên
         * NTD 14/8/2022
         */
        this.emitter.on('searchItemInList',(valueSearch) => {
           this.keyword = valueSearch
        }),
        /***
         * load lại page
         * NTD 5/9/2022
         */
        this.emitter.on('loadNewTable', () => {
            if(this.pageNumber !=1 ){
                this.pageNumber =1
            } else {
                this.getNewData()
            }
        }),
        /**
         * Focus vào table
         * NTD 5/9/2022
         */
        this.emitter.on('focusTable', () => {
            this.$nextTick(() => this.$refs.table.focus())
        })
    },
    created() {
        /***
         * Lấy dữ liệu khi tạo component
         * NTD 25/8/2022
         */
        this.getNewData()
                  
    },
    watch: {
        /**
         * Kiểm tra xem có chọn hết tất cả các giá trị trong bảng
         * NTD 5/10/2022
         */
        selectList() {
            
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
        numberItemInTable() {
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getNewData()
            }
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
        pageNumber() {
            this.getNewData()
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
        propertyTypeID() {
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getNewData()
            }
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
        departmentID() {
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getNewData()
            }
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
        keyword(){
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getNewData()
            }
        }
    },

}
</script>
<style>
 @import url(@/css/layout/page.css);
@import url(@/css/layout/table.css);
@import url(@/css/base/button.css); 
</style>