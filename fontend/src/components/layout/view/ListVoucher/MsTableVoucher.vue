<template>
    <MsToolVoucher
        v-model:sizeViewProperty = "sizeViewProperty"
    ></MsToolVoucher>
    <Splitpanes horizontal>
        <Pane style="background-color: #fff;display: flex;flex-direction: column;" :size="100-sizeViewProperty" >
            <div style="padding: 12px;display:flex;flex-direction: row; justify-content: space-between; ">
                <div class="input-tool" style="width: 270px;">
                    <div class="icon icon-search-input" style="margin-left:10px; position: absolute;"></div>
                    <input 
                        type="text" class="input-box" 
                        style="margin-left:39px" 
                        placeholder="Tìm kiếm theo số chứng từ, nội dung" 
                        @change="keyword = $event.target.value"
                        @input="checkInputNull($event.target.value)"
                    >
                    
                </div>
                <div style="display:flex;flex-direction: row; column-gap: 10px;">
                    <button class="btn-icon" v-show="selectList.length > 1" @click="openPopupDeleteVoucher()" title="Xóa nhiều">
                        <div class="icon icon-trash"></div>
                    </button>
                    <button class="btn-icon" title="in">
                        <div class="icon icon-print"></div>
                    </button>
                    <button class="btn-icon " title="...">
                        <div class="icon icon-dot-column"></div>
                    </button>
                </div>
            </div>
            <table class="table"
                tabindex="0"
                @keyup.down="setFocusDown()" 
                @keyup.up="setFocusUp()" 
                ref="table"
            >
                <thead>
                    <tr>
                        <th class="col-check" @click="selectAllRow"><div class="icon icon-box" :class="{'selectAllRow': isSelectAllRow}"></div></th>
                        <th  class="col-stt" title="Số thứ tự">STT</th>
                        <th class="col-code">Số chứng từ</th>
                        <th class="col-date">Ngày chứng từ</th>
                        <th class="col-date">Ngày ghi tăng</th>
                        <th class="col-value">Tổng nguyên giá</th>
                        <th class="col-description">Nội dung</th>
                        <th class="col-fix">Chức năng</th>
                    </tr>
                </thead>
                <tbody ref="tbody">
                    <MsRowVoucher 
                        v-for="(item, index) in datalist"
                        :item = item
                        :key="item.voucherID"
                        :stt = "index+1"
                        :selectList = "selectList"
                        @removeSelectAll="isSelectAllRow = false"
                        @removeItemSelectList = "removeItemSelectList(item, index)"
                        @addItemSelectList = "addItemSelectList(item, index)"
                        @selectOnlyRow="selectOnlyRow(item, index)"
                        @onShiftClick="onShiftClick(index)"
                        @loadNewTable="getPagingVoucher()"
                        @deleteVoucher = "openPopupDeleteVoucher(item)"
                        :class="{'focus-row' : index == focusIndex}"
                    ></MsRowVoucher>
                    
                    <div class="loader" v-if="isLoader"></div> 
                </tbody>
                <tfoot style="background-color: #fff">
                    <tr style="background-color: #f5f5f5;">
                        <td style="width: 31%"></td>
                        <th class="col-value">{{replaceNumber(totalPrice())}}</th>
                        <th class="col-description"></th>
                        <th class="col-fix"></th>
                    </tr>
                    <tr>
                        <td style="width:31%;font-size: 11px;">
                            <div style="margin-left:16px; ">Tổng số: <b style="font-weight:700">{{totalItem}}</b> bản ghi</div>
                            
                            <div class="select-number-footer" @click="showSelect = true"  v-click-outside="closeSelect">
                                <div 
                                    class="option-box" 
                                    v-show="showSelect"
                                >
                                    <div class="option-item" @click.stop="changeNumberItemInTable(10)">10</div>
                                    <div class="option-item" @click.stop="changeNumberItemInTable(20)">20</div>
                                    <div class="option-item" @click.stop="changeNumberItemInTable(50)">50</div>
                                </div>
                                <div class="select-number">{{numberItemInTable}}</div>
                                <div class="icon icon-mini-down" style="position: absolute; left:40px; top:10px; pointer-events: none;"></div>
                            </div>
                            <v-pagination
                                style="position:absolute; bottom: 7px;left: 230px;bottom: 10px;"
                                v-model="pageNumber"
                                :pages="totalPage()"
                                :range-size="1"
                                active-color="#f5f5f5"
                                :hideFirstButton = "true"
                                :hideLastButton = "true"
                                
                            ></v-pagination>
                        </td>
                        <th class="col-value"></th>
                        <th class="col-description"></th>
                        <th class="col-fix"></th>
                    </tr>
                </tfoot>
            </table>
        </Pane>
        <Pane style="background-color: #fff;" :size="sizeViewProperty">
            <div style="padding: 12px;display:flex;flex-direction: row; justify-content: space-between; ">
                <label><b>Thông tin chi tiết</b></label>
                <button class="btn-icon " style="margin:0px" @click="sizeViewProperty = 100" v-show="sizeViewProperty <= 30" title="Phóng to">
                    <div class="icon icon-zoom-in"></div>
                </button>
                <button class="btn-icon " style="margin:0px" @click="sizeViewProperty = 30" v-show="sizeViewProperty > 30" title="Thu nhỏ">
                    <div class="icon icon-zoom-out"></div>
                </button>
            </div>
            
            <table class="table" @mousedown.right.prevent style="height: calc(100% - 48px)">
                <thead>
                    <tr>
                        <th  class="col-stt" title="Số thứ tự">
                            STT
                        </th>
                        <th  class="col-code" >Mã tài sản</th>
                        <th  class="col-name" >Tên tài sản</th>
                        <th  class="col-department" >Bộ phận sử dụng</th>
                        <th  class="col-value">Nguyên giá</th>
                        <th class="col-value" title="Hao mòn/Khấu hao lũy kế" >
                            Hao mòn năm
                        </th>
                        <th  class="col-value" >Giá trị còn lại</th>
                      
                    </tr>
                </thead>
                <tbody>
                    <MsRow
                    v-for="(item, index) in listPropertyInVoucher"
                        :item = item
                        :key="item.propertyID"
                        :stt = "index+1"
                        :showCheckbox="false"
                        :showInfo="false"
                        :selectList = "[]"
                        :showContext="false"
                        :showFix="false"
                    ></MsRow>
                    
                    <div class="loader" v-if="isLoader2"></div> 
                </tbody>
                <tfoot>
                    <tr style="background-color:#f5f5f5">
                        <td style="width: 34%;">
                            
                        </td>
                        <th  class="col-value">{{replaceNumber(totalMarkedPrice())}}</th>
                        <th class="col-value">{{replaceNumber(totalAttritionValue())}}</th>
                        <th  class="col-value" >{{replaceNumber(totalMarkedPrice() - totalAttritionValue())}}</th>
                    </tr>
                </tfoot>
            </table>
        </Pane>
    </Splitpanes>
