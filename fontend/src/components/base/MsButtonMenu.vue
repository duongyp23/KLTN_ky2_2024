<template>
    <router-link class="link" :to="routerPath" @click="selectRow">
        <button class="btn-menu"  :class="{'selected': isSelected}" >
            <div class="btn-icon" :title="label">
                    <div :class="[{'icon':true}, icon ? 'icon-'+icon:'']"></div>
            </div>                    
            <div class="btn-menu-name" style="padding-left:14px">{{label}}</div>
            <div :class="[{'icon':true}, icon2 ? 'icon-'+icon2:'']"></div>
        </button>
        <router-link v-for="selection in selectList" :key="selection" class="link" :to="routerPath + selection.router" v-show="isSelected && isOpen" >
            <button class="btn-menu-selection" :class="{'selected': checkRouter(selection.router)}">
                <div class="icon icon-arrow" style="position:absolute;  left: 25px; rotate:3.142rad;"></div>
                {{selection.label}}
            </button>

        </router-link>
    </router-link>
    
</template>
<script>
export default {
    data() {
        return {
        isSelected: false // hiển thị
        }   
    },
    props: {
        routerPath: {type:String, default: "/"}, // routerlink
        icon: {type:String}, // icon hiển thị
        icon2: { type:String}, // icon xuống
        label: {required: true, type:String}, // tiêu đề
        selectList: {type:Array},
        isOpen: {type:Boolean}
    },
    computed: {
        
    },
    methods: {
        /**
         * Bỏ chọn tất cả menu rồi chọn chính nó
         * NTD 11/8/2022
         */
        selectRow() {
            
            if(this.isSelected == false ) {
                this.emitter.emit('removeSelectMenu')
                this.isSelected = !this.isSelected
            }
            
        },
        /**
         * 
         */
        checkRouter(routerPath) {
            if(this.$route.path.includes(routerPath)){
                return true
            }
            return false
        },
    },
    mounted() {
        /**
         * gọi funcion bỏ chọn tất cả menu ở menu
         * NTD 11/8/2022
         */
        this.emitter.on('removeSelectMenu',() => {
            this.isSelected = false
        })
    },
    watch: {
        /**
         * Kiểm tra khi router thay đổi
         * NTD 15/9/2022
         */
        $route () {
            if(this.$route.path.includes(this.routerPath)){
                this.selectRow()
            }
        }
    }
}
</script>
<style>

@import url(../../css/base/button.css);
@import url(../../css/layout/menu.css);
@import url(../../css/base/icon.css);
</style>