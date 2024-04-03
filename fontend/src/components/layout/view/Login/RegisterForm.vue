<template>
  <div class="login-form">
    <div class="login-list-input">
      <label class="form-title">Đăng ký tài khoản</label>
      <StyleInput
        rightIcon="user"
        v-model:value="user.user_name"
        :label="'Họ và tên'"
      />
      <StyleInput
        rightIcon="phone"
        v-model:value="user.phone_number"
        :label="'Số điện thoại'"
      />
      <StyleInput
        rightIcon="home"
        v-model:value="user.user_address"
        :label="'Địa chỉ'"
      />
      <StyleInput
        rightIcon="lock"
        v-model:value="user.password"
        :label="'Mật khẩu'"
      />
      <button class="login-button" @click="register()">
        Đăng ký tài khoản
      </button>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import { apiRegister } from "@/api/userApi";
export default {
  data() {
    return {
      typePassword: "password",
      check: false,
      user: {
        account: null,
        password: null,
      },
    };
  },
  components: { StyleInput },
  methods: {
    async register() {
      await apiRegister(this.user)
        .then(async (response) => {
          this.$cookies.set("token", response.data.token, { expires: 1 / 24 });
          this.$cookies.set("userName", response.data.user_name, {
            expires: 1 / 24,
          });
          this.$cookies.set("userId", response.data.user_id, {
            expires: 1 / 24,
          });
          this.$cookies.set("role", response.data.role, { expires: 1 / 24 });
        })
        .catch(() => {});
      this.emitter.emit("reloadRole");
      if (this.$cookies.get("token") != null) {
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