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
import { apiLogin } from "@/api/userApi";
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
     * Xử lý sự kiện khi nhấn đăng nhập
     */
    async login() {
      await apiLogin(this.user)
        .then(async (response) => {
          Cookies.set("token", response.data.token, { expires: 1 / 24 });
          Cookies.set("userName", response.data.user_name, { expires: 1 / 24 });
          Cookies.set("role", response.data.role, { expires: 1 / 24 });
        })
        .catch(() => {});

      if (Cookies.get("token") != null) {
        this.$router.replace(this.$router.path);
        this.$router.push("/homepage");
      }
    },
  },
  created() {},
};
</script>
<style>
@import url(./login.scss);
</style>