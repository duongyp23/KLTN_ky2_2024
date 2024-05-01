<template>
  <div class="manage-category">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Danh sách nhãn dán</div>
      <button class="form-btn btn3" @click="openFormAdd">Thêm nhãn dán</button>
    </div>
    <table class="table">
      <thead>
        <tr>
          <th class="left w-20">Mã nhãn dán</th>
          <th class="left w-20">Loại nhãn dán</th>
          <th class="left">Diễn giải</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="item in listData"
          :key="item.category_id"
          @dblclick="openFormEdit(item)"
        >
          <td class="left w-20">{{ item.category_code }}</td>
          <td class="left w-20">{{ getCategoryTypeName(item.type) }}</td>
          <td class="left">{{ item.description }}</td>
        </tr>
      </tbody>
    </table>
    <CategoryForm></CategoryForm>
  </div>
</template>
<script>
import { apiGetAllCategory } from "@/api/categoryApi";
import { getCategoryTypeName } from "@/method/methodFormat";
import CategoryForm from "../ManageCategory/CategoryForm.vue";
export default {
  data() {
    return {
      listData: [],
      pagaSize: 20,
      pageNumber: 1,
    };
  },
  components: { CategoryForm },
  methods: {
    openFormAdd() {
      this.emitter.emit("addNewCategory");
    },
    getCategoryTypeName,
    async getData() {
      await apiGetAllCategory([]).then((response) => {
        this.listData = response.data;
      });
    },
    openFormEdit(category) {
      this.emitter.emit("updateCategory", category);
    },
  },
  created() {
    this.getData();
  },
  mounted() {
    this.emitter.on("loadDataCategory", () => {
      this.getData();
    });
  },
};
</script>
<style>
@import url(./ManageCategory.scss);
@import url(@/css/layout/table.scss);
</style>