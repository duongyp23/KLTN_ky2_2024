<template>
    <div class="tool">
        <div style="display: flex; height: 100%;position: relative;">
            <div class="input-tool">
                <div class="icon icon-search-input" style="margin-left:10px; position: absolute;"></div>
                <input 
                    type="text" class="input-box" 
                    style="margin-left:39px" 
                    placeholder="Tìm kiếm sản phẩm" 
                    @change="searchItem($event.target.value)" 
                    ref="inputSearch"
                    @input="inputNullValue($event.target.value)"
                >
            </div>
            <MsCombobox 
                style="margin-left: 11px;width: 219px;" 
                :items="type" 
                v-model:value ="inputPropertyType"
                :placeholder="'Nhãn dán'"
                :icon="'select'" 
                v-model:id = "propertyTypeID"
                
            ></MsCombobox>
        </div>
        <div style="display:flex;">
            <button class="btn-tool btn-add"  @click="openForm">+ Thêm sản phẩm</button>
        </div>
        <MsForm></MsForm>
    </div>
    
</template>
<script>
import MsCombobox from "@/components/base/MsCombobox.vue";
import MsForm from "./MsForm.vue";
import { apiGetDepartment, apiGetPropertyType } from '@/api/modules';
export default {
    
    data() {
        return {
            propertyTypeID: null, // ID loại tài sản
            departmentID: null, // ID phòng ban
            inputPropertyType: '', // tên loại tài sản
            inputDepartment:'', // tên phòng ban
            type: [], //danh sách loại tài sản
            department: [], // danh sách phòng ban
            file: File, //file
            arrayBuffer: null, // đọc file
        }
    },
    components: { MsCombobox , MsForm},
    methods: {
        /**
         * Khi input null thì load lại data
         * NTd 5/10/2022
         */
        inputNullValue(value) {
            if(value == null || value == "") {
                this.emitter.emit('searchItemInList', value)
            }
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
         * chọn input file
         * NTD 15/9/2022
         */
        clickInputFile() {
            this.$refs.inputfile.click()
        },
        /**
         * Mở form thêm tài sản
         * NTD 10/8/2022
         */
        openForm () {
            this.emitter.emit('openForm')
        },
    
        /**
         * Tìm kiếm tài sản 
         * NTD 14/8/2022
         */
        searchItem(value) {    
            this.emitter.emit('searchItemInList', value)
        },
       

    },
    mounted() {
        
    },
    async created() {
        /***
         * Lấy danh sách phòng ban
         * NTD 5/9/2022
         */
        await  apiGetDepartment()
        .then(response => {
            var listDepartment = response.data
            this.department = []
            listDepartment.forEach(element => {
                this.department.push({name: element.DepartmentName, id: element.DepartmentID})
            })
        
        })
        .catch(() => {
            
        }),
        /***
         * Lấy danh sách loại tài sản
         * NTD 5/9/2022
         */
        await  apiGetPropertyType()
        .then(response => {
            var listPropertyType = response.data
            this.type = []
            listPropertyType.forEach(element => {
                this.type.push({name: element.PropertyTypeName, id: element.PropertyTypeID})
            })
        })
        .catch(()=> {
            
        })

    },
    watch: {
        selectList() {
            
        },
        /**
         * Lọc tài sản theo loại
         * NTD 5/9/2022
         */
        propertyTypeID() {
            this.emitter.emit('sortType', this.propertyTypeID)
        },
        /***
         * Lọc tài sản theo bộ phận
         * NTD 5/9/2022
         */
        departmentID() {
            this.emitter.emit('sortDepartment', this.departmentID )
        }
    }
}
</script>
<style>
 @import url(@/css/layout/page.css);
@import url(@/css/base/button.css);
@import url(@/css/base/combobox.css);
@import url(@/css/base/icon.css);
@import url(@/css/base/input.css);
</style>