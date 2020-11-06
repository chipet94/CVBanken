<template>
  <footer id="footer" class="site-footer">
    <div class="container">
      <div class="columns">
        <div class="column">
          <div class="brand">
            <img class="" src="https://www.iths.se/wp-content/themes/stella/assets/images/logo.svg">
          </div>
        </div>
        <div class="column">
          <ul class="social-icons">
            <li>
              <a class="facebook" href="https://www.facebook.com/ITHogskolan?ref=hl" target="_blank">
                <i class="fa fa-facebook"></i>
              </a>
            </li>

            <a href="https://www.instagram.com/ithogskolan/" target="_blank">
              <i class="fa fa-instagram"></i>
            </a>
            <a href="https://www.linkedin.com/school/it-hogskolan/?viewAsMember=true" target="_blank">
              <i class="fa fa-linkedin-square"></i>
            </a>
          </ul>
        </div>
        <div class="column">
          <div class="columns">
            <div class="column contact-container">
              <div class="">
                <p class="heading">
                  Göteborg
                </p>
                <a href="mailto:info@iths.se" target="_blank"><i class="fa fa-envelope-square"></i> info@iths.se</a>
                <a href="callto:031-790 42 55" target="_blank"><i class="fa fa-phone-square"></i> 031-790 42 55</a>
                <a href="https://www.google.se/search?q=Ebbe+Leiberathsgatan+18c&amp;oq=Ebbe+Leiberathsgatan+18c&amp;aqs=chrome..69i57j0.10475j0j4&amp;sourceid=chrome&amp;ie=UTF-8#q=Ebbe+Lieberathsgatan+18+c"
                   target="_blank"><i class="fa fa-map-marker"></i> Ebbe Lieberathsgatan 18 c</a>
              </div>
            </div>
            <div class="column contact-container">

              <div class="">
                <p class="heading">
                  Stockholm
                </p>
                <a href="mailto:info@iths.se" target="_blank"><i class="fa fa-envelope-square"></i> info@iths.se</a>
                <a href="callto:08-557 683 53" target="_blank"><i class="fa fa-phone-square"></i> 08-557 683 53</a>
                <a href="https://www.google.com/maps/place/%C3%85rsta%C3%A4ngsv%C3%A4gen+9,+117+43+Stockholm,+Sverige/@59.3109076,18.0275707,17z/data=!3m1!4b1!4m5!3m4!1s0x465f77c700f04af3:0x23dcb5ace59f0e6a!8m2!3d59.3109049!4d18.0297593?hl=sv-SE"
                   target="_blank"><i class="fa fa-map-marker"></i> Årstaängsvägen 9</a>
              </div>
            </div>
          </div>

        </div>

      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "IthsFooter"
}
</script>

<style scoped>

.brand {
  display: block;
  text-align: center;
  top: 0;
  bottom: 0;
}

.contact-container {
  display: flex;
  position: center;
  overflow: auto;
}

.contact-container div a {
  display: inline-block;
  float: left;
  color: white;
  padding-right: 10px;
  padding-left: 5px;
  font-size: small;
}

.contact-container div p {
  display: block;
  color: gray;
  font-weight: bold;
}

.contact-container div .heading {
  position: center;
}

.site-footer {
  background-color: #693250;
  padding: 45px 0 20px;
  font-size: 15px;
  line-height: 24px;
  color: #737373;
}

.site-footer hr {
  border-top-color: #bbb;
  opacity: 0.5
}

.site-footer hr.small {
  margin: 20px 0
}

.site-footer h6 {
  color: #fff;
  font-size: 16px;
  text-transform: uppercase;
  margin-top: 5px;
  letter-spacing: 2px
}

.site-footer a {
  color: #737373;
}

.site-footer a:hover {
  color: #3366cc;
  text-decoration: none;
}

.footer-links {
  padding-left: 0;
  list-style: none
}

.footer-links li {
  display: block
}

.footer-links a {
  color: #737373
}

.footer-links a:active, .footer-links a:focus, .footer-links a:hover {
  color: #3366cc;
  text-decoration: none;
}

.footer-links.inline li {
  display: inline-block
}

.site-footer .social-icons {
  text-align: center;
}

.site-footer .social-icons a {
  width: 40px;
  height: 40px;
  line-height: 40px;
  margin-left: 6px;
  margin-right: 0;
  border-radius: 100%;
  background-color: #794761;
}

.copyright-text {
  margin: 0
}

@media (max-width: 991px) {
  .site-footer [class^=col-] {
    margin-bottom: 30px
  }
}

@media (max-width: 767px) {
  .site-footer {
    padding-bottom: 0
  }

  .site-footer .copyright-text, .site-footer .social-icons {
    text-align: center
  }
}

.social-icons {
  padding-left: 0;
  margin-bottom: 0;
  list-style: none
}

.social-icons li {
  display: inline-block;
  margin-bottom: 4px
}

.social-icons li.title {
  margin-right: 15px;
  text-transform: uppercase;
  color: #959fad;
  font-weight: 700;
  font-size: 13px
}

.social-icons a {
  background-color: #eceeef;
  color: #818a91;
  font-size: 16px;
  display: inline-block;
  line-height: 44px;
  width: 44px;
  height: 44px;
  text-align: center;
  margin-right: 8px;
  border-radius: 100%;
  -webkit-transition: all .2s linear;
  -o-transition: all .2s linear;
  transition: all .2s linear
}

.social-icons a:active, .social-icons a:focus, .social-icons a:hover {
  color: #fff;
  background-color: #29aafe
}

.social-icons.size-sm a {
  line-height: 34px;
  height: 34px;
  width: 34px;
  font-size: 14px
}

.social-icons a.facebook:hover {
  background-color: #3b5998
}

.social-icons a.twitter:hover {
  background-color: #00aced
}

.social-icons a.linkedin:hover {
  background-color: #007bb6
}

.social-icons a.dribbble:hover {
  background-color: #ea4c89
}

.is-ITHS-purple {
  background-color: #693250;
  color: white;
}

</style>