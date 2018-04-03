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
                            '<a href="https://www.facebook.com/slutsksfc/" target="_blank"><span class="fcicon fcicon-facebook"></span></a>' +
                            '<a href="https://twitter.com/SlutskSfc" target="_blank"><span class="fcicon fcicon-twitter"></span></a>' +
                            '<a href="https://www.youtube.com/user/SFCSlutsk" target="_blank"><span class="fcicon fcicon-youtube"></span></a>' +
                            '<a href="https://www.instagram.com/sfc.slutsk/" target="_blank"><span class="fcicon fcicon-instagram"></span></a>' +
                            '<a href="https://vk.com/sfc.slutsk" target="_blank"><span class="fcicon fcicon-vk"></span></a>' +
                        '</div>'
        }
    }
})();