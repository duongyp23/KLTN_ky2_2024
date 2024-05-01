<template>
  <div class="category-form form-view" v-show="isOpen" @keyup.esc="closeForm()">
    <div class="form">
      <div class="form-header">
        <div>{{ label }}</div>
        <button
          class="btn-header"
          style="border: none; background: transparent"
          title="Đóng"
          @click="closeForm()"
        >
          <div class="btn-close">X</div>
        </button>
      </div>
      <div class="form-center col-flex">
        <div class="row-flex">
          <StyleInput
            :label="'Mã nhãn dán'"
            v-model:value="category.category_code"
          ></StyleInput>
          <div class="style-input col-flex">
            <label class="Form-Label">Loại nhãn dán</label>
            <MsCombobox
              :items="categoryType"
              v-model:value="category.type_name"
              v-model:id="category.type"
            ></MsCombobox>
          </div>
        </div>
        <StyleInput
          class="description"
          :label="'Diễn giải'"
          v-model:value="category.description"
          type="textarea"
        ></StyleInput>
      </div>
      <div class="form-footer">
        <button class="form-btn btn1" v-on:click="closeForm">Hủy</button>
        <button
          class="form-btn btn2"
          v-on:click="deleteCategory"
          v-if="formStatus == 2"
        >
          Xóa
        </button>
        <button class="form-btn btn3" @click="saveForm">Lưu</button>
      </div>
    </div>
  </div>
</template>
<script>
import Resource from "@/resource/MsResource";
import {
  apiInsertCategory,
  apiUpdateCategory,
  apiDeleteCategory,
} from "@/api/categoryApi";
import MsCombobox from "@/components/base/MsCombobox.vue";
import CategoryType from "@/resource/CategoryType";
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";

/**
 * Khởi tạo 1 Item với giá trị ban đầu là null
 * NTD 22/8/2022
 */
export default {
  data() {
    return {
      formStatus: 0,
      isOpen: false, // hiển thị
      label: "", // tiêu đề
      categoryType: CategoryType,
      category: {},
    };
  },
  methods: {
    async deleteCategory() {
      await apiDeleteCategory(this.category.category_id).then(() => {
        this.emitter.emit("loadDataCategory");
        this.closeForm();
      });
    },
    closeForm() {
      this.isOpen = false;
    },
    /**
     *  Thay đổi tiêu đề form
     *  NTD 8/8/2022
     */
    setLabel(newLabel) {
      this.label = newLabel;
    },

    /**
     * Lưu form, kiểm tra các trường đã được nhập dữ liệu hay chưa khi click
     */
    saveForm() {
      if (this.formStatus == 1) {
        this.addNewCategory();
      } else if (this.formStatus == 2) {
        this.updateCategory();
      }
    },

    /**
     * Gọi API thêm mới tài sản
     * NYD 5/9/2022
     */
    async addNewCategory() {
      await apiInsertCategory(this.category)
        .then(() => {
          this.emitter.emit(
            "openToastMessage",
            Resource.SuccessMessage.SuccessMessageAdd
          );
          this.emitter.emit("loadDataCategory");
          this.isOpen = false;
        })
        .catch(() => {
          this.emitter.emit(
            "openToastMessageError",
            Resource.ErrorMessage.ErrorMessageAdd
          );
        });
    },
    /**
     * gọi API Sửa tài sản
     * NTD 5/9/2022
     */
    async updateCategory() {
      await apiUpdateCategory(this.category)
        .then(() => {
          this.emitter.emit(
            "openToastMessage",
            Resource.SuccessMessage.SuccessMessageAdd
          );
          this.emitter.emit("loadDataCategory");
          this.isOpen = false;
        })
        .catch(() => {
          this.emitter.emit(
            "openToastMessageError",
            Resource.ErrorMessage.ErrorMessageAdd
          );
        });
    },
  },
  components: { MsCombobox, StyleInput },
  mounted() {
    /**
     * Mở form thêm tài sản từ Tool
     * NTD 8/8/2022
     */
    this.emitter.on("addNewCategory", async () => {
      this.formStatus = 1;
      this.category = {};
      this.setLabel("Thêm nhãn dán");
      this.isOpen = true;
    });
    /**
     * Mở form sửa tài sản từ Row trong table
     * NTD 8/8/2022
     */
    this.emitter.on("updateCategory", async (category) => {
      this.category = Object.assign({}, category);
      this.formStatus = 2;
      this.setLabel("Sửa nhãn dán");
      this.isOpen = true;
    });
  },
  watch: {},
};
</script>

<style>
@import url(./categoryForm.scss);
@import url(@/css/layout/form.scss);
</style>