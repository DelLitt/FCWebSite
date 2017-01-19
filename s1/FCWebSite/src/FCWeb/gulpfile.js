/// <binding />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less"),
    project = require('./project.json');

var wwwroot = "./wwwroot/";

var paths = {
    webroot: wwwroot,
    fclib: wwwroot + "lib/fc/"
};

//paths.js = paths.webroot + "js/**/*.js";
paths.js = paths.webroot + "lib/fc/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatJsOfficeDest = paths.webroot + "js/site.office.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

paths.fc = {    
    js: {
        front: [
            paths.webroot + "lib/angular-route/angular-route.js",
            paths.webroot + "lib/angular-translate/angular-translate.js",
            paths.webroot + "lib/angular-translate-storage-local/angular-translate-storage-local.js",
            paths.webroot + "lib/angular-translate-loader-static-files/angular-translate-loader-static-files.js",
            paths.webroot + "lib/angular-sanitize/angular-sanitize.js",
            paths.webroot + "lib/angular-animate/angular-animate.js",
            paths.webroot + "lib/ng-image-gallery/dist/ng-image-gallery.js",

            paths.fclib + "modules/fc.ui.js",
            paths.fclib + "modules/fc.core.js",
            paths.fclib + "app.js",
            "!" + paths.fclib + "office/**/*.js",
            "!" + paths.fclib + "layout/office/**/*.js",
            paths.fclib + "**/*.js",
        ],
        office: [
            paths.webroot + "lib/angular-route/angular-route.js",
            paths.webroot + "lib/angular-translate/angular-translate.js",
            paths.webroot + "lib/angular-translate-storage-local/angular-translate-storage-local.js",
            paths.webroot + "lib/angular-translate-loader-static-files/angular-translate-loader-static-files.js",
            paths.webroot + "lib/angular-sanitize/angular-sanitize.js",
            paths.webroot + "lib/angular-animate/angular-animate.js",
            paths.webroot + "lib/angular-ui-bootstrap/ui-bootstrap-tpls-1.2.4.js",
            paths.webroot + "lib/angular-ui-bootstrap-datetimepicker/datetimepicker.js",
            paths.webroot + "lib/angular-ui-numeric-master/src/angular-ui-numeric.js",
            paths.webroot + "lib/ng-file-upload/ng-file-upload.js",
            paths.webroot + "lib/ng-file-upload/ng-file-upload-shim.js",
            paths.webroot + "lib/simple-angular-autocomplete-master/src/simple-autocomplete.js",
            paths.webroot + "lib/ckeditor/ckeditor.js",

            paths.fclib + "modules/fc.ui.js",
            paths.fclib + "modules/fc.core.js",
            paths.fclib + "appAdmin.js",
            paths.fclib + "modules/**/*.js",
            paths.fclib + "office/**/*.js",
            paths.fclib + "layout/office/**/*.js",
            paths.fclib + "**/*.js",
        ]
    }
};

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
    //rimraf(paths.concatJsOfficeDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

//gulp.task("min:js", function () {
//    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
//        .pipe(concat(paths.concatJsDest))
//        .pipe(uglify())
//        .pipe(gulp.dest("."));
//});

gulp.task("min:jsfront", function () {
    return gulp.src(paths.fc.js.front, { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:jsoffice", function () {
    return gulp.src(paths.fc.js.office, { base: "." })
        .pipe(concat(paths.concatJsOfficeDest))
        //.pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js", ["min:jsfront", "min:jsoffice"]);

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("less", function () {
    console.log(paths.webroot);

    return gulp.src(paths.webroot + '/lib/bootstrap/less/bootstrap.less')
            .pipe(less())
            .pipe(gulp.dest(paths.webroot + '/css'));
});
