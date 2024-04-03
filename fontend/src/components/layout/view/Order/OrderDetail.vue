<template>
  <div class="order-detail">
    <popup-payment></popup-payment>
    <div class="detail-list">
      <div
        class="item"
        v-for="item in orderDetails"
        :key="item.order_detail_id"
      >
        <div
          class="small-img"
          :style="'background-image : url(' + item.product_image_url + ')'"
        ></div>
        <div
          class="product-name"
          v-tooltip.bottom="{ content: item.product_name }"
        >
          {{ item.product_name }}
        </div>
        <div class="price">
          {{ replaceNumber(item.product_deposit) }}
        </div>
        <div class="price">
          {{ replaceNumber(item.product_payment) }}
        </div>
        <div class="order-type">
          <input
            type="radio"
            v-model="item.order_type"
            :value="0"
            :disabled="isDisable"
            @change="checkMoney(item, 0)"
          />
          Thuê
        </div>
        <div class="order-type">
          <input
            type="radio"
            v-model="item.order_type"
            :value="1"
            :disabled="isDisable"
            @change="checkMoney(item, 1)"
          />
          Mua
        </div>
        <button
          class="btn-img remove"
          @click="remove(item)"
          :style="'background-image : url(' + Images.del + ')'"
          v-show="!isDisable"
        ></button>
      </div>
    </div>
    <div class="total-order">
      <div class="amount-row">
        <div>Tổng tiền phải cọc</div>
        <div>{{ replaceNumber(order.total_order_deposit) }}</div>
      </div>
      <div class="amount-row">
        <div>Chi phí tạm tính</div>
        <div>{{ replaceNumber(order.total_order_payment) }}</div>
      </div>
      <div class="amount-row">
        <div>Số tiền hoàn lại</div>
        <div>{{ replaceNumber(order.total_order_return) }}</div>
      </div>
      <div class="date-row">
        <style-input
          :label="'Ngày bắt đầu thuê'"
          class="date-input"
          v-model:value="order.from_date"
          type="date"
          :disabled="isDisable"
        ></style-input>
        <style-input
          :label="'Ngày kết thúc thuê'"
          class="date-input"
          v-model:value="order.to_date"
          type="date"
          :disabled="isDisable"
        ></style-input>
      </div>

      <style-input
        :label="'Họ tên người nhận'"
        class="user-name"
        v-model:value="order.user_name"
        :disabled="isDisable"
      ></style-input>
      <style-input
        :label="'Số điện thoại'"
        class="phone"
        v-model:value="order.phone_number"
        :disabled="isDisable"
      ></style-input>
      <style-input
        :label="'Địa chỉ nhận hàng'"
        class="address"
        v-model:value="order.address"
        :disabled="isDisable"
      ></style-input>
      <style-input
        :label="'Ghi chú'"
        type="textarea"
        class="description"
        v-model:value="order.description"
        :disabled="isDisable"
      ></style-input>
      <div>
        <input
          type="radio"
          v-model="order.payment_type"
          value="0"
          :disabled="isDisable"
        />
        Thanh toán khi nhận hàng
      </div>
      <div>
        <input
          type="radio"
          v-model="order.payment_type"
          value="1"
          :disabled="isDisable"
        />
        Thanh toán trước
      </div>
      <button
        @click="saveOrder(false)"
        v-if="order.status == 1"
        class="form-btn btn3"
      >
        Xác nhận đơn hàng
      </button>
      <button
        @click="openPopupPayment()"
        v-else-if="order.status == 2 && !isManager"
        class="form-btn btn3"
      >
        Thanh toán đơn hàng
      </button>
      <div class="flex-row center" v-if="isManager && order.status == 3">
        <button
          @click="updateOrderStatus(1)"
          v-if="order.payment_type == 1"
          class="form-btn btn2"
        >
          Chưa thanh toán
        </button>
        <button @click="updateOrderStatus(4)" class="form-btn btn3">
          Đã gửi hàng
        </button>
      </div>
      <div class="flex-row center" v-if="isManager && order.status == 4">
        <button class="form-btn btn1" @click="openPopupPayment()">
          Hoàn tiền cọc
        </button>
        <button @click="saveOrder(false)" class="form-btn btn3">
          Hoàn thành
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import StyleInput from "../../../base/StyleInput/StyleInput.vue";
import Images from "@/assets/icon/images";
import { apiDeleteOrderDetail } from "@/api/orderDetailApi";
import { replaceNumber } from "@/method/methodFormat";
import {
  apiUpdateOrderData,
  apiGetOrder,
  apiUpdateOrder,
} from "@/api/orderApi";
import PopupPayment from "./PopupPayment.vue";
export default {
  setup() {},
  data() {
    return {
      order: {
        total_order_deposit: 0,
        total_order_payment: 0,
        total_order_return: 0,
      },
      orderDetails: [],
      Images,
      isDisable: false,
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  components: { StyleInput, PopupPayment },
  methods: {
    checkMoney(item, type) {
      switch (type) {
        case 0:
          item.product_payment = item.product_rental;
          break;
        case 1:
          item.product_payment = item.product_deposit;
          break;
        default:
          break;
      }
      this.calculateOrderMoney();
    },
    async updateOrderStatus(status) {
      await apiUpdateOrder({
        order_id: this.order.order_id,
        status: status,
      });
    },
    async saveOrder(isPay) {
      if (!isPay) {
        if (!this.isManager) {
          this.order.order_date = new Date();
          this.order.status = this.order.payment_type == 0 ? 3 : 2;
        } else {
          this.order.status = 5;
        }
      }
      await apiUpdateOrderData(this.order, this.orderDetails).then(() => {
        debugger;
        if (this.order.status == 3) {
          this.closeForm();
        } else if (this.order.status == 2) {
          this.openPopupPayment();
        }
      });
    },
    openPopupPayment() {
      this.emitter.emit("openPopupPayment", this.order);
    },
    async remove(item) {
      await apiDeleteOrderDetail(item.order_detail_id).then(() => {
        this.loadData();
      });
    },
    async loadData() {
      await apiGetOrder(this.$route.params.id).then((response) => {
        this.order = response.data.order;
        this.orderDetails = response.data.orderDetails;
        this.calculateOrderMoney();
        if (this.order.status != 1) {
          this.isDisable = true;
        }
      });
    },
    replaceNumber,
    calculateOrderMoney() {
      this.order.total_order_deposit = 0;
      this.order.total_order_payment = 0;
      this.orderDetails.forEach((item) => {
        this.order.total_order_deposit += item.product_deposit;
        this.order.total_order_payment += item.product_payment;
      });
      this.order.total_order_return =
        this.order.total_order_deposit - this.order.total_order_payment;
    },
    closeForm() {
      this.$router.replace(this.$router.path);

      if (!this.isManagers) {
        this.$router.push("/homepage");
      } else {
        this.$router.push("/orderlist");
      }
    },
  },
  created() {
    this.loadData();
  },
  mounted() {
    this.emitter.on("updateOrderData", (isSuccess) => {
      switch (this.order.status) {
        case 2:
          if (isSuccess) {
            this.order.status = 3;
            this.saveOrder(true);
          } else {
            this.order.status = 1;
            this.saveOrder(true);
          }
          break;
        case 4:
          if (isSuccess) {
            this.order.status = 5;
            this.saveOrder(true);
          }
          break;
        default:
          break;
      }
    });
  },
};
</script>
<style>
@import url(./orderDetail.scss);
</style>