<template>
    <div class="login">
        <div class="login-form">
            <div class="login-form-left"></div>
            <div class="login-form-right">
                <div class="login-list-input">
                  <div class="login-logo"></div>
                  <label class="login-title">Đăng nhập để làm việc với <b>MISA QLTS</b></label>
                  <input type="text" class="login-input" placeholder="Username, email hoặc số điện thoại" :class="{'error': checkAccount()}" v-model="user.account" ref="inputAccount">
                  <div style="width: 100%; position: relative;">
                    <input :type="typePassword" class="login-input" placeholder="Mật khẩu" :class="{'error': checkPassword()}" v-model="user.password" ref="inputPassword"> 
                    <div class="password-icon icon" :class="[typePassword =='password' ? 'icon-eye' : 'icon-eye2']" @click="changeTypePassword()"></div>
                  </div>
                  <button class="login-button" @click="login()">Đăng nhập</button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
// import request from "@/api/request";
// import Resource from "@/resource/MsResource"
import { apiLogin } from "@/api/modules";
import Cookies from "js-cookie";

export default ({
    data() {
      return {
        typePassword: 'password',
        check : false,
        user: {
          account: null,
          password: null,
        },
      }  
    },
    methods: {
      /**
       * Kiểm tra tên tài khaonr đã được nhập chưa
       * NTD 29/9/2022
       */
      checkAccount() {
        if(this.check == true && (this.user.account == null || this.user.account == "")) {
          return true;
        }
        return false;
      },
      /**
       * Kiểm tra mật khẩu đã được nhập chưa
       * NTD 29/9/2022
       */
      checkPassword() {
        if(this.check == true && (this.user.password == null || this.user.password == "")) {
          return true;
        }
        return false;
      },
      /**
       * Xử lý sự kiện khi nhấn đăng nhập
       * NTD 29/9/2022
       */
      async login() {
        this.check = false
        if(this.user.account == null || this.user.account == "") {
          this.check = true
          this.$nextTick(() => this.$refs.inputAccount.focus())
        } else if (this.user.password == null || this.user.password == ""){
          this.check = true
          this.$nextTick(() => this.$refs.inputPassword.focus())
        }
        if(this.check == false) {
          await apiLogin(this.user)
          .then(async response => {
            Cookies.set('token', response.data, {expires: 1/24})
            Cookies.set('userName', this.user.account, {expires: 1/24})
          })
          .catch(response => {
            console.log(response);
            this.user.account = null
            this.user.password = null
            this.check = true
            this.$nextTick(() => this.$refs.inputAccount.focus())
          })
        }
        
        if(Cookies.get('token') != null) {
          this.$router.replace(this.$router.path)
          this.$router.push("/taisan")
        }
      },
      /**
       * Hiển thị password
       * NTD 29/9/2022
       */
      changeTypePassword() {
        if(this.typePassword == 'password') {
          this.typePassword = 'text'
        } else {
          this.typePassword = 'password'
        }
      }
    },
    created() {
    }
})
</script>
<style>
@import url(@/css/layout/login.css);
</style>