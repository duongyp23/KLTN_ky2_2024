<template>
    <tr  @click.exact="selectOnlyRow()" 
        @click.ctrl.exact="selectMultipleRow()"
        @click.shift.exact="onShiftClick()"
        :class="{'selectRow': isSelect}" @mousedown.right="openContextMenu" @contextmenu.prevent @dblclick="openFormFix()">
        <td  class="col-check" @click.stop="selectMultipleRow()" v-show="showCheckbox">
            <div class="icon icon-box"></div>
        </td>
        <td  class="col-stt" >{{stt}}</td>
        <td  class="col-code" >{{item.propertyCode}}</td>
        <td  class="col-name" >{{item.propertyName}}</td>
        <td  class="col-type" v-show="showInfo">{{item.propertyTypeName}}</td>
        <td  class="col-department" >{{item.departmentName}}</td>
        <td  class="col-num"  v-show="showInfo">{{item.quantity}}</td>
        <td  class="col-value">{{replaceNumber(item.markedPrice)}}</td>
        <td  class="col-value">{{replaceNumber(item.attritionValue)}}</td>
        <td  class="col-value">{{replaceNumber(item.markedPrice-item.attritionValue)}}</td>
        <td  class="col-info" v-show="showInfo" >{{checkIsActive(item.isActive)}}</td>
        <td  class="col-fix" v-show="showFix" >
            <button class="btn-icon-table" @click.stop="openFormFix()" title="Sửa">
                <div class="icon icon-pen"></div>
            </button>
            <button class="btn-icon-table " title="Nhân bản" v-show="showCheckbox" @click.stop="createCopy()">
                <div class="icon icon-copy"></div>
            </button>
            <button class="btn-icon-table " title="Xóa" @click.stop="deleteItem()">
                <div class="icon icon-trash"></div>
            </button>
        </td>
        <MsContextMenu 
            v-if="showContext"
            :display="showContextMenu" 
            ref="menu"
            @fixThisItem = "openFormFix()"
            @deleteThisItem = "deleteItem()"
            @createCopy="createCopy()"
        ></MsContextMenu>
    </tr>
    
</template>
<script>
//import Resource from '@/resource/MsResource';
import MsContextMenu from './MsContextMenu.vue'
import { replaceNumber } from "@/method/methodFormat";
import { onShiftClick, selectOnlyRow,calculateAttritionValue,selectMultipleRow,selectRowByCtrl,createCopy,openContextMenu,
    openFormFix, deleteItem} from '@/method/methodRow';
export default {
    
    data() {
        return {
            isSelect: false, // dòng được chọn
            showContextMenu : false, // mở context menu
        };
    },
    props: { 
        item: {}, // tài sản
        stt: {}, // số thứ tự
        selectList: {type:Array}, // danh sách tài sản được chọn
        showCheckbox: {default: true, type:Boolean},
        showInfo: {default: true, type:Boolean},
        showContext: {default:true}, // mở context menu
        showFix: {default:true}, // mở context menu
    },
    methods: {
        /**
         * Kiểm tra tài sản được sử dụng hay chưa
         * NTD 19/10/2022
         * @param {*} valueIsActive 
         */
        checkIsActive(valueIsActive) {
            if(valueIsActive == 0) {
                return "Chưa ghi tăng"
            } else {
                return "Đang sử dụng"
            }
        },
        /**
         * fomat lại dữ liệu kiểu số
         * NTD 8/8/2022
         */
        replaceNumber,
        /**
         * Sự kiện khi bấm chọn shift + click
         * NTD 5/10/2022
         */
        onShiftClick,
        /**
         * Sự kiện khi bấm vào dong. chỉ chọn mỗi dòng ấy
         * NTD 5/10/2022
         */
        selectOnlyRow,
        /**
         * Kiểm tra item có trong danh sách chọn không
         * NTD 11/9/2022
         */
        checkItemInList() {
            if(this.isSelect == true) {
                this.isSelect = false
            }

            this.selectList.forEach(element => {
                if(element.propertyID == this.item.propertyID) {
                    this.isSelect = true
                }
            });
            
        },
        /**
         * Tính giá trị hao mòn lũy kế theo năm
         * NTD 24/8/2022
         */
        calculateAttritionValue,
        /**
         * Mở form chỉnh sửa từ button trong row
         * NTD 8/8/2022
         */
        openFormFix,
        /**
         * Sự kiện khi bấm vào ô chckbox hoặc là ctrl + click
         * NTD 9/8/2022
         */
        selectMultipleRow,
        /**
         * chon row bang ctrl
         * NTD 6/9/2022
         */
        selectRowByCtrl,
        /**
         * Tạo bản sao
         * NTD 14/8/2022
         */
        createCopy,
        /**
         * Mở contextmenu
         *  NTD 14/8/2022
         */
        openContextMenu,
        /**
         * send lệnh xóa item
         * NTD 14/8/2022
         */
         deleteItem,
        

    },
    mounted() {
        this.emitter.on("checkRow", () => {
            this.checkItemInList()
        })
    },
    created() {
        this.checkItemInList()
    },
    components: { MsContextMenu },
    watch: {
        /**
         * kiểm tra item có trong danh sách được chọn không khi thay đổi danh sách
         * NTD 11/9/2022
         */
        selectList() {
            this.checkItemInList()
        },
        /**
         * Kiểm tra khi thay đổi item nhận được
         * NTD 11/9/2022
         */
        item() {
            this.checkItemInList()
        }
    }
}
</script>
<style>
@import url(../../css/layout/table.css);
</style>