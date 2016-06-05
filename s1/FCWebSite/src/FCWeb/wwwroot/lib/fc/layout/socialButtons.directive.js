(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('socialButtons', socialButtons);

    socialButtons.$inject = [];
    
    function socialButtons() {
        return {
            restrict: 'E',
            scope: {
            },

            link: function (scope, elem, attrs) {

            },
            template: '<div class="social-btns">' +
                            '<a href="https://www.youtube.com/user/SFCSlutsk" target="_blank"><img src="/images/skin/socials/youtube-btn.png" /></a>' +
                            '<a href="https://www.instagram.com/sfc.slutsk/" target="_blank"><img src="/images/skin/socials/instagram-btn.png" /></a>' +
                            '<a href="https://vk.com/sfc.slutsk" target="_blank"><img src="/images/skin/socials/vk-btn.png" /></a>' +
                            '<a href="https://twitter.com/SlutskSfc" target="_blank"><img src="/images/skin/socials/twitter-btn.png" /></a>' +
                            '<a href="https://www.facebook.com/slutsksfc/" target="_blank"><img src="/images/skin/socials/facebook-btn.png" /></a>' +
                        '</div>'
        }
    }
})();