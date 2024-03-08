<template>
    <input type="text" class="form-input input-number" ref="inputNumber" @input="inputValue()">
</template>
<script>
import Resource from "@/resource/MsResource";
import AutoNumeric from 'autonumeric';
export default ({
    data() {
        return {
            InputNumber: null,
        }
    },
    props: {
        numberValue: {default: 0, type:Number}
    },
    methods: {
        inputValue(){
            const rawValueText = this.InputNumber.rawValue;
            const valueNumber = Number(rawValueText);
            this.$emit('update:numberValue', valueNumber)
            this.$emit('changeValue')
        },
        setValue() {
            this.InputNumber.set(this.numberValue)
        }
    },
    mounted() {
        
        this.InputNumber = new AutoNumeric(this.$refs.inputNumber, {
            decimalCharacter: Resource.DecimalCharacter,
            digitGroupSeparator : Resource.DigitGroupSeparator,
            decimalPlaces:'2',
            minimumValue:'0',
            allowDecimalPadding: false,
            maximumValue:'999999999999999999.99'
        })
        this.setValue()
    },
    watch: {
        numberValue() {
            this.InputNumber.set(this.numberValue)
        }
    },
    created() {
        
    }
})
</script>
<style>
@import url(@/css/base/input.css);
@import url(@/css/layout/form.css);
</style>