<template>
    <div class="form-view" v-if="showFixBudget">
        <div class="form">
            <div class="form-header">
                <label style="font-size:20px">Sửa tài sản {{item.propertyName}}</label>
                <button class="btn-header" style="border: none; background:transparent" title="Đóng">
                    <div class="icon icon-close" v-on:click="closeForm"></div>
                </button>
            </div>
            <div class="form-center voucher" style="padding:0px; background-color: #fff; padding: 12px 12px; position: relative;">
                <label for="">Bộ phận sử dụng</label>
                <input type="text" class="form-input" style="width: 70%" disabled :value="item.departmentName">
                <label><b>Nguyên giá</b></label>
                <div class="grid-budget" style="height: 15px">
                    <div class="grid-combobox">
                        Nguồn hình thành
                    </div>
                    <div class="grid-mount">
                        Giá trị
                    </div>
                </div>
                <div class="view-input-budget">
                    <div v-for="(item,index) in budget" :key="item.BudgetID" class="grid-budget">
                        <div class="grid-combobox">
                            <MsCombobox
                                style="height: 38px"
                                :items="budgetList"
                                v-model:value = "item.BudgetName"
                                v-model:id = "item.BudgetID"
                                :class="{'error':check && checkBudget(item,index)}"
                            ></MsCombobox>
                            <label v-show="check && checkBudget(item,index)" class="text-error">
                                <div v-if="item.BudgetID == null">Nguồn tài sản không được để trống</div>
                                <div v-if="item.BudgetID != null">Nguồn tài sản không được trùng lặp</div>
                            </label>
                        </div>
                        <div class="grid-mount">
                            <MsInputNumber
                                v-model:numberValue="item.Mount"
                                @changeValue="totalMount()"
                                :class="{'error':check && checkMount(item)}"
                            ></MsInputNumber>
                            <label v-show="check && checkMount(item)" class="text-error">Giá trị không được để trống</label>
                        </div>
                        <div class="grid-button">
                            <button class="btn-icon" title="Thêm" @click.stop="addNewBudget()">
                                <div class="icon icon-plus"></div>
                            </button>
                            <button class="btn-icon" title="Xóa" @click.stop="removeBudget(index)" v-show="budget.length > 1">
                                <div class="icon icon-minus"></div>
                            </button>
                        </div>
                    </div>
                </div>
                <div class="grid-budget" style="position:sticky; bottom:0px">
                    <div class="grid-combobox">
                        <input type="text" class="form-input" style="width:482px" disabled value="Tổng">
                    </div>
                    <div class="grid-mount">
                        <MsInputNumber
                            :numberValue="tempItem.markedPrice"
                            disabled
                        ></MsInputNumber>
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
import MsCombobox from "@/components/base/MsCombobox.vue";
import MsInputNumber from "@/components/base/MsInputNumber.vue";
import { apiGetBudget } from "@/api/modules";
import Resource from "@/resource/MsResource";

export default ({
    data() {
        return {
            check: false,
            isSelectAllRow: false,
            datalist : [],
            selectList: [],
            keyword: null,
            totalItem: 0,
            numberItemInTable: 20,
            pageNumber: 1,
            showSelect: false,
            budgetList: [],
            budget: [],
            tempItem: {}
        }
    },
    props: {
        showFixBudget: {default:false, type:Boolean},
        item:{},
    },
    components: {MsCombobox, MsInputNumber},
    methods: {
        /**
         * Kiểm tra nguồn tài sản có bị trùng hoặc null
         * NTD 19/10/2022
         */
        checkBudget(item,index) {
            if(item.BudgetID == null || item.BudgetID == "" || item.BudgetName == "" ||item.BudgetName == null){
                return true
            }
            for (let i = 0; i < index; i++) {
                if(this.budget[i].BudgetID == item.BudgetID) {
                    return true
                }
            }
            return false
        },
        /**
         * Kiểm tra gái trị nguồn tài sản
         * NTD 19/10/2022
         */
        checkMount(item) {
            if(item.Mount == 0 || item.Mount == null) {
                return true
            }
            return false
        },
        /**
         * Sự kiện khi bấm nứt lưu. check và save
         * NTD 19/10/2022
         */
        saveForm() {
            this.check = false
            for (let i = 0; i < this.budget.length; i++) {
                if(this.check ==  false) {
                    this.check = this.checkBudget(this.budget[i], i)
                }
                if(this.check ==  false) {
                    this.check = this.checkMount(this.budget[i])
                }
            }
            if(this.check ==  false) {
                this.tempItem.budget = JSON.stringify(this.budget)
                this.$emit('update:item', this.tempItem)
                this.$emit('saveBudget')
                this.$emit('update:showFixBudget', false)
            }
        },
        /**
         * Thêm mới 1 dòng nguồn tài sản
         * NTD 19/10/2022
         */
        addNewBudget() {
            this.budget.push({
                BudgetID: null,
                BudgetName: null,
                Mount: 0
            })
        },
        /**
         * Xóa 1 nguồn tài sản
         * NTD 19/10/2022
         * @param {} index 
         */
        removeBudget(index) {
            this.budget.splice(index, 1)
            this.totalMount()
        },
        /**
         * Đóng form sửa nguồn tài sản
         * NTD 19/10/2022
         */
        closeForm() {
            this.$emit('update:showFixBudget', false)
        },
        /**
         * Gọi api lấy danh sách nguồn tài sản
         * NTD 19/10/2022
         */
        async getBudget(){
            await apiGetBudget()
            .then(response => {
                var listBudget = response.data
                this.budgetList = []
                listBudget.forEach(element => {
                    this.budgetList.push({name: element.BudgetName, id: element.BudgetID})
                })
            })
            .catch(() => {
                this.emitter.emit('openPopupNotice', Resource.PopupNotice.ErrorGetBudget)
            })
            
        },
        /**
         * Tính tổng của các nguồn tài sản vfa ssawtj marketprice
         * NTD 19/10/2022
         */
        totalMount() {
            this.tempItem.markedPrice = 0
            this.budget.forEach(element => {
                this.tempItem.markedPrice += element.Mount
            });
        }
    },
    created() {
        
        this.getBudget()
        
    }, 
    watch: {
        /**
         * Kiểm tra đặt budget khi mở mở form
         * NTD 19/10/2022
         */
        showFixBudget() {
            this.tempItem = Object.assign({}, this.item)
            this.budget = JSON.parse(this.tempItem.budget);
            this.totalMount()
            this.check = false
        }
        
    }
})
</script>
<style >
@import url(@/css/layout/form.css);
</style>
