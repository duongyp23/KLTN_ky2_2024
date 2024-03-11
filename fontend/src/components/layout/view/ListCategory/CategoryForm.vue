<template>
    <div class="category-form form-view" v-show="isOpen" @keyup.esc="closeForm()">
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
                        <label class="Form-Label">Mã nhãn dán<span style="color:red">*</span></label>
                        <input 
                            type="text" 
                            class="form-input" 
                            ref="inputCategoryCode" 
                            v-model="item.category_code" 
                            :class="{'error':checkNull('category_code')}" 
                            maxlength="20"
                        >
                        <label v-show="checkNull('category_code')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    
                    <div class="form-big-div col-flex">
                        <label class="Form-Label">Tên nhãn dán <span style="color:red">*</span></label>
                        <input 
                            type="text" class="form-input" 
                            v-model="item.category_name" 
                            :class="{'error':checkNull('category_name')}" 
                            maxlength="255" 
                            ref="inputCategoryName"
                        >
                        <label v-show="checkNull('category_name')" class="text-error">Cần phải nhập thông tin</label>
                    </div>
                    <div class="form-small-div col-flex">
                        <label class="Form-Label">Loại nhãn dán</label>
                        <MsCombobox 
                            :items="categoryType" 
                            v-model:value ="item.type_name"
                            v-model:id = "item.type_id"
                        ></MsCombobox>
                    </div>
                    
                </div>       
                <div class="row-flex">
                    <div class="col-flex">
                        <label class="Form-Label">Diễn giải</label>
                        <input 
                            type="text" 
                            class="form-input"
                            v-model="item.description"
                        >
                    </div>
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
import Resource from "@/resource/MsResource";
import { apiAddNewCategory, apiGetPropertyByID, apiUpdateProperty } from "@/api/modules";
import MsCombobox from "@/components/base/MsCombobox.vue";
import CategoryType from "@/resource/CategoryType";

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
            // Tài sản
            item: {
                
            },
            isOpen: false, // hiển thị
            label: '', // tiêu đề
            categoryType: CategoryType,
            listInput: ['category_code', 'category_name'], 
        }
        
    },
    methods: {
        /***
         * Khởi tạo 1 tài sản mới
         * NTD 5/9/2022
         */
         defaultItem() {
            return {
                
            };
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
            this.isOpen= false
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
        setValue() {
            
            
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
                    }
                }
            });
            if (this.check == false) {
                if(this.isAdd == true) {
                    this.addNewCategory()
                } else if(this.isFix == true){
                    this.updateProperty()
                }
            }
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
        async addNewCategory() {
            await  apiAddNewCategory(this.item)
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
    components: { MsCombobox},
    mounted() {
        /**
         * Mở form thêm tài sản từ Tool
         * NTD 8/8/2022
         */
        this.emitter.on('addNewCategory',async () => {
            this.setValue(this.defaultItem())
            this.isAdd = true
            this.isFix = false
            this.isOpen= true
            this.check = false
            this.setLabel('Thêm nhãn dán')
            this.$nextTick(() => this.$refs.inputCategoryCode.focus())
            
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
            this.setLabel('Sửa nhãn dán')
            this.getPropertyById(item.propertyID)
            this.tempItemValue = Object.assign({},this.item)
            this.isOpen = true
            this.$nextTick(() => this.$refs.inputPropertyCode.focus())
        })
    },
    watch: {
    },
}
</script>

<style>
    @import url(@/css/layout/form.scss);
    @import url(@/css/base/combobox.css);
    @import url(@/css/base/button.css);
    @import url(@/css/base/input.css);
    @import url(@/css/base/icon.css); 
</style>