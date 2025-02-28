document.addEventListener("DOMContentLoaded", () => {
  const header = document.querySelector(".header");
  const introduction = document.querySelector(".introduction");

  const changeHeaderBackground = () => {
    const introductionBottom = introduction.getBoundingClientRect().bottom;

    if (introductionBottom <= 0) {
      header.classList.add("header--scrolled");
    } else {
      header.classList.remove("header--scrolled");
    }
  };

  window.addEventListener("scroll", changeHeaderBackground);
});
