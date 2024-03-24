<template>
  <div class="category-table">
    <div style="display: flex" v-if="isManager">
      <button class="btn-tool btn-add" @click="openForm">
        + Thêm nhãn dán
      </button>
    </div>
    <div
      class="group-category-type"
      v-for="item in CategoryType"
      :key="item.id"
    >
      <div class="type-name">{{ item.name }}</div>
      <div class="list-category">
        <div v-for="category in datalist" :key="category.category_id">
          <button
            v-if="category.type == item.id"
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

      <hr />
    </div>
    <div class="search">
      <button class="btn-tool btn-search" @click="openForm">Tìm kiếm</button>
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
  components: { CategoryForm },
  methods: {
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
      if (this.keyword != null) {
        filter.push({
          columnName: "category_code",
          filterValue: this.keyword,
          operatorValue: "=",
        });
      }
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
      if (this.isManager) {
        this.emitter.emit("updateCategory", category);
      }
    },
  },

  mounted() {
    /**
     * Lọc tài sản theo tên
     * NTD 14/8/2022
     */
    this.emitter.on("searchItemInList", (valueSearch) => {
      this.keyword = valueSearch;
    }),
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
  watch: {
    /***
     * Kiểm tra thay đổi giá trị để phân trang
     * NTD 5/9/2022
     */
    keyword() {
      if (this.pageNumber != 1) {
        this.pageNumber = 1;
      } else {
        this.getNewData();
      }
    },
  },
};
</script>
<style>
@import url(./categoryTable.scss);
</style>