</template>
<script>
import Resource from "@/resource/MsResource";
import MsToolVoucher from "./MsToolVoucher.vue";
import { Splitpanes, Pane } from 'splitpanes'
import 'splitpanes/dist/splitpanes.css'
import MsRowVoucher from "@/components/base/MsRowVoucher.vue";
import MsRow from "@/components/base/MsRow.vue";
import {apiGetPagingVoucher, apiGetPropertyInVoucher, apiDeleteMultipleVoucher, apiDeleteVoucherByID} from "@/api/modules";
import VPagination from "@hennge/vue3-pagination";
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import {replaceNumber} from '@/method/methodFormat';
import { setFocusUp,setFocusDown,removeItemSelectList,addItemSelectList,selectOnlyRow,
    onShiftClick,changeNumberItemInTable,totalPage,selectAllRow,
    checkSelectAllRow, closeSelect, checkItemInList} from "@/method/methodTable";
export default ({
    data() {
        return {
            sizeViewProperty: 30,
            isLoader2: false,
            isLoader: false,
            listPropertyInVoucher: [],
            focusIndex: 0,
            selectList:[],
            showSelect :false,
            isSelectAllRow: false,
            datalist: [],
            keyword:null, // từ cần lọc
            pageNumber: 1, // số thứ tự page
            numberItemInTable: 20, // số bản ghi trong 1 page
            totalItem: 0, // Tổng số bản ghi
            tempVoucher: {},
        } 
    },
    methods: {
        /**
         * Kiểm tra khi nhập input null
         * NTD 2/11/2022
         * @param {*} inputValue 
         */
        checkInputNull(inputValue) {
            if(inputValue == null || inputValue == "") {
                this.keyword = null
            }
        },
        /**
         * Gửi popup delete
         * NTD 2/11/2022
         * @param {*} voucher 
         */
        openPopupDeleteVoucher(voucher) {
            if(voucher) {
                this.emitter.emit('openPopupDeleteVoucher', voucher)
                this.tempVoucher = voucher
            } else {
                this.emitter.emit('openPopupDeleteVoucherList', this.selectList)
            }
        },
        /**
         * Format lại kiểu số
         */
        replaceNumber,
        /**
         * Sự kiện khi bấm mũi tên lên
         */
        setFocusUp,
        /**
         * Sự kiện khi bấm mũi tên xuống
         */
        setFocusDown,
        /**
         * Xóa item khỏi selectlist
         */
        removeItemSelectList,
        /**
         * thêm item vào list được chọn
         */
        addItemSelectList,
        /**
         * Chọn 1 item
         */
        selectOnlyRow,
        /**
         * chọn banwgf shift
         */
        onShiftClick,
        /**
         * Thay đổi số item hiển thị
         */
        changeNumberItemInTable,
        /**
         * tỉnh tổng số trang
         */
        totalPage,
        /**
         * chọn tất cả
         */
        selectAllRow,
        /**
         * Kiểm tra chọn tất cả
         */
        checkSelectAllRow, 
        /**
         * đóng chọn số lượng hiển thị 1 trang
         */
        closeSelect, 
        /**
         * Kiểm tra 1 item trong list đã chọn
         */
        checkItemInList,
        /**
         * Xóa nhiều
         * NTD 19/10/2022
         */
        async deleteMultipleVoucher() {
            const listID = []
            this.selectList.forEach(element => {
                listID.push(element.voucherID)
            });
            
            await  apiDeleteMultipleVoucher(listID)
            .then(() => {
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageDelete)
                if(this.pageNumber == 1) {
                    this.getPagingVoucher()
                } else {
                    this.pageNumber = 1
                }
                this.selectList = []
            })
            .catch(() => {
                this.emitter.emit('openToastMessageError', Resource.ErrorMessage.ErrorMessageDelete)
            })
        },
        /**
         * Tổng nguyên giá của bảng nhỏ
         * NTD 19/10/2022
         */
        totalMarkedPrice() {
            var total = 0
            this.listPropertyInVoucher.forEach(element => {
                total += element.markedPrice
            }); 
            return total
        },
        /**
         * Tổng hoa mòn của bảng tài sản
         * \NTD 19/10/2022
         */
        totalAttritionValue( ) {
            var total = 0
            this.listPropertyInVoucher.forEach(element => {
                total += element.attritionValue
            }); 
            return total
        },
        /**
         * tổng của các voucher
         * NTD 19/10/2022
         */
        totalPrice(){
            var total = 0
            this.datalist.forEach(element => {
                total += element.totalPrice
            }); 
            return total
        },
        /**
         * Lấy danh sách voucher
         * NTD 19/10/2022
         */
         async getPagingVoucher() {
            this.isLoader = true
            this.datalist=[]
            await apiGetPagingVoucher(this.keyword, this.numberItemInTable, this.pageNumber)
            .then(response => {
                
                this.datalist = response.data.data
                this.totalItem = response.data.totalCount
            })
            .catch( () => {
            })
            this.isLoader = false
            if(this.selectList.length == 0) {
                this.getPropertyInVoucher(this.datalist[0].voucherID)
            }
        },
        /**
         * Lấy danh sách tái ản của voucher
         * NTD 19/10/2022
         * @param {*} id 
         */
        async getPropertyInVoucher(id) {
            this.isLoader2 = true
            this.listPropertyInVoucher = []
            await apiGetPropertyInVoucher(id)
            .then(response => {
                this.listPropertyInVoucher = response.data.data
            })
            .catch()
            this.isLoader2 = false
        },
        async deleteVoucherByID(id) {
            await apiDeleteVoucherByID(id)
            .then(() => {
                this.getPagingVoucher()
            })
        }
    },
    components: {
        MsToolVoucher,Splitpanes,Pane,MsRowVoucher,VPagination,MsRow
    },
    mounted() {
        this.emitter.on('deleteVoucher', () => {
            this.deleteVoucherByID(this.tempVoucher.voucherID)
        })

        this.emitter.on('deleteVoucherList', () => {
            this.deleteMultipleVoucher()
        })

        this.emitter.on('loadNewTableVoucher', () => {
            this.getPagingVoucher()
        })
    },
    created() {
        this.getPagingVoucher()
        
    },
    watch: {
        /**
         * Hiển thị tài sản của voucher khi selectlist thay đổi
         * NTD 19/10/2022
         */
        selectList() {
            if(this.selectList.length > 0) {
                this.getPropertyInVoucher(this.selectList[0].voucherID)
            } 
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
         numberItemInTable() {
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getPagingVoucher()
            }
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
        pageNumber() {
            this.getPagingVoucher()
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
         keyword(){
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getPagingVoucher()
            }
        }
    }
})
</script>
<style>
@import url(@/css/base/icon.css);
</style>
