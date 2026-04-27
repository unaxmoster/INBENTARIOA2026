function login() {
  let user = document.getElementById("user").value;
  let pass = document.getElementById("pass").value;

  if (user === "admin" && pass === "1234") {
    localStorage.setItem("auth", "true");
    window.location.href = "dashboard.html";
  } else {
    document.getElementById("error").innerText = "Errorea datuetan";
  }
}

function logout() {
  localStorage.removeItem("auth");
  window.location.href = "login.html";
}

function checkAuth() {
  if (localStorage.getItem("auth") !== "true") {
    window.location.href = "login.html";
  }
}

(function(){
  emailjs.init("o0xdb9bnp5If9H5io");
})();

document.getElementById("contact-form").addEventListener("submit", function(e) {
  e.preventDefault();

  emailjs.sendForm('service_xptzyqv', 'template_7de8ki9', this)
    .then(() => {
      alert("Mezua bidalia!");
    }, (error) => {
      alert("Errorea");
    });
});