<template>
  <div class="tool">
    <div style="display: flex; height: 100%; position: relative">
      <div class="input-tool">
        <div
          class="icon icon-search-input"
          style="margin-left: 10px; position: absolute"
        ></div>
        <input
          type="text"
          class="input-box"
          style="margin-left: 39px"
          placeholder="Tìm kiếm nhãn dán"
          @change="searchItem($event.target.value)"
          ref="inputSearch"
          @input="inputNullValue($event.target.value)"
        />
      </div>
      <MsCombobox
        style="margin-left: 11px; width: 219px"
        :items="categoryType"
        v-model:value="categoryTypeName"
        :placeholder="'Loại nhãn dán'"
        :icon="'select'"
        v-model:id="categoryTypeId"
      ></MsCombobox>
    </div>
    <div style="display: flex">
      <button class="btn-tool btn-add" @click="openForm">
        + Thêm nhãn dán
      </button>
    </div>
    <CategoryForm></CategoryForm>
  </div>
</template>
<script>
import MsCombobox from "@/components/base/MsCombobox.vue";
import CategoryForm from "./CategoryForm.vue";
import CategoryType from "@/resource/CategoryType";
export default {
  data() {
    return {
      categoryTypeId: null, // ID Loại nhãn dán
      categoryTypeName: "", // tên Loại nhãn dán
      categoryType: CategoryType,
    };
  },
  components: { MsCombobox, CategoryForm },
  methods: {
    /**
     * Khi input null thì load lại data
     * NTd 5/10/2022
     */
    inputNullValue(value) {
      if (value == null || value == "") {
        this.emitter.emit("searchItemInList", value);
      }
    },
    /**
     * Mở form thêm tài sản
     * NTD 10/8/2022
     */
    openForm() {
      this.emitter.emit("addNewCategory");
    },
    /**
     * Tìm kiếm tài sản
     * NTD 14/8/2022
     */
    searchItem(value) {
      this.emitter.emit("searchItemInList", value);
    },
  },
  mounted() {},
  watch: {
    selectList() {},
    /**
     * Lọc tài sản theo loại
     * NTD 5/9/2022
     */
    propertyTypeID() {
      this.emitter.emit("sortType", this.propertyTypeID);
    },
  },
};
</script>
<style>
@import url(@/css/layout/page.scss);
@import url(@/css/base/button.scss);
@import url(@/css/base/combobox.css);
@import url(@/css/base/input.css);
</style>