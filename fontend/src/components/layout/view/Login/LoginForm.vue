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
import ButtonMenu from "@/components/base/ButtonMenu.vue";
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";

export default {
  setup() {},
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
      this.$cookies.remove("token");
      await apiLogin(this.user)
        .then(async (response) => {
          if (response.data) {
            this.$cookies.set("token", response.data.token, {
              expires: 1 / 24,
            });
            this.$cookies.set("userName", response.data.user_name, {
              expires: 1 / 24,
            });
            this.$cookies.set("userId", response.data.user_id, {
              expires: 1 / 24,
            });
            this.$cookies.set("role", response.data.role, { expires: 1 / 24 });
          } else {
            this.emitter.emit(
              "openPopupNotice",
              "Thông tin đăng nhập không chính xác vui lòng nhập lại"
            );
          }
        })
        .catch(() => {
          this.emitter.emit(
            "openPopupNotice",
            "Thông tin đăng nhập không chính xác vui lòng nhập lại"
          );
        });

      this.emitter.emit("reloadRole");
      if (this.$cookies.get("token")) {
        this.$router.replace(this.$router.path);
        if (this.$cookies.get("role") == 0) {
          this.$router.push("/homepage");
        } else {
          this.$router.push("/dashboard");
        }
      }
    },
  },
  created() {},
};
</script>
<style>
@import url(./login.scss);
</style>