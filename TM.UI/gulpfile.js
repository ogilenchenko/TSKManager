var gulp = require("gulp"),
    ngAnnotate = require('gulp-ng-annotate'),
    minifyCSS = require("gulp-minify-css"),
    uglify = require('gulp-uglify'),
    usemin = require('gulp-usemin'),
    jshint = require('gulp-jshint'),
    rev = require('gulp-rev'),
    del = require('del');

var paths = {
    dev: {
        bowerDir: "./bower_components",
        css: 'content/**/*.css',
        html: './*.html',
        js: 'app/**/*.js',
        views: 'views'
    },
    build: {
        base: 'dist/',
        fonts: 'dist/fonts',
        views: 'dist/views'
    }
}

// JS Lint
//gulp.task('jshint', function() {
//    return gulp.src(paths.dev.js)
//        .pipe(jshint())
//        .pipe(jshint.reporter('default'))
//        .pipe(jshint.reporter('fail'));
//});

// Usemin : Concat & Minify Scripts un add revision to avoid cache
gulp.task('usemin', function() {
    return gulp.src(paths.dev.html)
        .pipe(usemin({
            css: ['concat', minifyCSS()],
            vendorjs: [uglify(), rev()],
            js: [ngAnnotate(), uglify(), rev()]
        }))
        .pipe(gulp.dest(paths.build.base));
});

// Copy fonts for dist
gulp.task('fonts', function() {
    return gulp.src([paths.dev.bowerDir + '/font-awesome/fonts/*', paths.dev.bowerDir + '/bootstrap/fonts/*'])
        .pipe(gulp.dest(paths.build.fonts));
});

// Copy fonts for dist
gulp.task('views', function () {
    return gulp.src(paths.dev.views + '/*')
    .pipe(gulp.dest(paths.build.views));
});

gulp.task('clean', function() {
    del.sync(paths.build.base);
});

gulp.task('default', ['clean',
   // 'jshint',
     'fonts', 'views', 'usemin'], function () {
});