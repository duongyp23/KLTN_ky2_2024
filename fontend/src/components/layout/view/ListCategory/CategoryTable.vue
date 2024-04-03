<template>
  <div class="category-table">
    <div
      class="group-category-type"
      v-for="item in CategoryType"
      :key="item.id"
    >
      <div class="type-name">{{ item.name }}</div>
      <hr />
      <div class="list-category">
        <div
          v-for="category in datalist.filter((x) => x.type == item.id)"
          :key="category.category_id"
        >
          <button
            :class="
              selectCategory.find((x) => x.category_id == category.category_id)
                ? 'category category-select'
                : 'category category-not-select'
            "
            @click="changeCategory(category)"
            @dblclick="openFormEdit(category)"
          >
            {{ category.category_code }}
          </button>
        </div>
      </div>
    </div>
    <div class="flex-row center mt-1" v-if="showButton && isManager">
      <button class="form-btn btn1" @click="searchProductByCategory">
        Tìm kiếm
      </button>
      <button class="form-btn btn3" @click="openForm">+ Thêm nhãn dán</button>
    </div>
    <CategoryForm></CategoryForm>
  </div>
</template>
<script>
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import Resource from "@/resource/MsResource";
import { apiGetAllCategory } from "@/api/categoryApi";
import CategoryType from "@/resource/CategoryType";
import CategoryForm from "./CategoryForm.vue";

export default {
  data() {
    return {
      keyword: null, // từ cần lọc
      datalist: [], // danh sách tài sản
      CategoryType,
      isManager: this.$cookies.get("role") == 1 ? true : false,
      selectCategory: [],
    };
  },
  props: {
    showButton: {
      default: false,
      type: Boolean,
    },
  },
  components: { CategoryForm },
  methods: {
    openForm() {
      this.emitter.emit("addNewCategory");
    },
    changeCategory(category) {
      var index = this.selectCategory.indexOf(category);
      if (index > -1) {
        this.selectCategory.splice(index, 1);
      } else {
        this.selectCategory.push(category);
      }
    },
    /**
     * Lấy dữ liệu từ backend
     */
    async getNewData() {
      this.isLoader = true;
      this.datalist = [];
      let filter = [];
      await apiGetAllCategory(filter)
        .then((response) => {
          this.datalist = response.data;
        })
        .catch(() => {
          this.emitter.emit(
            "openPopupNotice",
            Resource.PopupNotice.ErrorGetPaging
          );
        });
    },
    openFormEdit(category) {
      if (this.isManager && this.showButton) {
        this.emitter.emit("updateCategory", category);
      }
    },
  },

  mounted() {
    /***
     * load lại page
     * NTD 5/9/2022
     */
    this.emitter.on("loadDataCategory", () => {
      this.getNewData();
    });
  },
  created() {
    /***
     * Lấy dữ liệu khi tạo component
     * NTD 25/8/2022
     */
    this.getNewData();
  },
  watch: {},
};
</script>
<style>
@import url(./categoryTable.scss);
</style>