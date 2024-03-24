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
    <div class="login-list-input">
      <label class="form-title">Danh sách đơn hàng</label>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import { apiUpdateUserInfo, apiGetInfoUser } from "@/api/userApi";
export default {
  data() {
    return {
      typePassword: "password",
      check: false,
      user: {},
    };
  },
  components: { StyleInput },
  methods: {
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
    logout() {
      this.$cookies.remove("token");
      this.$router.replace(this.$router.path);
      this.$router.push("/homepage");
    },
  },
  created() {
    this.getUserInfo();
  },
};
</script>
<style>
@import url(./login.scss);
</style>