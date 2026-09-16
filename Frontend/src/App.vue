<template>
  <div class="row">
    <div class="col-1"><label class="form-label fw-bold">Input:</label></div>
    <div class="col-6">
      <input
        type="text"
        @keyup.enter="fetchOutput"
        v-model="inputValue"
        placeholder="Enter a number and then press Enter or click Convert"
        class="form-control"
      />
    </div>
    <div class="col">
      <button type="button" class="btn btn-success" @click="fetchOutput" :disabled="invalidData">
        Convert
      </button>
    </div>
  </div>
  <div class="row">
    <div class="col-1"><label class="form-label fw-bold">Output:</label></div>
    <div class="col-11" :class="{ 'text-danger': errorOccurred }">
      {{ outputValue }}
    </div>
  </div>
</template>

<script>
export default {
  name: "NumberConversionApp",

  data() {
    return {
      inputValue: "",
      outputValue: "",
      invalidData: true,
      errorOccurred: false,
    };
  },

  watch: {
    inputValue(newValue) {
      this.invalidData = !newValue || newValue.trim() === "";
    },
  },

  methods: {
    async fetchOutput() {
      this.errorOccurred = true;
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
        this.errorOccurred = false;
      } catch (error) {
        this.outputValue = error.message;
      }
    },
  },
};
</script>
