<template>
    <div class="form-view" v-if="openSelectForm">
        <div class="form">
            <div class="form-header">
                <label style="font-size:20px">Chọn tài sản ghi tăng</label>
                <button class="btn-header" style="border: none; background:transparent" title="Đóng">
                    <div class="icon icon-close" v-on:click="closeForm"></div>
                </button>
            </div>
            <hr style="width: 100%; color: #bbb;">
            <div class="form-center voucher" style="padding:0px">
                <div class="voucher-info" style="padding:0px">
                    <div class="row-flex" style="height: 34px; justify-content: space-between;align-items: center;padding: 0px 12px; padding-top: 12px; width: auto;">
                        <div class="input-tool" style="width: 270px;">
                            <div class="icon icon-search-input" style="margin-left:10px; position: absolute;"></div>
                            <input 
                                type="text" class="input-box" 
                                style="margin-left:39px" 
                                placeholder="Tìm kiếm theo mã, tên tài sản" 
                                @change="keyword = $event.target.value"
                                
                            >
                        </div>
                    </div>
                    <table class="table" style="height: 510px; width: 100%;box-shadow: none;">
                        <thead>
                            <tr>
                                <th class="col-check" @click="selectAllRow"><div class="icon icon-box" :class="{'selectAllRow': isSelectAllRow}"></div></th>
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
                                v-for="(item, index) in datalist"
                                :item = item
                                :key="item.propertyID"
                                :stt = "index+1"
                                :showInfo="false"
                                :selectList = "selectList"
                                :showContext="false"
                                :showFix="false"
                                @removeSelectAll="isSelectAllRow = false"
                                @removeItemSelectList = "removeItemSelectList(item, index)"
                                @addItemSelectList = "addItemSelectList(item, index)"
                                @selectOnlyRow="selectOnlyRow(item, index)"
                                @onShiftClick="onShiftClick(index)"
                            
                            ></MsRow>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td style="width: 43%; font-size: 11px;">
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
                                <th  class="col-value"></th>
                                <th  class="col-value"></th>
                                <th  class="col-value"></th>
                            </tr>
                        </tfoot>
                    </table>
                    
                </div>
            </div>
            <div class="form-footer">
                <button class="form-btn" v-on:click="closeForm">Hủy</button>
                <button class="form-btn" style="background-color:rgba(26, 164, 200);color: #fff;margin-left:10px ;" @click="saveForm">Lưu</button>
            </div>
        </div>
    </div>
</template>
<script>
import {apiGetPropertyNotActive} from "@/api/modules"
import MsRow from "@/components/base/MsRow.vue";
import VPagination from "@hennge/vue3-pagination";
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import { replaceNumber } from "@/method/methodFormat";
import { totalMarkedPrice, totalAttritionValue,closeSelect,totalPage,changeNumberItemInTable,
    selectAllRow,removeItemSelectList,addItemSelectList,selectOnlyRow,onShiftClick,checkItemInList } from "@/method/methodTable";
import Resource from "@/resource/MsResource";
export default ({
    data() {
        return {
            isSelectAllRow: false,
            datalist : [],
            selectList: [],
            keyword: null,
            totalItem: 0,
            numberItemInTable: 20,
            pageNumber: 1,
            showSelect: false,
        }
    },
    props: {
        openSelectForm: {type:Boolean},
        tempList: {type:Array}
    },
    components: {MsRow,VPagination},
    methods: {
        /**
         * Format lại số
         */
        replaceNumber,
        /**
         * Tính tổng giá trị hao mòn
         */
        totalAttritionValue,
        /**
         * Tính tổng nguyên giá
         */
        totalMarkedPrice,
        /**
         * Đóng chọn paging số hiển thị
         */
        closeSelect,
        /**
         * Tính tổng số trang
         */
        totalPage,
        /**
         * Thay đổi số lượng hiển thị 1 trang
         */
        changeNumberItemInTable,
        /**
         * Chọn tất cả các item
         */
        selectAllRow,
        /**
         * Xóa item khỏi list đã đã chọn
         */
        removeItemSelectList,
        /** 
         * Thêm item vào list chọn
        */
        addItemSelectList,
        /**
         * Chỉ chọn 1 dòng
         */
        selectOnlyRow,
        /**
         * Chọn banwgf shift
         */
        onShiftClick,
        /**
         * Kiểm tra item trong list ko
         */
        checkItemInList,
        /**
         * Đóng form
         * NTD 19/10/2022
         */
        closeForm() {
            this.selectList = []
            this.$emit('update:openSelectForm', false)
        },
        /**
         * Lưu form và các tài sản đã chọn
         * NTD 19/10/2022
         */
        saveForm() {
            var tmp = Object.assign([] ,this.selectList)
            this.$emit('update:tempList', tmp)
            this.$emit('saveSelectForm')
            this.$emit('update:openSelectForm', false)
        },
        /**
         * Gọi api lấy danh sách tài sản chưa ghi tăng
         * NTD 19/10/2022
         */
        async getListProperty() {
            this.datalist = []
            var listPropertyID = []
            this.tempList.forEach(element => {
                listPropertyID.push(element.propertyID)
            });
            await apiGetPropertyNotActive(this.keyword, this.numberItemInTable, this.pageNumber,listPropertyID) 
            .then(response => {
                this.datalist = response.data.data
                this.totalItem = response.data.totalCount
            })
            .catch(() => {
                this.emitter.emit('openPopupNotice',Resource.PopupNotice.ErrorGetPaging)
            })
        }
    },
    created() {
        this.getListProperty()
    }, 
    watch: {
        openSelectForm() {
            this.getListProperty()
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
         numberItemInTable() {
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getListProperty()
            }
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
         pageNumber() {
            this.getListProperty()
        },
        /***
         * Kiểm tra thay đổi giá trị để phân trang
         * NTD 5/9/2022
         */
         keyword(){
            if(this.pageNumber != 1 ){
                this.pageNumber = 1
            } else {
                this.getListProperty()
            }
        }
    }
})
</script>
<style >
@import url(@/css/layout/form.css);
</style>
