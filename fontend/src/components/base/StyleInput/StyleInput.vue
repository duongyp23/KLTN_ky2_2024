<template>
  <div class="style-input">
    <div v-if="label != null" class="input-label">{{ label }}</div>
    <div class="row-input">
      <div
        v-if="rightIcon != null"
        :class="'icon-input'"
        :style="'background-image : url(' + Images[rightIcon] + ')'"
      ></div>
      <input
        :type="type"
        class="input-box"
        :value="value"
        :placeholder="[placeholder ? placeholder : label ? label : '']"
        @input="$emit('update:value', $event.target.value)"
      />
    </div>
  </div>
</template>
<script>
import Images from "@/assets/icon/images";
export default {
  data() {
    return {
      Images,
    };
  },
  props: {
    placeholder: { type: String }, // placeholder
    label: { type: String },
    type: { default: "text", type: String },
    value: { default: null, type: String }, // giá trị input
    rightIcon: { type: String }, // icon
    id: { default: null, type: String }, // ID
  },
  computed: {},
  methods: {},
  watch: {},
  mounted() {},
  created() {},
};
</script>
<style>
@import url(./styleInput.scss);
</style>