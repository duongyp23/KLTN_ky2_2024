<template>
    <div class="header">
        <div style="font-weight: 700;white-space: nowrap;">Danh sách tài sản</div>
        <div style="display: flex;align-items: center;">
            <div style="font-weight: 500;color: #001031;white-space: nowrap;">Sở tài chính</div>
            <div class="select-year-header">
                <label style="margin-left:12px;font-size: 13px;">Năm</label>
                <div style="font-size: 13px;font-weight:700; margin-left: 13px;">{{count}}</div>
                <div style="margin-left:13px;display:flex;flex-direction:column;justify-content: space-between;row-gap: 3px;">
                    <div class="icon icon-mini-up" @click="count++"></div>
                    <div class="icon icon-mini-down" @click="count--"></div>
                </div>
            </div>
            <button class="btn-header icon icon-bell" style="margin-left:27px" title="Thông báo"></button>
            <button class="btn-header icon icon-window"  style="margin-left:20px" title="Menu"></button>
            <button class="btn-header icon icon-question" style="margin-left:20px" title="Câu hỏi"></button>
            <button class="btn-header icon icon-avartar"  style="margin-left:20px; position: relative;" title="Tài khoản" @click="isOpen = !isOpen" v-click-outside="close">
                <div class="box-user" v-show="isOpen">
                    {{userName}}
                    <button class="btn-logout" @click="logout()">Đăng xuất</button>
                </div>
            </button>
            <button class="btn-mini icon icon-mini-down"  style="margin-left:8px"></button>
        </div>
    </div>
</template>
<script>
import Cookies from 'js-cookie'
export default {
    data() {
        return {
            userName:  Cookies.get('userName'),
            isOpen: false,
            count: 2022 // mặc định năm
        }
    },
    methods: {
        /**
         * Đăng xuất
         * NTD 29/9/2022
         */
        logout() {
            this.$cookies.remove('token')
            this.$router.replace(this.$route.path)
            this.$router.push("/login")
        },
        /**
         * Đóng cửa sổ thông tin user
         * NTD 2/10/2022
         */
        close() {
            this.isOpen = false
        }
    }
}
</script>
<style>
@import url(../../css/layout/page.css);
@import url(../../css/base/select.css);
@import url(../../css/base/icon.css);
@import url(../../css/base/button.css);
@import url(../../css/base/input.css);

</style>