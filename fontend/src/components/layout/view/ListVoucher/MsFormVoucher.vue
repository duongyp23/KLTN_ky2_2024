<template>
    <div class="form-view">
        <div class="form">
            <div class="form-header">
                <label style="font-size:20px">{{label}}</label>
                <button class="btn-header" style="border: none; background:transparent" title="Đóng">
                    <div class="icon icon-close" v-on:click="openPopupCloseForm"></div>
                </button>
            </div>
            <div class="form-center voucher">
                <label><b>Thông tin chứng từ</b></label>
                <div class="voucher-info">
                    <div class="row-flex">
                        <div class="form-small-div col-flex">
                            <label class="Form-Label">Mã chứng từ <span style="color:red">*</span></label>
                            <input 
                                type="text" 
                                class="form-input" 
                                ref="inputVoucherCode" 
                                maxlength="20"
                                v-model="voucher.VoucherCode"
                                :class="{'error':checkVoucher('VoucherCode')}"
                            >
                            <label v-show="checkVoucher('VoucherCode')" class="text-error">Mã chứng từ không được để trống</label>
                        </div>
                        <div class="form-small-div col-flex">
                            <label class="Form-Label">Ngày bắt đấu sử dụng <span style="color:red">*</span></label>
                            <div style="align-items: center;display: flex;height: 40px;position: relative;">
                                <input 
                                    type="date" class="form-input " 
                                    style="width: 100% ;padding-right: 12px;" 
                                    v-model="voucher.StartUsingDate"
                                    ref="inputStartUsingDate"
                                    :class="{'error':checkVoucher('StartUsingDate')}"
                                >
                                <div class="icon icon-calendar" style="position:absolute;right:12px;pointer-events: none;cursor: pointer;"></div>
                            </div>
                            <label v-show="checkVoucher('StartUsingDate')" class="text-error">Cần phải nhập thông tin</label>
                        </div>
                        <div class="form-small-div col-flex">
                            <label class="Form-Label">Ngày ghi tăng <span style="color:red">*</span></label>
                            <div style="align-items: center;display: flex;height: 40px;position: relative;">
                                <input 
                                    type="date" class="form-input " 
                                    style="width: 100% ;padding-right: 12px;" 
                                    v-model="voucher.IncrementDate"
                                    ref="inputIncrementDate"
                                    :class="{'error':checkVoucher('IncrementDate')}"
                                >
                                <div class="icon icon-calendar" style="position:absolute;right:12px;pointer-events: none;cursor: pointer;"></div>
                            </div>
                            <label v-show="checkVoucher('IncrementDate')" class="text-error">Cần phải nhập thông tin</label>
                        </div>
                        
                    </div>
                    <div class="row-flex">
                        <div class="form-big-div col-flex" style="width: 100%">
                            <label class="Form-Label">Ghi chú</label>
                            <input 
                                type="text" 
                                class="form-input" 
                                v-model="voucher.Description"
                                maxlength="255"
                            >
                            <label v-show="false" class="text-error">Cần phải nhập thông tin</label>
                        </div>
                    </div>
                </div>
                <label><b>Thông tin chi tiết</b></label>
                <div class="voucher-info" style="padding:0px">
                    <div class="row-flex" style="height: 34px; justify-content: space-between;align-items: center;padding: 0px 12px; padding-top: 12px; width: auto;">
                        <div class="input-tool" style="width: 270px;">
                            <div class="icon icon-search-input" style="margin-left:10px; position: absolute;"></div>
                            <input 
                                type="text" class="input-box" 
                                style="margin-left:39px" 
                                placeholder="Tìm kiếm theo mã, tên tài sản" 
                                @change="setNewDatalist($event.target.value)"
                                @input="checkNullInput($event.target.value)"
                            >
                        </div>
                        
                        <button class="form-btn" style="border: 1px solid #bbb;" @click="openSelectForm = true">Chọn tài sản</button>
                        
                    </div>
                    <table class="table" style="height: 216px; width: 100%;">
                        <thead>
                            <tr>
                                <th  class="col-stt" title="Số thứ tự">
                                    STT
                                </th>
                                <th  class="col-code" >Mã tài sản</th>
                                <th  class="col-name" >Tên tài sản</th>
                                <th  class="col-department" >Bộ phận sử dụng</th>
                                <th  class="col-value">Nguyên giá</th>
                                <th class="col-value" >
                                    Hao mòn năm
                                </th>
                                <th  class="col-value" >Giá trị còn lại</th>
                                <th class="col-fix">Chức năng</th>
                            </tr>
                        </thead>
                        <tbody>
                            <MsRow
                                v-for="(item, index) in datalist"
                                :item = item
                                :key="item.propertyID"
                                :stt = "index+1"
                                :showCheckbox="false"
                                :showInfo="false"
                                :selectList = "[]"
                                :showContext="false"
                                @openFormItem = "showFixBudget = true; itemFix = item"
                                @deleteItem="deleteItemInList(item)"
                            ></MsRow>
                            
                        </tbody>
                        <tfoot style="background-color: #f5f5f5" v-show="datalist.length > 0">
                            <tr>
                                <td style="width: 34%;"></td>
                                <th  class="col-value">{{replaceNumber(totalMarkedPrice())}}</th>
                                <th  class="col-value">{{replaceNumber(totalAttritionValue())}}</th>
                                <th  class="col-value">{{replaceNumber(totalMarkedPrice() - totalAttritionValue())}}</th>
                                <th class="col-fix"></th>
                            </tr>
                        </tfoot>
                    </table>
                    
                </div>
            </div>
            <div class="form-footer">
                <button class="form-btn" v-on:click="openPopupCloseForm">Hủy</button>
                <button class="form-btn" style="background-color:rgba(26, 164, 200);color: #fff;margin-left:10px ;" @click="saveForm">Lưu</button>
            </div>
        </div>
    </div>
    <MsFormFix 
        v-model:showFixBudget="showFixBudget" 
        v-model:item="itemFix"
        @saveBudget = "saveBudget()"
    ></MsFormFix>
    <MsFormSelect 
        v-model:openSelectForm="openSelectForm"
        v-model:tempList="tempList"
        @saveSelectForm = "newDatalist()"
    ></MsFormSelect>
