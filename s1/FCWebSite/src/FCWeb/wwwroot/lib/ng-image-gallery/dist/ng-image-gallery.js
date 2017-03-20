 (function(){
	'use strict';
	
	// Key codes
	var keys = {
		enter : 13,
		esc   : 27,
		left  : 37,
		right : 39
	};

	angular
	.module('thatisuday.ng-image-gallery', ['ngAnimate'])
	.directive('ngImageGallery', ['$timeout', '$document', '$q', 'helper', function ($timeout, $document, $q) {
		return {
			replace : true,
			transclude : false,
			restrict : 'AE',
			scope : {
				images : '=',		// []
				methods : '=?',		// {}
				thumbnails : '=?',	// true|false
				inline : '=?',		// true|flase
				onOpen : '&?',		// function
				onClose : '&?'		// function
			},
			template : 	'<div class="ng-image-gallery" ng-class="{inline:inline}">'+
							
							// Thumbnails container
							//  Hide for inline gallery
							'<div ng-if="thumbnails && !inline" class="ng-image-gallery-thumbnails">'+
								'<div class="thumb" ng-repeat="image in images" ng-click="methods.open($index);" style="background-image:url({{image.thumbUrl || image.url}});"></div>'+
							'</div>'+

							// Modal container
							// (inline container for inline modal)
							'<div class="ng-image-gallery-modal" ng-show="opened" ng-cloak>' +
								
								// Gallery backdrop container
								// (hide for inline gallery)
								'<div class="ng-image-gallery-backdrop" ng-if="!inline"></div>'+
								
								// Gallery contents container
								// (hide when image is loading)
								'<div class="ng-image-gallery-content" ng-show="!imgLoading">'+
									
									// Icons
									// (hide close icon on inline gallery)
									'<div class="close" ng-click="methods.close();" ng-if="!inline"></div>'+
									'<div class="prev" ng-click="methods.prev();"></div>'+
									'<div class="next" ng-click="methods.next();"></div>'+


									// Galleria container
									'<div class="galleria">'+
										
										// Images container
										'<div class="galleria-images">'+
											'<img ng-repeat="image in images" ng-if="activeImg == image" ng-src="{{image.url}}" />'+
										'</div>'+

										// Bubble navigation container
										'<div class="galleria-bubbles">'+
											'<span ng-click="setActiveImg(image);" ng-repeat="image in images" ng-class="{active : (activeImg == image)}"></span>'+
										'</div>'+

									'</div>'+

								'</div>'+
								
								// Loading animation overlay container
								// (show when image is loading)
								'<div class="ng-image-gallery-loader" ng-show="imgLoading">'+
									'<div class="spinner">'+
										'<div class="rect1"></div>'+
										'<div class="rect2"></div>'+
										'<div class="rect3"></div>'+
										'<div class="rect4"></div>'+
										'<div class="rect5"></div>'+
									'</div>'+
								'</div>'+
							'</div>'+
						'</div>',
						
			link : function(scope, elem, attr){
				
				/*
				 *	Operational functions
				**/

				// Show gallery loader
				scope.showLoader = function(){
					scope.imgLoading = true;
				}

				// Hide gallery loader
				scope.hideLoader = function(){
					scope.imgLoading = false;
				}

				// Image load complete promise
				scope.loadImg = function(imgObj){
					var deferred =  $q.defer();

					// Show loder
					if(!imgObj.hasOwnProperty('cached')) scope.showLoader();

					// Process image
					var img = new Image();
					img.src = imgObj.url;
					img.onload = function(){
						// Hide loder
						if(!imgObj.hasOwnProperty('cached')) scope.hideLoader();

						// Cache image
						if(!imgObj.hasOwnProperty('cached')) imgObj.cached = true;

						return deferred.resolve(imgObj);
					}

					return deferred.promise;
				}

				scope.setActiveImg = function(imgObj){
					// Load image
					scope.loadImg(imgObj).then(function(imgObj){
						scope.activeImg = imgObj;
					});
				}

				/***************************************************/
				
				/*
				 *	Gallery settings
				**/

				// Modify scope models
				scope.images = (scope.images) ? scope.images : [];
				scope.methods = (scope.methods) ? scope.methods : {};
				scope.onOpen = (scope.onOpen) ? scope.onOpen : angular.noop;
				scope.onClose = (scope.onClose) ? scope.onClose : angular.noop;

				// If images populate dynamically, reset gallery
				var imagesFirstWatch = true;
				scope.$watch('images', function(){
					if(imagesFirstWatch){
						imagesFirstWatch = false;
					}
					else if(scope.images.length) scope.setActiveImg(
						scope.images[scope.activeImageIndex || 0]
					);
				});

				// Watch index of visible/active image
				// If index changes, make sure to load/change image
				var activeImageIndexFirstWatch = true;
				scope.$watch('activeImageIndex', function(newImgIndex){
					if(activeImageIndexFirstWatch){
						activeImageIndexFirstWatch = false;
					}
					else if(scope.images.length) scope.setActiveImg(
						scope.images[newImgIndex]
					);
				});

				// Open model automatically if inline
				scope.$watch('inline', function(){
					$timeout(function(){
						if(scope.inline) scope.methods.open();
					});
				});
				
				
				

				/***************************************************/

				/*
				 *	Methods
				**/

				// Open gallery modal
				scope.methods.open = function(imgIndex){
					// Open modal from an index if one passed
					scope.activeImageIndex = (imgIndex) ? imgIndex : 0;

					scope.opened = true; 

					 // call open event after transition
					$timeout(function(){
						scope.onOpen();
					}, 300);
				}


				// Close gallery modal
				scope.methods.close = function(){
					scope.opened = false; // Model closed

					// call close event after transition
					$timeout(function(){
						scope.onClose();
						scope.activeImageIndex = 0; // Reset index
					}, 300);
				}

				// Change image to next
				scope.methods.next = function(){
					if(scope.activeImageIndex == (scope.images.length - 1)){
						scope.activeImageIndex = 0;
					}
					else{
						scope.activeImageIndex = scope.activeImageIndex + 1;
					}
				}

				// Change image to prev
				scope.methods.prev = function(){
					if(scope.activeImageIndex == 0){
						scope.activeImageIndex = scope.images.length - 1;
					}
					else{
						scope.activeImageIndex--;
					}
				}

				/***************************************************/

				/*
				 *	User interactions
				**/

				// Key events
				$document.bind('keyup', function(event){
					// If inline model, do not interact
					if(scope.inline) return;

					if(event.which == keys.right || event.which == keys.enter){
						$timeout(function(){
							scope.methods.next();
						});
					}
					else if(event.which == keys.left){
						$timeout(function(){
							scope.methods.prev();
						});
					}
					else if(event.which == keys.esc){
						$timeout(function(){
							scope.methods.close();
						});
					}
				});

			}
		}
	}]);
 })();