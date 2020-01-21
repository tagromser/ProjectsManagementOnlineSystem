///// <binding BeforeBuild='build' Clean='clear-folders' ProjectOpened='watcher-less, watcher-typescript' />

const gulp = require("gulp");
const autoprefixer = require("gulp-autoprefixer");
const cssmin = require("gulp-cssmin");
const less = require("gulp-less");
const typescript = require("gulp-typescript");
const sourcemaps = require("gulp-sourcemaps");
const rename = require("gulp-rename");
const pump = require("pump");
const del = require("del");
const lessDirectory = "./Content/Less";
const tsDirectory = "./Content/TypeScript";
const cssDirectory = "./wwwroot/Styles";
const javaScriptDirectory = "./wwwroot/Scripts";

const lessAllFiles = `${lessDirectory}/**/*.less`;
const tsAllFiles = `${tsDirectory}/**/*.ts`;
const cssAllFiles = `${cssDirectory}/**/*.css`;
const cssMinAllFiles = `${cssDirectory}/**/*min.css`;

const tsproject = typescript.createProject("./tsconfig.json");

gulp.task("clear-folder-css",
    function () {
        return del(cssDirectory);
    });

gulp.task("clear-folder-js",
    function () {
        return del(javaScriptDirectory);
    });

gulp.task("clear-folders", gulp.series(["clear-folder-css", "clear-folder-js"]));

gulp.task("less",
    function (errorhandler) {
        pump([
            gulp.src([
                `${lessDirectory}/generic.less`
            ]),
            less(),
            autoprefixer({
                overrideBrowserslist: ["last 2 versions", "ie >= 11"],
                cascade: true
            }),
            gulp.dest(cssDirectory)
        ], errorhandler);
    });

gulp.task("minify-css",
    function (errorhandler) {
        pump([
            gulp.src([cssAllFiles, `!${cssMinAllFiles}`]),
            cssmin(),
            rename({ suffix: ".min" }),
            gulp.dest(cssDirectory)
        ], errorhandler);
    });

gulp.task("minify", gulp.series(["minify-css"]));

gulp.task("typescript",
    function (errorHandler) {
        pump([
            gulp.src(tsAllFiles),
            sourcemaps.init(),
            tsproject(),
            sourcemaps.write(tsDirectory, { includeContent: true }),
            gulp.dest(javaScriptDirectory)
        ],
            errorHandler);
    });

gulp.task("build-less", gulp.series(["clear-folder-css", "less", "minify-css"]));
gulp.task("build-typescript", gulp.series(["clear-folder-js", "typescript"]));
gulp.task("build", gulp.series(["clear-folders", "build-less", "build-typescript"]));

gulp.task("watcher-less",
    function () {
        gulp.watch(lessAllFiles, gulp.series(["build-less"]));
    });

gulp.task("watcher-typescript",
    function () {
        gulp.watch(tsAllFiles, gulp.series(["build-typescript"]));
    });

gulp.task("watcher", gulp.series(["build", "watcher-less", "watcher-typescript"]));