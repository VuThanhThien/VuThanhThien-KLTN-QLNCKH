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
    }
  }
}
</script>

