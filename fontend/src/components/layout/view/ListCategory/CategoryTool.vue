<template>
  <div class="tool">
    <div style="display: flex; height: 100%; position: relative">
      <StyleInput
        :rightIcon="'search'"
        :placeholder="'Tìm kiếm nhãn dán'"
        v-model:value="searchText"
      ></StyleInput>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import CategoryForm from "./CategoryForm.vue";
export default {
  data() {
    return {
      searchText: null,
    };
  },
  components: { CategoryForm, StyleInput },
  methods: {
    /**
     * Mở form thêm tài sản
     * NTD 10/8/2022
     */
    openForm() {
      this.emitter.emit("addNewCategory");
    },
  },
  mounted() {},
  watch: {
    searchText() {
      this.emitter.emit("searchItemInList", this.searchText);
    },
  },
};
</script>
<style>
@import url(@/css/layout/page.scss);
@import url(@/css/base/button.scss);
@import url(@/css/base/combobox.scss);
@import url(@/css/base/input.css);
</style>