<template>
  <p>
    Input:
    <input
      type="text"
      @keyup.enter="fetchOutput"
      v-model="inputValue"
      placeholder="Enter a number"
    />
    &nbsp;
    <button type="button" @click="fetchOutput" :disabled="invalidData">
      Convert
    </button>
  </p>
  <p>Output: {{ outputValue }}</p>
</template>

<script>
export default {
  name: "NumberConversionApp",

  data() {
    return {
      inputValue: "",
      outputValue: "",
      invalidData: true,
    };
  },

  watch: {
    inputValue(newValue) {
      this.invalidData = !newValue || newValue.trim() === "";
    },
  },

  methods: {
    async fetchOutput() {
      if (this.invalidData) {
        this.outputValue = "A number is required.";
        return;
      }

      try {
        const encodedInputValue = encodeURIComponent(this.inputValue);
        const apiEndPoint = `https://localhost:12346/NumberConversion/${encodedInputValue}`;
        const response = await fetch(apiEndPoint);

        if (!response.ok) {
          const errorText = await response.json();
          throw new Error(
            errorText.detail ?? `Request failed with status ${response.status}`,
          );
        }

        this.outputValue = await response.text();
      } catch (error) {
        this.outputValue = error.message;
      }
    },
  },
};
</script>
