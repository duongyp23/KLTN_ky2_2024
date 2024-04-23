<template>
  <div class="tool">
    <div style="display: flex; height: 100%; position: relative">
      <StyleInput
        :rightIcon="'search'"
        :placeholder="'Tìm kiếm sản phẩm'"
        v-model:value="searchText"
        @change="searchProduct"
      ></StyleInput>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
export default {
  data() {
    return {
      searchText: null,
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  components: { StyleInput },
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