<template>
    <div class="tool">
        <label class="tool-label">Ghi tăng tài sản</label>
        <div style="display:flex;">
            <router-link class="link" to="/taisan/ghitang/newForm">
                <button class="btn-tool btn-add" style="width: 80px">Thêm</button>
            </router-link>
            <button class="btn-tool select-view" @click="showSelection = true" v-click-outside="closeSelection" title="Giao diện">
                <div class="icon icon-voucher-view"></div>
                <div class="icon icon-down-black"></div>
                <div class="selection-view" v-show="showSelection" >
                    <div class="selection" @click="$emit('update:sizeViewProperty', 0); showSelection = false" >
                        <div class="icon icon-voucher-view2" style="margin: auto 0px;"></div>
                        Giao diện dọc
                    </div>
                    <div class="selection" @click="$emit('update:sizeViewProperty', 30); showSelection = false">
                        <div class="icon icon-voucher-view" style="margin: auto 0px;"></div>
                        Giao diện ngang
                    </div>
                </div>
            </button>
        </div>
    </div>
    <router-view></router-view>
</template>
<script>

export default ({
    data() {
       return{
            showSelection: false,
       } 
    },
    props: {
        sizeViewProperty: {default: 30, type:Number},
    },
    components: {
        
    },
    methods: {
        closeSelection() {
            this.showSelection = false
        }
    }
})
</script>
<style>
@import url(@/css/layout/page.css);
@import url(@/css/layout/tool.css);
@import url(@/css/base/button.css);
</style>
