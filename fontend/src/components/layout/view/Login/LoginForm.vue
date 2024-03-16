<template>
  <div class="login-form">
    <div class="login-list-input">
      <label class="form-title">Đăng nhập</label>

      <StyleInput
        rightIcon="user"
        v-model:value="user.account"
        :label="'Tài khoản'"
        :placeholder="'Số điện thoại'"
      />
      <StyleInput
        rightIcon="lock"
        v-model:value="user.password"
        :label="'Mật khẩu'"
        :placeholder="'Mật khẩu'"
      />
      <button class="login-button" @click="login()">Đăng nhập</button>
      <div class="register">
        <ButtonMenu
          :label="'Đăng ký tài khoản'"
          :routerPath="'/register'"
        ></ButtonMenu>
      </div>
    </div>
  </div>
</template>
<script>
// import request from "@/api/request";
// import Resource from "@/resource/MsResource"
import { apiLogin } from "@/api/modules";
import Cookies from "js-cookie";
import ButtonMenu from "@/components/base/ButtonMenu.vue";
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";

export default {
  data() {
    return {
      typePassword: "password",
      check: false,
      user: {},
    };
  },
  components: { ButtonMenu, StyleInput },
  methods: {
    /**
     * Kiểm tra tên tài khaonr đã được nhập chưa
     * NTD 29/9/2022
     */
    checkAccount() {
      if (
        this.check == true &&
        (this.user.account == null || this.user.account == "")
      ) {
        return true;
      }
      return false;
    },
    /**
     * Kiểm tra mật khẩu đã được nhập chưa
     * NTD 29/9/2022
     */
    checkPassword() {
      if (
        this.check == true &&
        (this.user.password == null || this.user.password == "")
      ) {
        return true;
      }
      return false;
    },
    /**
     * Xử lý sự kiện khi nhấn đăng nhập
     * NTD 29/9/2022
     */
    async login() {
      await apiLogin(this.user)
        .then(async (response) => {
          Cookies.set("token", response.data, { expires: 1 / 24 });
          Cookies.set("userName", this.user.account, { expires: 1 / 24 });
        })
        .catch((response) => {
          console.log(response);
          this.user.account = null;
          this.user.password = null;
          this.check = true;
          this.$nextTick(() => this.$refs.inputAccount.focus());
        });

      if (Cookies.get("token") != null) {
        this.$router.replace(this.$router.path);
        this.$router.push("/taisan");
      }
    },
    /**
     * Hiển thị password
     * NTD 29/9/2022
     */
    changeTypePassword() {
      if (this.typePassword == "password") {
        this.typePassword = "text";
      } else {
        this.typePassword = "password";
      }
    },
  },
  created() {},
};
</script>
<style>
@import url(./login.scss);
</style>