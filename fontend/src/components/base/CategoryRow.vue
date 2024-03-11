<template>
  <tr
    @click.exact="selectOnlyRow()"
    :class="{ selectRow: isSelect }"
    @contextmenu.prevent
    @dblclick="openFormFix()"
  >
    <td class="col-stt">{{ stt }}</td>
    <td class="col-code">{{ item.category_code }}</td>
    <td class="col-name">{{ item.category_name }}</td>
    <td class="col-type" v-show="showInfo">{{ item.type_name }}</td>
    <td class="col-description">{{ item.description }}</td>
    <td class="col-fix" v-show="showFix">
      <button class="btn-icon-table" @click.stop="openFormFix()" title="Sửa">
        <div class="icon icon-pen"></div>
      </button>
      <button class="btn-icon-table" title="Xóa" @click.stop="deleteItem()">
        <div class="icon icon-trash"></div>
      </button>
    </td>
  </tr>
</template>
<script>
import { selectOnlyRow, openFormFix, deleteItem } from "@/method/methodRow";
export default {
  data() {
    return {
      isSelect: false, // dòng được chọn
    };
  },
  props: {
    item: {}, // tài sản
    stt: {}, // số thứ tự
    showInfo: { default: true, type: Boolean },
    showFix: { default: true }, // mở context menu
  },
  methods: {
    /**
     * Sự kiện khi bấm vào dong. chỉ chọn mỗi dòng ấy
     * NTD 5/10/2022
     */
    selectOnlyRow,

    /**
     * Mở form chỉnh sửa từ button trong row
     * NTD 8/8/2022
     */
    openFormFix,
    /**
     * send lệnh xóa item
     * NTD 14/8/2022
     */
    deleteItem,
  },
  mounted() {},
  created() {},
  watch: {},
};
</script>
<style>
@import url(../../css/layout/table.css);
</style>