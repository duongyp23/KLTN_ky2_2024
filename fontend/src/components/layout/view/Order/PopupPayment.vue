<template>
  <div class="popup-payment" v-if="isOpenPopup">
    <div class="popup">
      <div class="title">Thanh toán đơn hàng</div>
      <div class="infomation mt-1">
        <div class="flex-row between mt-1">
          <div>Số tài khoản:</div>
          <div>{{ account.bank_account_number }}</div>
        </div>
        <div class="flex-row between">
          <div>Ngân hàng:</div>
          <div>{{ account.bank_name }}</div>
        </div>
        <div class="flex-row between">
          <div>Mã ngân hàng:</div>
          <div>{{ account.bank_code }}</div>
        </div>
        <div class="flex-row between">
          <div>Số cần chuyển:</div>
          <div>{{ replaceNumber(payment) }}</div>
        </div>
        <div class="qrcode">
          <div
            class="img"
            :style="'background-image: url(' + account.qr_code_url + ')'"
          ></div>
        </div>
      </div>
      <div class="popup-footer mt-1">
        <button class="popup-btn btn2" @click="UpdateNewType(false)">
          Hủy thanh toán
        </button>
        <button class="popup-btn btn3" @click="UpdateNewType(true)">
          Thanh toán thành công
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { apiGetInfoUser, apiGetBankAccountAdmin } from "@/api/userApi";
import { replaceNumber } from "@/method/methodFormat";
export default {
  data() {
    return {
      order: {},
      account: {},
      type: 0,
      payment: 0,
      isOpenPopup: false,
    };
  },
  methods: {
    UpdateNewType(isSuccess) {
      this.emitter.emit("updateOrderData", isSuccess);
      this.isOpenPopup = false;
    },
    replaceNumber,
    closeForm() {
      this.isOpenPopup = false;
    },
    async getBankData() {
      switch (this.order.status) {
        case 2:
          await apiGetBankAccountAdmin().then((response) => {
            this.account = response.data;
            this.payment = this.order.total_order_deposit;
          });
          break;
        case 4:
          await apiGetInfoUser(this.$cookies.get("userId")).then((response) => {
            this.account = response.data;
            this.payment = this.order.total_order_return;
          });
          break;
      }
    },
  },
  mounted() {
    this.emitter.on("openPopupPayment", (order) => {
      this.order = order;
      this.getBankData();
      this.isOpenPopup = true;
    });
  },
};
</script>
<style>
@import url(./popupPayment.scss);
</style>