<template>
    <div class="menu" :class="{'open-menu': isSelect}" v-on:click="isSelect=true" >
        <div class="row-logo" >
            <div class="logo" title="Menu"></div>
            <div class="name">MISA QLTS</div>
        </div>

        <MsButtonMenu :icon="'screen'" :label="'Tổng quan'" :routerPath="'/tongquan'"></MsButtonMenu>

        <MsButtonMenu :icon="'car'" :label="'Tài sản'" :icon2="'down'" :routerPath="'/taisan'" :selectList="selectList" :isOpen="isSelect"></MsButtonMenu>

        <MsButtonMenu :icon="'road'" :label="'Tài sản HT-DB'" :icon2="'down'" :routerPath="'/taisanht-db'"></MsButtonMenu>
    
        <MsButtonMenu :icon="'tool'" :label="'Công cụ dụng cụ '" :icon2="'down'" :routerPath="'/congcu'"></MsButtonMenu>

        <MsButtonMenu :icon="'document'" :label="'Danh mục'" :routerPath="'/danhmuc'"></MsButtonMenu>

        <MsButtonMenu :icon="'search'" :label="'Tra cứu'" :icon2="'down'" :routerPath="'/tracuu'"></MsButtonMenu>

        <MsButtonMenu :icon="'report'" :label="'Báo cáo'" :icon2="'down'" :routerPath="'/baocao'"></MsButtonMenu>
      
        <div class="menu-footer">
            <hr style="opacity: .5">
            <button class="btn-header" style="border:1px solid #fff;background-color: transparent;position: relative;left:160px;border-radius: 4px" v-on:click.stop="isSelect = false" title="Đóng" >
                <div class="icon icon-prev-white"></div>
            </button>
        </div>
    </div>
</template>
<script>
import MsButtonMenu from '../base/MsButtonMenu.vue';
import Cookies from 'js-cookie';
export default {
 
    data() {
        return {
            isSelect: false, // Mở rộng menu
            selectList: [
                {
                    label: 'Ghi tăng',
                    router: '/ghitang'
                },
                {
                    label: 'Thay đổi thông tin',
                    router: '/thayđoithongtin'
                },
                {
                    label: 'Đánh giá lại',
                    router: '/đanhgialai'
                },
                {
                    label: 'Tính hao mòn',
                    router: '/tinhhaomon'
                },
                {
                    label: 'Điều chuyển tài sản',
                    router: '/đieuchuyentaisan'
                },
                {
                    label: 'Ghi giảm',
                    router: '/ghigiam'
                },
                {
                    label: 'Kiểm kê',
                    router: '/kiemke'
                },
                {
                    label: 'Khác',
                    router: '/khac'
                },

            ]
        }
    },
    components: { MsButtonMenu },
    created() {
    },
    watch: {
        /**
         * Kiểm tra sự thay đổi router và check cookies
         * NTD 29/9/2022
         */
        async $route() {
            if(this.$route.path != "/login") {
                if(Cookies.get('token') == null) {
                    this.$router.replace(this.$route.path)
                    this.$router.push("/login")
                } else if(this.$route.path == "/") {
                    this.$router.push("taisan")
                }
            }
        }
    }
}
</script>
<style>
@import url(../../css/layout/page.css);
@import url(../../css/layout/menu.css); 
</style>