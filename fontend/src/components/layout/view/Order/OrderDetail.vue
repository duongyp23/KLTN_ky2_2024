<template>
  <div class="order-detail">
    <popup-payment></popup-payment>
    <div class="detail-list">
      <div
        v-for="(orderDetail, index) in orderDetails"
        :key="index"
        class="order-detail-item flex-row"
      >
        <div
          class="product-img"
          :style="
            'background-image : url(' +
            getFirstImage(orderDetail.product_image_url) +
            ')'
          "
        ></div>
        <div class="flex-column">
          <div>{{ orderDetail.product_name }}</div>
          <div class="flex-row">
            <StyleInput
              v-model:value="orderDetail.option_code"
              :label="'Mã chọn'"
              :disabled="true"
            ></StyleInput>
            <InputNumber
              v-model:numberValue="orderDetail.quantity"
              :label="'Số lượng'"
              :disabled="true"
            ></InputNumber>
            <InputNumber
              v-model:numberValue="orderDetail.product_payment"
              :label="'Tổng tiền'"
              @changeValue="calculateOrderMoney"
              :disabled="true"
            ></InputNumber>
          </div>
        </div>
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
          @change="checkMoney"
        ></style-input>
        <style-input
          :label="'Ngày kết thúc thuê'"
          class="date-input"
          v-model:value="order.to_date"
          type="date"
          :disabled="isDisable"
          @change="checkMoney"
        ></style-input>
      </div>
      <div class="flex-row">
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
      </div>

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
      <div class="mb-1">
        <input
          type="radio"
          v-model="order.payment_type"
          value="1"
          :disabled="isDisable"
        />
        Thanh toán online
      </div>
      <button
        @click="saveOrder(false)"
        v-if="order.status == 1"
        class="form-btn btn3"
      >
        Xác nhận đơn hàng
      </button>

      <div class="flex-row center" v-if="order.status == 2">
        <button
          @click="removeOrder"
          v-if="order.payment_type == 1"
          class="form-btn btn2"
        >
          Hủy đơn hàng
        </button>
        <button @click="openPopupPayment()" class="form-btn btn3">
          Thanh toán đơn hàng
        </button>
      </div>
      <div class="flex-row center" v-if="isManager && order.status == 3">
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
import { replaceNumber, datetimeToDate } from "@/method/methodFormat";
import {
  apiUpdateOrderData,
  apiGetOrder,
  apiUpdateOrder,
  apiOrderPayment,
  apiDeleteOrder,
} from "@/api/orderApi";
import PopupPayment from "./PopupPayment.vue";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";
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
      products: [],
      Images,
      isDisable: false,
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  components: { StyleInput, PopupPayment, InputNumber },
  methods: {
    datetimeToDate,
    async removeOrder() {
      await apiDeleteOrder(this.order.order_id).then(() => {
        this.closeForm();
      });
    },
    checkMoney() {
      if (
        this.order.from_date &&
        this.order.to_date &&
        this.order.to_date > this.order.from_date
      ) {
        // Chuyển đổi chuỗi YYYY-MM-DD thành đối tượng Date
        const ngay1 = new Date(this.order.from_date);
        const ngay2 = new Date(this.order.to_date);

        // Tính số mili giây chênh lệch giữa hai ngày
        const miliGiayChenhLech = Math.abs(ngay2 - ngay1);

        // Chuyển đổi mili giây thành số ngày (1 ngày = 86400 giây = 86400,000 mili giây)
        const soNgayChenhLech = miliGiayChenhLech / (1000 * 60 * 60 * 24);
        const month = Math.floor(soNgayChenhLech / 30);
        const monthLeft = soNgayChenhLech % 30;
        const week = Math.floor(monthLeft / 7);
        const dayleft = monthLeft % 7;
        this.orderDetails.forEach((item) => {
          let product = this.products.find(
            (x) => x.product_id == item.product_id
          );
          let payment =
            month * product.rental_price_month +
            week * product.rental_price_week +
            dayleft * product.rental_price_day;
          item.product_payment = payment * item.quantity;
          if (item.product_payment > item.product_deposit) {
            item.product_payment = item.product_deposit;
          }
        });
        this.calculateOrderMoney();
      }
    },
    async updateOrderStatus(status) {
      await apiUpdateOrder({
        order_id: this.order.order_id,
        status: status,
      }).then(() => {
        this.order.status = status;
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
        if (this.order.status == 3) {
          this.closeForm();
        } else if (this.order.status == 2) {
          this.openPopupPayment();
        }
      });
    },
    async openPopupPayment() {
      await apiOrderPayment(this.order.order_id).then((response) => {
        window.location = response.data.orderurl;
      });
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
        this.products = response.data.products;
        this.calculateOrderMoney();
        if (this.order.status != 1) {
          this.isDisable = true;
        }
        if (this.order.from_date == null) {
          this.order.from_date = this.datetimeToDate(new Date());
        } else {
          this.order.from_date = this.datetimeToDate(this.order.from_date);
        }
        if (this.order.to_date == null) {
          this.order.to_date = this.datetimeToDate(new Date());
        } else {
          this.order.to_date = this.datetimeToDate(this.order.to_date);
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
    getFirstImage(listImage) {
      let arrImg = listImage.split(";");
      return arrImg[0];
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