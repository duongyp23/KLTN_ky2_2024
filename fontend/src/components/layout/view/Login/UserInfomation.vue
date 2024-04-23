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
      <StyleInput
        rightIcon="phone"
        v-model:value="user.phone_number"
        :label="'Số điện thoại'"
      />
      <!-- <StyleInput
        v-model:value="user.bank_account_number"
        :label="'Số tài khoản'"
      />
      <StyleInput v-model:value="user.bank_name" :label="'Tên ngân hàng'" />
      <StyleInput v-model:value="user.bank_code" :label="'Mã ngân hàng'" />
      <div class="qrcode" v-if="!isManager">
        <div
          v-if="user.qr_code_url"
          class="img"
          :style="'background-image: url(' + user.qr_code_url + ')'"
        ></div>
        <div
          v-else
          class="img"
          @click="openUploadWidget()"
          :style="'background-image: url(' + Images.addP + ')'"
        ></div>
      </div> -->
      <button class="login-button" @click="UpdateUserInfo()">
        Cập nhật thông tin
      </button>
      <div class="logout">
        <button class="btn-logout" @click="logout">Đăng xuất</button>
      </div>
    </div>
    <div class="list-order" v-if="!isManager">
      <label class="form-title">Danh sách đơn hàng</label>
      <table class="table">
        <thead>
          <tr>
            <th>Ngày đặt hàng</th>
            <th>Ngày bắt đầu thuê</th>
            <th>Ngày kết thúc thuê</th>
            <th>Tổng tiền đơn hàng</th>
            <th>Trạng thái đơn hàng</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="item in listOrder"
            :key="item.order_id"
            @dblclick="viewOrder(item.order_id)"
          >
            <td>{{ datetimeToDate(item.order_date) }}</td>
            <td>{{ datetimeToDate(item.from_date) }}</td>
            <td>{{ datetimeToDate(item.to_date) }}</td>
            <td>{{ replaceNumber(item.total_order_deposit) }}</td>
            <td>{{ checkStatusOrder(item.status) }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import { apiUpdateUserInfo, apiGetInfoUser } from "@/api/userApi";
import { apiGetOrderOfUser, apiUpdateOrder } from "@/api/orderApi";
import {
  replaceNumber,
  datetimeToDate,
  checkStatusOrder,
} from "@/method/methodFormat";
import Images from "@/assets/icon/images";
export default {
  data() {
    return {
      typePassword: "password",
      check: false,
      user: {},
      listOrder: [],
      Images,
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  components: { StyleInput },
  methods: {
    openUploadWidget() {
      const myWidget = window.cloudinary.createUploadWidget(
        {
          cloudName: "dmci423da",
          uploadPreset: "kltn-image",
        },
        (error, result) => {
          if (!error && result && result.event === "success") {
            this.user.qr_code_url = result.info.url;
          }
        }
      );

      myWidget.open();
    },
    replaceNumber,
    datetimeToDate,
    checkStatusOrder,
    async UpdateUserInfo() {
      await apiUpdateUserInfo(this.user).then(async () => {});
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
      this.$cookies.remove("role");
      this.$router.replace(this.$router.path);
      this.$router.push("/login");
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
  },
  async mounted() {
    if (this.$route.query.apptransid) {
      if (this.$route.query.status == 1) {
        this.emitter.emit("openToastMessage", "Thanh toán đơn hàng thành công");
        let orderId = this.$route.query.apptransid.slice(7);
        console.log(orderId);
        await apiUpdateOrder({
          order_id: orderId,
          status: 3,
        });
      } else {
        this.emitter.emit(
          "openToastMessageError",
          "Thanh toán không thành công vui lòng kiểm tra lại đơn hàng"
        );
      }
    }
    this.getOrderOfUser();
  },
};
</script>
<style>
@import url(./login.scss);
@import url(@/css/layout/table.scss);
</style>