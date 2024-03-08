<template>
    <tr  @click.exact="selectOnlyRow()" 
        @click.ctrl.exact="selectMultipleRow()"
        @click.shift.exact="onShiftClick()"
        :class="{'selectRow': isSelect}" >
        <td  class="col-check" @click.stop="selectMultipleRow()"
    >
            <div class="icon icon-box"></div>
        </td>
        <td  class="col-stt" >{{stt}}</td>
        <td  class="col-code" style="color:blue" >
            <router-link class="link" @click.stop="addParamToRoute(item.voucherID)" to="/form">
                {{item.voucherCode}}
            </router-link>
        </td>
        <td class="col-date">{{formatDate(item.incrementDate)}}</td>
        <td class="col-date">{{formatDate(item.startUsingDate)}}</td>
        <td class="col-value">{{replaceNumber(item.totalPrice)}}</td>
        <td class="col-description">{{item.description}}</td>
        <td  class="col-fix"  >
            <router-link class="link" @click.stop="addParamToRoute(item.voucherID)" to="/form">
                <button class="btn-icon-table" title="Sửa">
                   <div class="icon icon-pen"></div>
                </button>
            </router-link>
            <button class="btn-icon-table" title="Xóa" @click.stop="$emit('deleteVoucher')">
                <div class="icon icon-trash"></div>
            </button>
        </td>
    </tr>
    
</template>
<script>
//import Resource from '@/resource/MsResource'
import { replaceNumber , datetimeToDate} from "@/method/methodFormat"
import {onShiftClick, selectOnlyRow,openFormFix,selectMultipleRow} from '@/method/methodRow';
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
        selectList : {type: Array}
    },
    methods: {
        /**
         * format hiển thị ngày tháng kiểu dd/mm/yyyy
         * NTD 2/11/2022
         * @param {*} date 
         */
        formatDate(date){
            const current = new Date(date)
            var day
            if(current.getMonth()+1< 10) {
                if(current.getDate() < 10) {
                    day = `0${current.getDate()}/0${current.getMonth()+1}/${current.getFullYear()}`
                } else {
                    day = `${current.getDate()}/0${current.getMonth()+1}/${current.getFullYear()}`
                }
            } else {
                if(current.getDate() < 10) {
                    day = `0${current.getDate()}/${current.getMonth()+1}/${current.getFullYear()}`
                } else {
                    day = `${current.getDate()}/${current.getMonth()+1}/${current.getFullYear()}`
                }
            }
            return day
        },
        /**
         * chỉnh sửa router và param
         * NTD 2/11/2022
         * @param {*} voucherID 
         */
        addParamToRoute(voucherID) {
            //this.$router.replace(this.$route.path)
            this.$router.push( `ghitang/form/${voucherID}`, {params: {id: voucherID}})
        },
        replaceNumber,
        datetimeToDate,
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
         checkItemInList () {
            if(this.isSelect == true) {
                this.isSelect = false
            }

            this.selectList.forEach(element => {
                if(element.voucherID == this.item.voucherID) {
                    this.isSelect = true
                }
            });
            
        },
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
    },
    mounted() {
        this.emitter.on("checkRow", () => {
            this.checkItemInList()
        })
    },
    created() {
        this.checkItemInList()
    },
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