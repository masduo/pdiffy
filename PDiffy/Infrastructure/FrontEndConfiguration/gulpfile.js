var path = require('path');
var fs = require('fs');

var gulp = require('gulp');

var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var endOfLine = require('gulp-eol');
var gulpwebpack = require('gulp-webpack');

var paths = require('./paths');
var webPackDev = require('./webpack.dev');
var webPack = require('./webpack');
var styles = require('./styles.entries');

gulp.task('webpack-dev', function () {
	gulp.src(webPackDev.src)
		.pipe(gulpwebpack(webPackDev))
		.pipe(endOfLine())
		.pipe(gulp.dest(webPackDev.output.path));
});

gulp.task('webpack', function () {
	gulp.src(webPack.src)
		.pipe(gulpwebpack(webPack))
		.pipe(endOfLine())
		.pipe(gulp.dest(webPack.output.path));
});

gulp.task('styles', function () {
	gulp.src(styles)
        .pipe(sass())
		.pipe(autoprefixer({ browsers: ['last 3 versions'] }))
        .pipe(gulp.dest(paths.SRC.STYLES));
});

gulp.task('watch', function () {
	gulp.watch(
		[paths.SRC.SCRIPTS,
		paths.SRC.STYLES,
		paths.SRC.FEATURES_SCSS,
		paths.SRC.FEATURES_JS], ['styles', 'webpack-dev']);
});

gulp.task('dev', ['styles','webpack-dev', 'watch']);
gulp.task('prod', ['styles', 'webpack']);

gulp.task('default', ['prod']);
