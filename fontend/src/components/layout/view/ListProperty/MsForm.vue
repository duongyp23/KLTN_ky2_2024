<template>
    <div class="form-view" v-show="isOpen" @keyup.esc="closeForm()">
        <div class="form">
            <div class="form-header">
                <div>{{label}}</div>
                <button class="btn-header" style="border: none; background:transparent" title="Đóng">
                    <div class="icon icon-close" v-on:click="closeForm"></div>
                </button>
            </div>
            <div class="form-center">
                <div class="row-flex">
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Mã tài sản <span style="color:red">*</span></label>
                        <input 
                            type="text" 
                            class="form-input" 
                            ref="inputPropertyCode" 
                            v-model="item.PropertyCode" 
                            :class="{'error':checkNull('PropertyCode')}" 
                            maxlength="20"
                        >
                        <label v-show="checkNull('PropertyCode')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    
                    <div class="form-big-div col-flex">
                        <label class="Form-Label">Tên tài sản <span style="color:red">*</span></label>
                        <input 
                            type="text" class="form-input" 
                            v-model="item.PropertyName" 
                            :class="{'error':checkNull('PropertyName')}" 
                            maxlength="255" 
                            placeholder="Nhập tên tài sản"
                            ref="inputPropertyName"
                        >
                        <label v-show="checkNull('PropertyName')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                </div>
                <MsFormSelect 
                    label1 ="Mã bộ phận sử dụng"
                    label2 ="Tên bộ phận sử dụng"
                    v-model:inputCode="item.DepartmentCode"
                    v-model:inputName="item.DepartmentName"
                    v-model:selectValue="department"
                    :check="checkNull('DepartmentCode')"
                    :items = "departmentList"
                    :placeholder="'Chọn mã bộ phận sử dụng'"
                    ref = "inputDepartmentCode"
                ></MsFormSelect>
                <MsFormSelect 
                    label1 ="Mã loại tài sản"
                    label2 ="Tên loại tài sản"
                    v-model:inputCode="item.PropertyTypeCode"
                    v-model:inputName="item.PropertyTypeName"
                    v-model:selectValue="propertyType"
                    :check="checkNull('PropertyTypeCode')"
                    :items="typeList"
                    :placeholder="'Chọn mã loại tài sản'"
                    ref = "inputPropertyTypeCode"
                ></MsFormSelect>
                <div class="row-flex">
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Số lượng <span style="color:red">*</span></label>
                        <div style="align-items: center;display: flex;height: 40px;position: relative;">
                            <input 
                                type="text" 
                                class="form-input input-number" 
                                style="width:100%;padding-right: 30px;" 
                                
                                :class="{'error':checkNull('Quantity')}" 
                                ref="inputQuantity"
                                @input="inputQuantity()"
                                value="0"
                            >
                            <div class="input-btn-updown">
                                <div class="icon icon-mini-up" @click="upQuantity()"  style="cursor:pointer"></div>
                                <div class="icon icon-mini-down" @click="downQuantity()" style="cursor:pointer"></div>
                            </div>
                        </div>
                        <label v-show="checkNull('Quantity')" class="text-error">Cần phải nhập thông tin </label>
                    </div>
                    
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Nguyên giá <span style="color:red">*</span></label>
                        <input type="text" class="form-input input-number" 
                            :class="{'error':checkNull('MarkedPrice')}"
                            ref="inputMarkedPrice"
                            @input="inputMarkedPrice()"
                            value="0"
                            :disabled="item.IsActive == 1"
                        >
                        <label v-show="checkNull('MarkedPrice')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Số năm sử dụng <span style="color:red">*</span></label>
                        <input type="text" 
                            class="form-input input-number" 
                            
                            :class="{'error':checkNull('UsedYear')}"
                            :maxlength="3"
                            ref="inputUsedYear"
                            @input="inputUsedYear()"
                            value="0"
                            
                        >
                        <label v-show="checkNull('UsedYear')" class="text-error">Cần phải nhập thông tin </label>
                    </div>
                </div>
                <div class="row-flex">
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Tỉ lệ hao mòn (%) <span style="color:red">*</span></label>
                        <div style="align-items: center;display: flex;height: 40px;position: relative;">
                            <input 
                                type="text" 
                                class="form-input input-number" 
                                style="width:100%;padding-right: 30px;" 
                                :class="{'error':checkNull('AttritionRate')}"
                                ref="inputAttritionRate"
                                @input="inputAttritionRate()"
                                value="0"
                                
                            >
                            <div class="input-btn-updown">
                                <div class="icon icon-mini-up" @click="upAttritionRate()" style="cursor:pointer"></div>
                                <div class="icon icon-mini-down" @click="downAttritionRate()" style="cursor:pointer"></div>
                            </div>
                        </div>
                        <label v-show="checkNull('AttritionRate')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Giá trị hao mòn năm <span style="color:red">*</span></label>
                        <input 
                            type="text" 
                            class="form-input input-number" 
                            :class="{'error': checkAttritionValuePerYear()}"
                            ref="inputAttritionValuePerYear"
                            @input="inputAttritionValuePerYear()"
                            value="0"
                            :disabled="item.IsActive == 1"
                        >
                        <label v-show="checkAttritionValuePerYear()" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Năm theo dõi </label>
                        <input type="number" 
                            class="form-input input-number" 
                            style="background-color: rgb(245, 245, 245);" 
                            :value="item.TrackingYear"
                            disabled
                        >
                        
                    </div>
                </div>
                <div class="row-flex">
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Ngày mua <span style="color:red">*</span></label>
                        <div style="align-items: center;display: flex;height: 40px;position: relative;">
                            <input 
                                type="date" class="form-input " 
                                style="width: 100%; padding-right: 12px ;" 
                                v-model="item.PurchasingDate" 
                                :class="{'error':checkNull('PurchasingDate')}"
                            >
                            <div class="icon icon-calendar" style="position:absolute;right:12px;pointer-events: none;cursor: pointer;"></div>
                        </div>
                        <label v-show="checkNull('PurchasingDate')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Ngày bắt đầu sử dụng <span style="color:red">*</span></label>
                        <div style="align-items: center;display: flex;height: 40px;position: relative;">
                            
                            <input 
                                type="date" class="form-input " 
                                style="width: 100% ;padding-right: 12px;" 
                                v-model="item.StartUsingDate" 
                                :class="{'error':checkNull('StartUsingDate')}"
                                @change="calculateAttritionValue()"
                            >
                            <div class="icon icon-calendar" style="position:absolute;right:12px;pointer-events: none;cursor: pointer;"></div>
                        </div>
                        <label v-show="checkNull('StartUsingDate')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    <div class="form-small-div col-flex"></div>
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

