<template>
  <div class="user-info">
    <div class="login-list-input">
      <label class="form-title">Thông tin tài khoản</label>
      <StyleInput
        rightIcon="user"
        v-model:value="user.user_name"
        :label="'Họ và tên'"
      />
      <StyleInput
        rightIcon="home"
        v-model:value="user.user_address"
        :label="'Địa chỉ'"
      />
      <button class="login-button" @click="UpdateUserInfo()">
        Cập nhật thông tin
      </button>
      <div class="logout">
        <button class="btn-logout" @click="logout">Đăng xuất</button>
      </div>
    </div>
    <div class="list-order">
      <label class="form-title">Danh sách đơn hàng</label>
      <div class="order-item">
        <div>Ngày đặt hàng</div>
        <div>Ngày bắt đầu thuê</div>
        <div>Ngày kết thúc thuê</div>
        <div>Tổng tiền thanh toán</div>
        <div>Trạng thái đơn hàng</div>
      </div>
      <div
        class="order-item"
        v-for="item in listOrder"
        :key="item.order_id"
        @dblclick="viewOrder(item.order_id)"
      >
        <div>{{ datetimeToDate(item.order_date) }}</div>
        <div>{{ datetimeToDate(item.from_date) }}</div>
        <div>{{ datetimeToDate(item.to_date) }}</div>
        <div>{{ replaceNumber(item.total_order_deposit) }}</div>
        <div>{{ item.status }}</div>
      </div>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import { apiUpdateUserInfo, apiGetInfoUser } from "@/api/userApi";
import { apiGetOrderOfUser } from "@/api/orderApi";
import { replaceNumber, datetimeToDate } from "@/method/methodFormat";
export default {
  data() {
    return {
      typePassword: "password",
      check: false,
      user: {},
      listOrder: [],
    };
  },
  components: { StyleInput },
  methods: {
    replaceNumber,
    datetimeToDate,
    async UpdateUserInfo() {
      await apiUpdateUserInfo(this.user)
        .then(async () => {})
        .catch(() => {});
    },
    async getUserInfo() {
      await apiGetInfoUser(this.$cookies.get("userId")).then((response) => {
        this.user = response.data;
      });
    },
    async getOrderOfUser() {
      await apiGetOrderOfUser(this.$cookies.get("userId")).then((response) => {
        this.listOrder = response.data;
      });
    },
    logout() {
      this.$cookies.remove("token");
      this.$router.replace(this.$router.path);
      this.$router.push("/homepage");
    },
    viewOrder(orderId) {
      this.$router.replace(this.$router.path);
      this.$router.push(`/order/${orderId}`, {
        params: { id: orderId },
      });
    },
  },
  created() {
    this.getUserInfo();
    this.getOrderOfUser();
  },
};
</script>
<style>
@import url(./login.scss);
</style>