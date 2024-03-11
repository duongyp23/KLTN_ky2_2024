<template>
    <div class="menu" :class="{'open-menu': isSelect}" v-on:click="isSelect=true" >
        <div class="row-logo" >
            <div class="" title="Menu"></div>
            <div class="name"></div>
        </div>

        <MsButtonMenu :icon="'screen'" :label="'Tổng quan'" :routerPath="'/tongquan'"></MsButtonMenu>

        <MsButtonMenu :icon="'document'" :label="'Danh mục'" :icon2="'down'" :routerPath="'/dictionary'" :selectList="dictionaryList" :isOpen="isSelect"></MsButtonMenu>

        <MsButtonMenu :icon="'search'" :label="'Tra cứu'" :routerPath="'/search'"></MsButtonMenu>

        <MsButtonMenu :icon="'report'" :label="'Báo cáo'" :routerPath="'/report'"></MsButtonMenu>
      
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
            dictionaryList: [
                {
                    label: 'Sản phẩm',
                    router: '/product'
                },
                {
                    label: 'Nhãn dán',
                    router: '/label'
                },
                {
                    label: 'Đơn hàng',
                    router: '/order'
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
                    this.$router.push("dictionary")
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