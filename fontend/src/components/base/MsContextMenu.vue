<template>
    <div class="context-menu" v-if="show" :style="style" ref="context" tabindex="0" @blur="close" v-click-outside="close">
        <div class="context-menu-option" @click.stop="addNewItem(), show = false">
            <div class="icon icon-add" style="margin: 8px;"></div>
            Thêm tài sản
        </div>
        <div class="context-menu-option" @click.stop="$emit('fixThisItem'), show = false">
            <div class="icon icon-pen" style="margin: 8px;"></div>
            Sửa tài sản
        </div>
        <div class="context-menu-option" @click.stop="$emit('deleteThisItem'), show = false">
            <div class="icon icon-trash-black" style="margin: 8px;"></div>
            Xóa tài sản
        </div>
        <div class="context-menu-option" @click.stop="$emit('createCopy'), show = false">
            <div class="icon icon-copy" style="margin: 8px;"></div>
            Nhân bản tài sản
        </div>
    </div>
</template>
<script>
export default {
   
    props: {
        display: Boolean, // prop detect if we should show context menu
    },
    data() {
        return {
            left: 0, // left position
            top: 0, // top position
            show: false, // affect display of context menu
        };
    },
    computed: {
        // get position of context menu
        /**
         * Thiết lập style cho context menu
         * 1/9/2022
         */
        style() {
            return {
                top: this.top + 'px',
                left: this.left + 'px',
            };
        },
    },
    methods: { 
        /**
         * Đóng context menu
         * NTD 1/9/2022
         */
        close() {
            this.show = false;
            this.left = 0;
            this.top = 0;
        },
        /**
         * Mở context menu
         *  NTD 1/9/2022
         */
        open(evt) {
            this.emitter.emit('closeOrtherContextMenu')
            // updates position of context menu 
            this.left = evt.pageX || evt.clientX;
            this.top = evt.pageY || evt.clientY;
            // make element focused 
            // @ts-ignore
            
            this.show = true;
         
        },
        /**
         * Gửi lệnh mở form thêm mới
         * NTD 1/9/2022
         */
        addNewItem() {
            this.emitter.emit('openForm')
        }
    },
    mounted() {
        /**
         * Nhận lệnh đóng context menu
         * NTD 1/9/2022
         */
        this.emitter.on('closeOrtherContextMenu', () => {
            this.close()
        })
    }
}
</script>
<style>
@import url(../../css/base/contextmenu.css);
@import url(../../css/base/icon.css);
</style>