import MsFormSelect from "@/components/base/MsFormSelect.vue";
import AutoNumeric from 'autonumeric';
import Resource from "@/resource/MsResource";
import { apiAddNewProperty, apiGetDepartment, apiGetNewCode, apiGetPropertyByID, apiGetPropertyType, apiUpdateProperty, apiGetBudget } from "@/api/modules";

/**
 * Khởi tạo 1 Item với giá trị ban đầu là null
 * NTD 22/8/2022
 */
export default {
    
    data() {
        return {
            check: false, // Chế độ kiểm tra
            isAdd: false, // form thêm mới
            isFix: false, // form sửa
            AttritionValuePerYear: null, // input giá trị hao mòn
            Quantity: null, // input số lượng
            MarkedPrice: null, // input nguyên giá
            UsedYear: null , // input số năm sử dụng
            AttritionRate: null, // input tỉ lệ hao mòn
            departmentList: [], // danh sách phòng ban
            typeList: [], // danh sách loại tài sản
            department: {}, // phòng ban
            propertyType: {}, // loại tài sản
            // Tài sản
            item: {
                PropertyID: null,
                PropertyCode: null,
                PropertyName: null,
                PropertyTypeID:null,
                PropertyTypeCode: null,
                PropertyTypeName: null,
                DepartmentID:null,
                DepartmentCode: null,
                DepartmentName: null,
                Quantity:0,
                MarkedPrice:0,
                UsedYear: 0,
                AttritionRate:0,
                AttritionValue: 0,
                TrackingYear: this.getYear(),
                IsActive: Resource.IsActive.NotActive,
                Budget: null,
                PurchasingDate: {type:Date},
                StartUsingDate: {type:Date},
            },
            //Danh sách các input của property
            listInput: ['PropertyCode','PropertyName','DepartmentCode','PropertyTypeCode','Quantity',
             'MarkedPrice', 'UsedYear', 'AttritionRate', 'PurchasingDate', 'StartUsingDate'],
            isOpen: false, // hiển thị
            label: '', // tiêu đề
            tempItemValue: {},
            Budget:[]
        }
        
    },
    methods: {
        /**
         * Ngày hiện tại
         * 
         */
        getToday (){
            return new Date()
        },
        /***
         * Tăng số lượng 
         * NTD 12/9/2022
         */
        upQuantity() {
            this.item.Quantity++
            this.Quantity.set(this.item.Quantity)
        },
        /***
         * Giảm số lượng 
         * NTD 12/9/2022nList
         */
        downQuantity() {
            if(this.item.Quantity > 0){
                this.item.Quantity--
                this.Quantity.set(this.item.Quantity)
            }
        },
        /***
         * Tăng tỉ lệ hao mòn
         * NTD 12/9/2022
         */
         upAttritionRate() {
            this.item.AttritionRate+=0.0001
            this.AttritionRate.set(this.item.AttritionRate*100)
        },
        /***
         * Giảm số lượng 
         * NTD 12/9/2022
         */
        downAttritionRate() {
            if(this.item.AttritionRate > 0){
                this.item.AttritionRate -= 0.0001
                this.AttritionRate.set(this.item.AttritionRate*100)
            }
        },
        /***
         * Khởi tạo 1 tài sản mới
         * NTD 5/9/2022
         */
         defaultItem() {
            return {
                PropertyCode: null,
                PropertyName: null,
                PropertyTypeCode: null,
                PropertyTypeName: null,
                DepartmentID:null,
                DepartmentCode: null,
                DepartmentName: null,
                Quantity: 0,
                MarkedPrice: 0,
                UsedYear: 0,
                AttritionRate: 0,
                AttritionValue: 0,
                TrackingYear: this.getYear(),
                PurchasingDate: {default: null, type:Date},
                StartUsingDate: {default: null, type:Date},
                IsActive: Resource.IsActive.NotActive,
                Budget: null,
                CreatedBy: "admin",
                CreatedDate: new Date(),
                ModifiedBy: "admin",
                ModifiedDate: new Date(),
            };
        },
        /**
         * Tính giá trị hao mòn lũy kế theo năm
         * NTD 24/8/2022
         */
        calculateAttritionValue() {
            const today = new Date()
            const yearStartUsing = new Date(this.item.StartUsingDate).getFullYear()
            const numberUsedYear = today.getFullYear() - yearStartUsing
            if(numberUsedYear > 0) {
                var temp = this.item.MarkedPrice*numberUsedYear*this.item.AttritionRate
                if(temp > this.item.MarkedPrice) {
                this.item.AttritionValue = this.item.MarkedPrice
                } else {
                    this.item.AttritionValue = temp
                }

            }
        },
        /**
         * chuyển dữ liệu về dạng số
         * NTD 25/8/2022
         */
        // textToNumber(key) {
        //     const rawValueText = this.key.rawValue;
        //     return Number(rawValueText);
        // },
        /**
         * Lưu dữ liệu dưới dạng số khi nhập vào item
         * NTD 24/8/2022
         */
        inputQuantity() {
            const rawValueText = this.Quantity.rawValue;
            const valueNumber = Number(rawValueText);
            this.item.Quantity  = valueNumber;
        
        },
        /**
         * Lưu dữ liệu dưới dạng số khi nhập vào item, và tính các giá trị thay đổi
         * NTD 24/8/2022
         */
        inputMarkedPrice() {
            const rawValueText = this.MarkedPrice.rawValue;
            const valueNumber = Number(rawValueText);
            this.item.MarkedPrice  = valueNumber;
            this.AttritionValuePerYear.set(this.item.MarkedPrice*this.item.AttritionRate)
            this.calculateAttritionValue()
            
        },
        /**
         * Lưu dữ liệu dưới dạng số khi nhập vào item
         * NTD 24/8/2022
         */
        inputUsedYear() {
            const rawValueText = this.UsedYear.rawValue;
            const valueNumber = Number(rawValueText);
            this.item.UsedYear  = valueNumber;
        },
        /**
         * Lưu dữ liệu dưới dạng số khi nhập vào item, và tính các giá trị thay đổi
         * NTD 24/8/2022
         */
        inputAttritionRate() {
            const rawValueText = this.AttritionRate.rawValue;
            const valueNumber = Number(rawValueText);
            this.item.AttritionRate  = valueNumber/100;
            this.AttritionValuePerYear.set(this.item.MarkedPrice*this.item.AttritionRate)
            this.calculateAttritionValue()
        },
        /**
         * Lưu dữ liệu dưới dạng số khi nhập vào item và tính các giá trị thay đổi
         * NTD 24/8/2022
         */
        inputAttritionValuePerYear() {
            const rawValueText = this.AttritionValuePerYear.rawValue;
            const valueNumber = Number(rawValueText);
            if(valueNumber > this.item.MarkedPrice){
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorAttritionValue)
                this.AttritionValuePerYear.set(0)
            } else {
                this.AttritionRate.set(valueNumber*100/this.item.MarkedPrice)
                this.calculateAttritionValue()
            }
            
        },
        /**
         * Kiểm tra giá trị hao mòn theo năm
         * NTD 24/8/2022
         */
        checkAttritionValuePerYear() {
            
            if(this.check == true) {
                const rawValueText = this.AttritionValuePerYear.rawValue;
                const valueNumber = Number(rawValueText);
                if(valueNumber == 0) {
                    return true
                }
            }
            return false
        },
        /**
         * Kiểm tra giá trị háo mòn năm với nguyên giá
         * NTD 12/9/2022
         */
        checkAttritionValueAndMarkedPrice() {
            const rawValueText = this.AttritionValuePerYear.rawValue;
            const valueNumber = Number(rawValueText);
            if(valueNumber > this.item.MarkedPrice){
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorAttritionValue)
                this.check = true
                this.AttritionValuePerYear.set(0)
            }
        },
        /***
         * Kiểm tra tỉ lệ hao mòn và số năm sử dụng
         * NTD 12/9/2022
         */
        checkAttritionRateAndUsedYear() {
            if(this.item.AttritionRate * (this.item.UsedYear-1) >= 1) {
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorAttritionRate)
                this.item.AttritionRate = 1/this.item.UsedYear 
                this.AttritionRate.set(this.item.AttritionRate*100)
                this.AttritionValuePerYear.set(this.item.MarkedPrice*this.item.AttritionRate)
                this.calculateAttritionValue()
                this.check = true
            }
        },
        /**
         * Kiểm tra ngày sử dụng và ngày mua
         * NTD 20/9/2022
         */
        checkStartUsingDate() {
            var today = this.datetimeToDate(new Date())
            if(this.item.StartUsingDate > today || this.item.PurchasingDate > today) {
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorDate)
                //this.setDateToday()
                this.item.StartUsingDate = this.datetimeToDate(new Date())
                this.item.PurchasingDate = this.datetimeToDate(new Date())
                this.check = true
            } else if(this.item.StartUsingDate < this.item.PurchasingDate) {
                this.item.StartUsingDate = this.item.PurchasingDate 
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorStartUsingDate)
                this.check = true
            }
        },
        /***
         * Kiểm tra các giá trị có null ko sau khi bấm save để hiện lỗi
         * NTD 18/8/2022
         */
        checkNull(key) {
            if(this.check == true && (this.item[key] == null || this.item[key]==0||this.item[key] =='')) {
                return true
            }
            return false
        },
        
        /**
         * Mở pop up Đóng form hủy tiến trình đang hoạt động
         * NTD 8/8/2022
         */
        closeForm () {
            if(this.isFix == true) {
                this.emitter.emit('openPopupCancelFix')
            } else if(this.isAdd == true) {
                this.emitter.emit('openPopupCancelAdd')
            }
            
            // this.isOpen = false
        },
        /**
         *  Thay đổi tiêu đề form 
         *  NTD 8/8/2022
         */
        setLabel(newLabel) {
            this.label = newLabel
        },
        /**
         * Truyền giá trị các trường trong form khi chọn sửa từ table
         *  NTD 11/8/2022
         */
        setValue(item) {
            this.item = Object.assign({}, item)
            this.item.StartUsingDate = this.datetimeToDate(item.StartUsingDate)
            this.item.PurchasingDate = this.datetimeToDate(item.PurchasingDate)
            
            this.MarkedPrice.set(this.item.MarkedPrice)
            this.Quantity.set(this.item.Quantity)
            if(this.item.UsedYear) {
                this.UsedYear.set(this.item.UsedYear)
            } else {
                this.UsedYear.set(0)
            }
            if(this.item.AttritionRate) {
                this.AttritionRate.set(this.item.AttritionRate*100)
                this.AttritionValuePerYear.set(this.item.MarkedPrice*this.item.AttritionRate)
            } else {
                this.AttritionRate.set(0)
                this.AttritionValuePerYear.set(0)
            }
            
        },
        /**
         * Lưu form, kiểm tra các trường đã được nhập dữ liệu hay chưa khi click
         */
        saveForm() {
            
            this.check = false
            this.listInput.forEach(key => {
                if(this.check == false) {
                    if(this.item[key] == '' || this.item[key] == null || this.item[key]== 0) {
                        this.check = true
                        if (key == 'DepartmentCode') {
                            this.$refs.inputDepartmentCode.setFocusInput()
                        } else if (key == 'PropertyTypeCode') {
                            this.$refs.inputPropertyTypeCode.setFocusInput()
                        } else {
                            var inputName = "input"+key.toString()
                            this.$nextTick(() => this.$refs[inputName].focus())
                        }
                    }
                }
            });
            if(this.check == false ){
                this.checkAttritionRateAndUsedYear()
            }
            if(this.check == false ){
                this.checkAttritionValueAndMarkedPrice()
            }
            if(this.check == false ) {
                this.checkStartUsingDate()
            }
            if(this.check ==  false) {
                
                if(this.isAdd == true) {
                    this.setBudget()
                    this.addNewProperty()
                } else if(this.isFix == true){
                    if(this.item.IsActive == Resource.IsActive.NotActive){
                        this.setBudget()
                    }
                    this.updateProperty()
                }
            }
        },
        /**
         * sửa dữ liệu nguồn tài sản 
         * NTD 21/10/2022
         */
        setBudget() {
            this.item.Budget = JSON.stringify(
                [{
                    BudgetID: this.Budget[0].id,
                    BudgetName: this.Budget[0].name,
                    Mount: this.item.MarkedPrice
                }]
            )
        },
        /**
         * Thay đổi kiểu nhập sạng số banwgf AutoNumeric
         * NTD 24/8/2022
         */
        callAutoNumeric() {
            this.Quantity = new AutoNumeric(this.$refs.inputQuantity, {
                decimalCharacter: Resource.DecimalCharacter,
                digitGroupSeparator : Resource.DigitGroupSeparator,
                decimalPlaces:'0',
                minimumValue:'0'
            })
            this.MarkedPrice = new AutoNumeric(this.$refs.inputMarkedPrice, {
                decimalCharacter: Resource.DecimalCharacter,
                digitGroupSeparator : Resource.DigitGroupSeparator,
                decimalPlaces:'2',
                minimumValue:'0',
                maximumValue:'999999999999999999.99',
                allowDecimalPadding: false
            })
            this.UsedYear = new AutoNumeric(this.$refs.inputUsedYear, {
                decimalPlaces:'0',
                maximumValue:'100'
            })
            this.AttritionRate =new AutoNumeric(this.$refs.inputAttritionRate, {
                decimalCharacter: Resource.DecimalCharacter,
                digitGroupSeparator : Resource.DigitGroupSeparator,
                decimalPlaces:'2',
                maximumValue:'100',
                minimumValue:'0',
            })
            this.AttritionValuePerYear=new AutoNumeric(this.$refs.inputAttritionValuePerYear, {
                decimalCharacter: Resource.DecimalCharacter,
                digitGroupSeparator : Resource.DigitGroupSeparator,
                decimalPlaces:'2',
                minimumValue:'0',
                maximumValue:'999999999999999999.99',
                allowDecimalPadding: false
            })
        },
        /**
         * Đặt ngày mua và ngày sử dụng thành ngày hiện tại 
         * NTD 24/8/2022
         */
        setDateToday() {
            const current = new Date()
            var today
            if(current.getMonth()+1< 10) {
                if(current.getDate() < 10) {
                    today = `${current.getFullYear()}-0${current.getMonth()+1}-0${current.getDate()}`
                } else {
                    today = `${current.getFullYear()}-0${current.getMonth()+1}-${current.getDate()}`
                }
            } else {
                if(current.getDate() < 10) {
                    today = `${current.getFullYear()}-${current.getMonth()+1}-0${current.getDate()}`
                } else {
                    today = `${current.getFullYear()}-${current.getMonth()+1}-${current.getDate()}`
                }
            }
            this.item.StartUsingDate = today
            this.item.PurchasingDate = today
        },
        /**
         * Lấy năm hiện tại
         * NTD 5/9/2022
         */
        getYear() {
            const current = new Date()
            return current.getFullYear()
        },
        /**
         * lấy mã tài sản mới khi thêm mới tài sản
         * NTD 25/8/2022
         */
        async getNewCode() {
            await  apiGetNewCode()
            .then(response => {
                this.item.PropertyCode = response.data
            })
            .catch(() => {
                this.emitter.emit("openPopupNotice", Resource.PopupNotice.ErrorGetNewCode)
            })
        },
        /**
         * Đổi kiểu dữ liệu từ datetime về date
         *  NTD 25/8/2022
         */
        datetimeToDate(date) {
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
        },
        /**
         * Lấy thông tin của tài sản khi sửa
         * NTD 25/8/2022
         */
        async getPropertyById(id){
            await  apiGetPropertyByID(id)
            .then(response => {
                this.setValue(response.data)
            })
            .catch(() => {
                this.emitter.emit('openPopupNotice', Resource.ErrorMessage.ErrorMessageGetProperty)
            })
            
        },
        /**
         * Gọi API thêm mới tài sản
         * NYD 5/9/2022
         */
        async addNewProperty() {
            await  apiAddNewProperty(this.item)
            .then( () => {
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageAdd)
                this.emitter.emit('loadNewTable')
                this.isOpen = false
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
         * gọi API Sửa tài sản
         * NTD 5/9/2022
         */
        async updateProperty() {
            await  apiUpdateProperty(this.item.PropertyID, this.item)
            .then( () => {
                this.isOpen = false
                this.emitter.emit('openToastMessage', Resource.SuccessMessage.SuccessMessageFix)
                this.emitter.emit('loadNewTable')
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
    components: {MsFormSelect },
    mounted() {
        this.callAutoNumeric()
        /**
         * Mở form thêm tài sản từ Tool
         * NTD 8/8/2022
         */
        this.emitter.on('openForm',async () => {
            
            // this.item = defaultItem()
            this.setValue(this.defaultItem())
            //this.setDateToday()
            this.item.StartUsingDate = this.datetimeToDate(new Date())
            this.item.PurchasingDate = this.datetimeToDate(new Date())
            await this.getNewCode()
            this.isAdd = true
            this.isFix = false
            this.isOpen= true
            this.check = false
            this.setLabel('Thêm tài sản')
            this.$nextTick(() => this.$refs.inputPropertyCode.focus())
            
        })
        /**
         * Đóng form sau khi xác nhận
         * NTD 8/8/2022
         */
        this.emitter.on('closeFormFinish',() => {
            this.isOpen= false
            this.emitter.emit('focusTable')
        })
        /**
         * Mở form sửa tài sản từ Row trong table
         * NTD 8/8/2022
         */
        this.emitter.on('openFormFix',async (item) => {
            this.isFix = true
            this.isAdd = false
            this.check = false
            this.setLabel('Sửa tài sản')
            this.getPropertyById(item.propertyID)
            this.tempItemValue = Object.assign({},this.item)
            this.isOpen = true
            this.$nextTick(() => this.$refs.inputPropertyCode.focus())
        })
        
        /**
         * Nhận lệnh save form
         * NTD 14/8/2022
         */
        this.emitter.on('saveForm', () => {
            this.saveForm()
        })
        /**
         * Nhận yêu cầu tạo form thêm tài sản copy
         * NTD 14/8/2022
         */
        this.emitter.on('openFormCopy', async (item) => {
            this.isAdd = true
            this.isFix = false
            this.check = false
            this.setLabel('Thêm tài sản')
            await this.getPropertyById(item.propertyID)
            await this.getNewCode()
            this.item.IsActive = Resource.IsActive.NotActive
            this.isOpen= true
            this.$nextTick(() => this.$refs.inputPropertyCode.focus())
        })
    },
    watch: {
        /***
         * Kiểm tra thay đổi giá trị của loại tài sản và đặt lại các giá trị khác
         * NTD 5/9/2022
         */
        propertyType() {
            this.item.PropertyTypeID =this.propertyType.id 
            this.item.PropertyTypeName = this.propertyType.name
            if(this.propertyType.attritionRateDefault) {
                this.item.AttritionRate = this.propertyType.attritionRateDefault
                this.AttritionRate.set(this.item.AttritionRate*100)
                this.AttritionValuePerYear.set(this.item.MarkedPrice*this.item.AttritionRate)
                this.calculateAttritionValue()
            } else {
                this.item.AttritionRate = 0
                this.AttritionRate.set(0)
                this.AttritionValuePerYear.set(0)
                this.item.AttritionValue = 0
            }
            if(this.propertyType.usedYearDefault) {
                this.item.UsedYear = this.propertyType.usedYearDefault
                this.UsedYear.set(this.item.UsedYear)
            } else {
                this.item.UsedYear =0
                this.UsedYear.set(0)
            }
        },
        /***
         * Kiểm tra thay đổi giá trị của phòng ban và đặt lại các giá trị khác
         * NTD 5/9/2022
         */
        department() {
            if(this.department) {
                this.item.DepartmentID = this.department.id
                this.item.DepartmentName = this.department.name
            } else {
                this.item.DepartmentID = null
                this.item.DepartmentName = null
            }
        }
    },
    async created() {
        /**
         * Lấy danh sách nguồn tài sản
         * NTD 19/10/2022
         */
        await apiGetBudget()
        .then(response => {
            var listBudget = response.data
            this.Budget = []
            listBudget.forEach(element => {
                this.Budget.push({name: element.BudgetName, id: element.BudgetID})
            })
        }),
        /**
         * Lấy danh sách phòng ban
         * NTD 5/9/2022
         */
        await  apiGetDepartment()
        .then(response => {
            var listDepartment = response.data
            this.departmentList = []
            listDepartment.forEach(element => {
                this.departmentList.push({code: element.DepartmentCode,name: element.DepartmentName, id: element.DepartmentID})
            })
        
        }),
        /**
         * Lấy danh sách loại tài sản
         * NTD 5/9/2022
         */
        await  apiGetPropertyType()
        .then(response => {
            var listPropertyType = response.data
            
            this.typeList = []
            listPropertyType.forEach(element => {
                this.typeList.push({
                    code: element.PropertyTypeCode,
                    name: element.PropertyTypeName, 
                    id: element.PropertyTypeID, 
                    attritionRateDefault: element.AttritionRateDefault, 
                    usedYearDefault: element.UsedYearDefault 
                })
            })
        
        })
    }
}
</script>

<style>
    @import url(@/css/layout/form.css);
    @import url(@/css/base/combobox.css);
    @import url(@/css/base/button.css);
    @import url(@/css/base/input.css);
    @import url(@/css/base/icon.css); 
</style>