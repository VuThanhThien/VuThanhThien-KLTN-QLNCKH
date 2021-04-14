<template>
  <div id="app" class="container">
    <div class="bodyApp">
      <img id="image" src="@/assets/images/bg2.jpg">
    </div>
  </div>
</template>

<script>
import Header from "./components/layouts/Header.vue"
export default {
  name: "App",
  components: { Header },
};
</script>

<style>
@import "./style/root.css";
@import url("https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css");
.container {
  margin: 0 auto;
}

.bodyApp {
  position: relative;
  display: flex;
  width: 100%;
  overflow: hidden;
}
#image{
  position: relative;
  height: 833px;
  overflow: hidden;
}
</style>
