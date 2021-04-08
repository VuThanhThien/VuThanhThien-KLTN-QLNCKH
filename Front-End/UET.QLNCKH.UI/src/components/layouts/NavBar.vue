<template>
<div>
  <div class="navBar"  v-if="loggedIn">
    <div class="headerBtn">
      <router-link :to="{ name: 'ListTopic' }">
        <DxButton
          :width="120"
          text="Đề tài"
          type="success"
          styling-mode="contained"
        />
      </router-link>
    </div>
    <div class="headerBtn">
      <router-link :to="{ name: 'ListAuthor' }">
        <DxButton
          :width="120"
          text="Tác giả"
          type="success"
          styling-mode="contained"
        />
      </router-link>
    </div>

    <div class="headerBtn">
      <DxButton
        :width="120"
        text="Thống kê"
        type="success"
        styling-mode="contained"
      />
    </div>
  </div>

  

  </div>
</template>

<script>
import DxButton from "devextreme-vue/button";

export default {
  components: {
    DxButton,
  },
  computed: {
    loggedIn() {
      return this.$store.getters.loggedIn;
    },
  },
};
</script>

<style lang="scss" scoped>
.navBar {
  display: flex;
  list-style: none;
  padding: 15px 0;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
}
.headerBtn{
  margin-right: 15px;
}
</style>