</template>
<script>

import { apiGetPropertyInVoucher,apiGetVoucherByID, apiGetNewVoucherCode, apiInsertVoucher, apiUpdateVoucher } from "@/api/modules";
import { datetimeToDate } from "@/method/methodFormat";
import { replaceNumber } from "@/method/methodFormat";
import { totalMarkedPrice,totalAttritionValue } from "@/method/methodTable";
import  MsRow  from "@/components/base/MsRow.vue";
import MsFormSelect from "./MsFormSelect.vue";
import MsFormFix from "./MsFormFix.vue";
import Resource from "@/resource/MsResource";
export default ({
    data() {
        return {
            check: false,
            itemFix:{},
            showFixBudget:false,
            openSelectForm: false,
            label:"",
            isAdd: false,
            isFix: false,
            voucher: {
                VoucherCode: null,
                IncrementDate: this.datetimeToDate(new Date()),
                StartUsingDate: this.datetimeToDate(new Date()),
                Description: null,
                TotalPrice:0,
            },
            listPropertyInVoucher: [],
            datalist: [],
            tempList:[],
        }
    },
    components: { MsRow, MsFormSelect, MsFormFix },
    created() {
        /**
         * Kiểm tra form là thêm mới hay sửa
         * NTD 19/10/2022
         */
        if(this.$route.params.id) {
            this.getVoucherByID(this.$route.params.id)
            this.getPropertyInVoucher(this.$route.params.id)
            this.label = "Sửa ghi tăng"
            this.isFix = true
            this.isAdd = false
            this.$nextTick(() => this.$refs.inputVoucherCode.focus())
        } else {
            this.getNewVoucherCode()
            this.label = "Thêm mới ghi tăng"
            this.isFix = false
            this.isAdd = true
            this.$nextTick(() => this.$refs.inputVoucherCode.focus())
        }
    },
    methods: {
        /**
         * Khi xóa hết ô input tìm kiểm
         * NTD 21/10/2022
         */
        checkNullInput(value) {
            if(value == null || value =="") {
                this.newDatalist()
            }
        },
        /**
         * Lọc theo input
         * NTD 21/10/2022
         */
        setNewDatalist(value) {
            var temp = []
            this.datalist.forEach(element => {
                if(element.propertyCode.toLowerCase().includes(value.toLowerCase()) || element.propertyName.toLowerCase().includes(value.toLowerCase())){
                    temp.push(element)
                }
            });
            this.datalist = Object.assign([], temp)
        },
        /**
         * Mở popup khi đóng
         * NTD 21/10/2022
         */
        openPopupCloseForm() {
            if(this.isFix == true) {
                this.emitter.emit('openPopupCancelFix')
            } else if(this.isAdd == true) {
                this.emitter.emit('openPopupCancelAdd')
            }
        },
        /**
         * Format ngày tháng
         */
        datetimeToDate,
        /**
         * Format số
         */
        replaceNumber,
        /**
         * Tính tổng nguyên giá
         */
        totalMarkedPrice,
        /**
         * Tỉnh tổng hao mòn
         */
        totalAttritionValue,
        /**
         * Kiểm tra xem có giá trị nào null ko
         * NTD 19/10/2022
         * @param {*} key 
         */
        checkVoucher(key) {
            if(this.check == true && (this.voucher[key] == null || this.voucher[key]=="") ){
                return true
            }
            return false
        },
        /**
         * Lưu nguồn tài sản sau sửa
         * NTD 19/10/2022
         */
        saveBudget(){
            var index = 0
            this.listPropertyInVoucher.forEach(element => {
                if(element.propertyID == this.itemFix.propertyID) {
                    this.listPropertyInVoucher[index] = this.itemFix
                }
                index ++
            });
            index = 0
            this.tempList.forEach(element => {
                if(element.propertyID == this.itemFix.propertyID) {
                    this.tempList[index] = this.itemFix
                }
                index++
            });
            this.newDatalist()
        },
        /**
         * Xóa tái sản khỏi danh sách
         * NTD 19/10/2022
         * @param {*} item 
         */
        deleteItemInList(item) {
            var tmpIndex = 0
            this.listPropertyInVoucher.forEach(element => {
                if(element == item) {
                    this.listPropertyInVoucher.splice(tmpIndex, 1);
                }
                tmpIndex++
            });
            tmpIndex = 0
            this.tempList.forEach(element => {
                if(element == item) {
                    this.tempList.splice(tmpIndex, 1);
                }
                tmpIndex++
            });
            this.newDatalist()
            this.emitter.emit('checkRow')
            
        },
        /**
         * Format ngày tháng
         * NTD 19/10/2022
         */
        formatDateVoucher(){
            this.voucher.IncrementDate = datetimeToDate(this.voucher.IncrementDate)
            this.voucher.StartUsingDate = datetimeToDate(this.voucher.StartUsingDate)
        },
        /**
         * Đóng form
         * NTD 19/10/2022
         */
        closeForm() {
            this.$router.push("/taisan/ghitang")

        },
        /**
         * Sự kiện khi bấm lưu
         * NTD 19/10/2022
         */
        saveForm() {
            this.check=false
            if(this.voucher.VoucherCode=="" || this.voucher.VoucherCode == null){
                this.$nextTick(() => this.$refs.inputVoucherCode.focus())
                this.check = true
            }
            if(this.voucher.StartUsingDate == null && this.check == false ){
                this.$nextTick(() => this.$refs.inputStartUsingDate.focus())
                this.check = true
            }
            if( this.voucher.IncrementDate == null && this.check == false){
                this.$nextTick(() => this.$refs.inputIncrementDate.focus())
                this.check = true
            }
            this.newDatalist()
            if( this.datalist.length < 1 && this.check == false){
                this.emitter.emit('openPopupNotice', "Chọn ít nhất 1 tài sản")
                this.check = true
            }
            if(this.check == false) {
                this.voucher.TotalPrice = 0
                for (let index = 0; index < this.datalist.length; index++) {
                    this.datalist[index].isActive = 1
                    this.voucher.TotalPrice += this.datalist[index].markedPrice
                }
                if(this.isAdd == true) {
                    this.insertVoucher()
                } else {
                    this.updateVoucher()
                }
            }
        },
        /**
         * cập nhật lại danh sách tài sản của voucher
         * NTD 19/10/2022
         */
        newDatalist() {
            this.datalist = []
            this.listPropertyInVoucher.forEach(element => {
                this.datalist.push(element)
            });
            this.tempList.forEach(element => {
                this.datalist.push(element)
            });
        },
        /**
         * Gọi api lấy danh sách tài sản của voucher khi sửa
         * NTD 19/10/2022
         * @param {*} id 
         */
        async getPropertyInVoucher(id) {
            this.datalist = []
            await apiGetPropertyInVoucher(id)
            .then(response => {
                this.listPropertyInVoucher = response.data.data
                this.newDatalist()
            })
            .catch(() => {
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorGetPropertyInVoucher)
            })
        },
        /**
         * Lấy thông tin voucher khi sửa
         * NTD 19/10/2022
         * @param {} id 
         */
        async getVoucherByID(id) {
            await apiGetVoucherByID(id)
            .then(response => {
                this.voucher = response.data
                this.formatDateVoucher()
            })
            .catch(() => {
                this.emitter.emit('openPopupNotice',Resource.PopupNotice.ErrorGetVoucher)
            }) 
        },
        /**
         * lấy mã voucher mới
         * NTD 19/10/2022
         */
        async getNewVoucherCode() {
            await apiGetNewVoucherCode() 
            .then(response => {
                this.voucher.VoucherCode = response.data
            })
            .catch(() => {
                this.emitter.emit("openPopupNotice", Resource.PopupNotice.ErrorGetNewCode)
            })
        },
        /**
         * Thêm mới 1 voucher
         * NTD 19/10/2022
         */
        async insertVoucher() {
            await apiInsertVoucher(this.voucher, this.datalist)
            .then(response => {
                console.log(response.data);
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageAdd)
                this.$router.push("/taisan/ghitang")
                this.emitter.emit('loadNewTableVoucher')
            })
            .catch( e => {
                if(e.response.data == Resource.ErrorCode.DuplicateCode) {
                    this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorDuplicate)
                } else {
                    this.emitter.emit('openToastMessageError', Resource.ErrorMessage.ErrorMessageAdd)
                }
            })
        },
        /**
         * Cập nhật voucher
         * NTD 19/10/2022
         */
        async updateVoucher() {
            await apiUpdateVoucher(this.voucher, this.datalist)
            .then(() => {
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageFix)
                this.$router.replace(this.$route.path)
                this.$router.push("/taisan/ghitang")
                this.emitter.emit('loadNewTableVoucher')
            })
            .catch( e => {
                if(e.response.data == Resource.ErrorCode.DuplicateCode) {
                    this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorDuplicate)
                } else {
                    this.emitter.emit('openToastMessageError', Resource.ErrorMessage.ErrorMessageFix)
                }
            })
        }


    },
    mounted() {
        /**
         * Đóng form sau khi xác nhận
         * NTD 8/8/2022
         */
         this.emitter.on('closeFormFinish',() => {
            this.closeForm()
        })
        /**
         * Nhận lệnh save form
         * NTD 14/8/2022
         */
         this.emitter.on('saveForm', () => {
            this.saveForm()
        })
    }
})
</script>
<style >
@import url(@/css/layout/form.css);
</style>
