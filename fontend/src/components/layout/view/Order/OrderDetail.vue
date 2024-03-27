<template>
  <div class="order-detail">
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
        <div class="deposit-price">{{ item.product_deposit }}</div>
        <div class="payment-price">{{ item.product_payment }}</div>
        <button
          class="btn-img remove"
          @click="remove(item)"
          :style="'background-image : url(' + Images.del + ')'"
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
      <style-input
        :label="'Họ tên người nhận'"
        class="user-name"
        v-model:value="order.user_name"
      ></style-input>
      <style-input
        :label="'Số điện thoại'"
        class="phone"
        v-model:value="order.phone_number"
      ></style-input>
      <style-input
        :label="'Địa chỉ nhận hàng'"
        class="address"
        v-model:value="order.address"
      ></style-input>
      <style-input
        :label="'Ghi chú'"
        type="textarea"
        class="description"
        v-model:value="order.description"
      ></style-input>
      <div>
        <input type="radio" v-model="order.payment_type" value="0" />
        Thanh toán khi nhận hàng
      </div>
      <div>
        <input type="radio" v-model="order.payment_type" value="1" />
        Thanh toán trước
      </div>
      <button v-if="order.status == 1" @click="saveOrder">
        Xác nhận đơn hàng
      </button>
    </div>
  </div>
</template>
<script>
import StyleInput from "../../../base/StyleInput/StyleInput.vue";
import Images from "@/assets/icon/images";
import { apiDeleteOrderDetail } from "@/api/orderDetailApi";
import { replaceNumber } from "@/method/methodFormat";
import { apiUpdateOrder, apiGetOrder } from "@/api/orderApi";
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
    };
  },
  components: { StyleInput },
  methods: {
    async saveOrder() {
      this.order.status = 2;
      await apiUpdateOrder(this.order).then(() => {
        this.emitter.emit(
          "openToastMessage",
          "Đơn hàng được tạo thành công. Vui lòng chờ người bán gửi hàng"
        );
        this.$router.replace(this.$router.path);
        this.$router.push("/homepage");
      });
    },
    async remove(item) {
      await apiDeleteOrderDetail(item.order_detail_id).then(() => {
        this.loadData();
      });
    },
    async loadData(id) {
      await apiGetOrder(id).then((response) => {
        this.order = response.data.order;
        this.orderDetails = response.data.orderDetails;
        this.calculateOrderMoney();
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
  },
  created() {
    this.loadData(this.$route.params.id);
  },
};
</script>
<style>
@import url(./orderDetail.scss);
</style>