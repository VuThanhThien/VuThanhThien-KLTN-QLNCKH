<template>
  <div id="app">
    <ul class="nav">
      <li>
        <img src="@/assets/Logo-UETEureka.png" class="logo-eureka" />
      </li>
      <li title="Thông tin của tôi">
        <router-link id="myTopic" :to="{ name: 'MyInfo' }">
          {{ this.currentName }}
        </router-link>
      </li>
      <li v-if="loggedIn">
        <router-link :to="{ name: 'Home' }">Trang chủ</router-link>
      </li>
      <li><router-link :to="{ name: 'about' }">Chi tiết</router-link></li>
      <li v-if="!loggedIn">
        <router-link :to="{ name: 'Login' }">Đăng nhập</router-link>
      </li>
      <li v-if="loggedIn">
        <router-link :to="{ name: 'logout' }">Đăng xuất</router-link>
      </li>
    </ul>
    <nav-bar />

    <router-view></router-view>
  </div>
</template>

<script>
import NavBar from "./NavBar.vue";
import DxDropDownButton from "devextreme-vue/drop-down-button";
import service from "../../../modules/data.js";
export default {
  components: {
    NavBar,
    DxDropDownButton,
  },
  computed: {
    loggedIn() {
      return this.$store.getters.loggedIn;
    },
    currentName() {
      return this.$store.getters.currentName;
    },
  },
  data() {
    return {
      myRoteInName: service.getRoute(),
    };
  },
  methods: {
    onItemClick(e) {
      this.$router.push({ name: e.itemData });
    },
  },
};
</script>

<style lang="scss">
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  font-size: 24px;
}
#myTopic:hover {
  color: #60bd4f !important;
}
.flex-center {
  display: flex;
  justify-content: center;
}

.nav {
  margin-right: 10px;
  display: flex;
  list-style: none;
  padding: 15px 0;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
}

.nav a {
  color: #636b6f;
  padding: 0 25px;
  font-size: 14px;
  font-weight: 600;
  letter-spacing: 0.1rem;
  text-decoration: none;
  text-transform: uppercase;
}
.logo-eureka {
  position: absolute;
  left: 20px;
  height: 45px;
  width: auto;
}
// Auth Pages

label {
  display: block;
  margin-bottom: 4px;
}

.login-heading {
  margin-bottom: 16px;
}

.form-control {
  margin-bottom: 24px;
}

.mb-more {
  margin-bottom: 42px;
}

.login-form {
  max-width: 500px;
  margin: auto;
}

.login-input {
  width: 100%;
  font-size: 16px;
  padding: 12px 16px;
  outline: 0;
  border-radius: 3px;
  border: 1px solid lightgrey;
}

.btn-submit {
  width: 100%;
  padding: 14px 12px;
  font-size: 18px;
  font-weight: bold;
  background: #60bd4f;
  color: white;
  border-radius: 3px;
  cursor: pointer;

  &:hover {
    background: darken(#60bd4f, 10%);
  }
}
</style>

