<template>
  <div class="tool">
    <div style="display: flex; height: 100%; position: relative">
      <StyleInput
        :rightIcon="'search'"
        :placeholder="'Tìm kiếm sản phẩm'"
        v-model:value="searchText"
      ></StyleInput>
      <button class="btn-tool btn-add ml-1" @click="searchProduct">
        Tìm kiếm
      </button>
    </div>
    <div style="display: flex" v-if="isManager">
      <button class="btn-tool btn-add" @click="openForm">
        + Thêm sản phẩm
      </button>
    </div>
    <ProductForm></ProductForm>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import ProductForm from "./ProductForm.vue";
export default {
  data() {
    return {
      searchText: null,
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  components: { ProductForm, StyleInput },
  methods: {
    /**
     * Mở form thêm tài sản
     * NTD 10/8/2022
     */
    openForm() {
      this.emitter.emit("addNewProduct");
    },
    searchProduct() {
      this.emitter.emit("searchProduct", this.searchText);
    },
  },
  mounted() {},
  watch: {},
};
</script>
<style>
@import url(@/css/layout/page.scss);
@import url(@/css/base/button.scss);
@import url(@/css/base/combobox.scss);
@import url(@/css/base/input.css);
</style>