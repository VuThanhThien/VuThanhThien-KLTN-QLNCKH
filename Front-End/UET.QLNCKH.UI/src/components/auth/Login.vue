<template>
  <div class="login-form">
    <h1 class="login-heading">Đăng nhập</h1>
    <form action="#" @submit.prevent="login">

      <div class="form-control">
        <label for="email">Tên tài khoản:</label>
        <input type="text" name="UserName" id="UserName" class="login-input" v-model="UserName">
      </div>

      <div class="form-control mb-more">
        <label for="Password">Mật khẩu:</label>
        <input type="Password" name="Password" id="Password" class="login-input" v-model="Password">
      </div>

      <div class="form-control">
        <button type="submit" class="btn-submit">Đăng nhập</button>
      </div>

    </form>
    <notifications position="bottom right" clean: true style="margin-bottom:
    20px"/>
  </div>
</template>

<script>
export default {
  name: 'Login',
  data() {
    return {
      UserName: '',
      Password: '',
    }
  },
  methods: {
    login() {
      this.$store.dispatch('retrieveToken', {
        UserName: this.UserName,
        Password: this.Password,
      })
        .then(response => {
          this.$router.push({ name: 'Home' })
        })
        .catch((e) => {
          if (e.response.status == 400) {
            this.$notify({
              type: "error",
              title: "THÔNG BÁO",
              text: e.response.data,
            });
          }
        });
    }
  }
}
</script>
<style scoped>

input{
  height: 50px;
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
    outline: none;
    border: none;
}
</style>
